using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ImageUploadApiDemo.Dtos;
using ImageUploadApiDemo.Helper;
using ImageUploadApiDemo.Models;
using ImageUploadApiDemo.Options;
using ImageUploadApiDemo.Result;

namespace ImageUploadApiDemo.Controllers
{

    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 图片操作相关接口
    /// </summary>
    public class PictureController : BaseController
    {
        // MongodbHost信息
        private readonly MongodbHostOptions _mongodbHostOptions;

        // 图片选项
        private readonly PictureOptions _pictureOptions;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="mongodbHostOptions">MongodbHost信息</param>
        /// <param name="pictureOptions">图片选项</param>
        public PictureController(IOptions<MongodbHostOptions> mongodbHostOptions, IOptions<PictureOptions> pictureOptions)
        {
            _mongodbHostOptions = mongodbHostOptions.Value;
            _pictureOptions = pictureOptions.Value;
        }

        /// <summary>
        /// 接口上传图片方法
        /// </summary>
        /// <param name="fileDtos">文件传输对象,传过来的json数据</param>
        /// <returns>上传结果</returns>
        [HttpPost]
        [Authorize]
        public async Task<UploadResult> Post([FromBody] FileDtos fileDtos)
        {
            UploadResult result = new UploadResult();
            if (ModelState.IsValid)
            {
                #region  验证通过
                //首先根据api参数判断是否为图片类型，是则处理，不是则返回对应的结果
                if (!string.IsNullOrEmpty(fileDtos.Type) && fileDtos.Type.ToLower() == "image")
                {
                    //文件类型
                    string FileEextension = Path.GetExtension(fileDtos.Filename).ToLower();//获取文件的后缀
                                                                                           //判断文件类型是否是允许的类型
                    if (_pictureOptions.FileTypes.Split(',').Contains(FileEextension))
                    {
                        //图片类型是允许的类型
                        Images_Mes fmster = new Images_Mes();//图片存储信息类，跟MongoDB里面表名一致
                        string fguid = Guid.NewGuid().ToString().Replace("-",""); //文件名称
                        fmster.AddTime = DateTimeOffset.Now;//添加时间为当前时间
                        fmster.AddUser = "server";//具体根据你的业务来获取
                        if (Base64Helper.IsBase64String(fileDtos.Base64String, out byte[] fmsterByte))
                        {
                            //判断是否是base64字符串，如果是则转换为字节数组，用来保存
                            fmster.FileCon = fmsterByte;
                        }
                        fmster.FileName = Path.GetFileName(fileDtos.Filename);//文件名称
                        fmster.FileSize = fmster.FileCon.Length;//文件大小
                        fmster.FileType = FileEextension;//文件扩展名
                        fmster.GuidID = fguid;//唯一主键，通过此来获取图片数据
                        await MongodbHelper<Images_Mes>.AddAsync(_mongodbHostOptions, fmster);//上传文件到mongodb服务器
                                                                                              //检查是否需要生产缩略图
                        if (_pictureOptions.MakeThumbnail)
                        {
                            //生成缩略图
                            Images_Mes fthum = new Images_Mes();
                            fthum.AddTime = DateTimeOffset.Now;
                            fthum.AddUser = "server";//具体根据你的业务来获取
                            fthum.FileCon = ImageHelper.GetReducedImage(fmster.FileCon, _pictureOptions.ThumsizeW, _pictureOptions.ThumsizeH);
                            fthum.FileName = Path.GetFileNameWithoutExtension(fileDtos.Filename) + "_thumbnail" + Path.GetExtension(fileDtos.Filename);//生成缩略图的名称
                            fthum.FileSize = fthum.FileCon.Length;//缩略图大小
                            fthum.FileType = FileEextension;//缩略图扩展名
                            fthum.GuidID = fguid + _pictureOptions.ThumbnailGuidKeys;//为了方面，缩略图的主键为主图主键+一个字符yilezhu作为主键
                            await MongodbHelper<Images_Mes>.AddAsync(_mongodbHostOptions, fthum);//上传缩略图到mongodb服务器
                        }
                        result.Errcode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                        result.Errmsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                        UploadEntity entity = new UploadEntity();
                        entity.Picguid = fguid;
                        entity.Originalurl = _pictureOptions.ImageBaseUrl + fguid;
                        entity.Thumburl = _pictureOptions.ImageBaseUrl + fguid + _pictureOptions.ThumbnailGuidKeys;
                        result.Data = entity;
                        return result;
                    }
                    else
                    {
                        //图片类型不是允许的类型
                        result.Errcode = ResultCodeAddMsgKeys.HttpFileInvalidCode;//对应的编码
                        result.Errmsg = ResultCodeAddMsgKeys.HttpFileInvalidMsg;//对应的错误信息
                        result.Data = null;//数据为null
                        return result;
                    }

                }
                else
                {
                    result.Errcode = ResultCodeAddMsgKeys.HttpFileNotFoundCode;
                    result.Errmsg = ResultCodeAddMsgKeys.HttpFileNotFoundMsg;
                    result.Data = null;
                    return result;
                }
                #endregion
            }
            else
            {
                #region 验证不通过
                StringBuilder errinfo = new StringBuilder();
                foreach (var s in ModelState.Values)
                {
                    foreach (var p in s.Errors)
                    {
                        errinfo.AppendFormat("{0}||", p.ErrorMessage);
                    }
                }
                result.Errcode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.Errmsg = errinfo.ToString();
                result.Data = null;
                return result;
                #endregion
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="guid">原始图片主键</param>
        /// <returns>执行结果</returns>
        [HttpDelete("{guid}")]
        public async Task<BaseResult> Delete(string guid)
        {
            await MongodbHelper<Images_Mes>.DeleteAsync(_mongodbHostOptions, guid);//删除mongodb服务器上对应的文件
            await MongodbHelper<Images_Mes>.DeleteAsync(_mongodbHostOptions, guid + _pictureOptions.ThumbnailGuidKeys);//删除mongodb服务器上对应的文件
            return new BaseResult(ResultCodeAddMsgKeys.CommonObjectSuccessCode, ResultCodeAddMsgKeys.CommonObjectSuccessMsg);
        }

        /// <summary>
        /// 返回图片对象
        /// </summary>
        /// <param name="guid">图片的主键</param>
        /// <returns>图片对象</returns>
        [Route("Show/{guid}")]
        [HttpGet]
        public async Task<FileResult> ShowAsync(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return null;
            }
            FilterDefinition<Images_Mes> filter = Builders<Images_Mes>.Filter.Eq("GuidID", guid);
            var result= await MongodbHelper<Images_Mes>.FindListAsync(_mongodbHostOptions, filter);
            if (result != null && result.Count > 0)
            {
                return File(result[0].FileCon, "image/jpeg", result[0].FileName);
            }
            else
            {
                return null;
            }
           
        }
    }
}