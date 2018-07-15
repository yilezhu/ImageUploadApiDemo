using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Helper
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 返回结果编码以及对应的信息
    /// </summary>
    public class ResultCodeAddMsgKeys
    {
        #region 通用 100
        /// <summary>
        /// 通用成功编码
        /// </summary>
        public const int CommonObjectSuccessCode = 0;
        /// <summary>
        /// 通用操作成功信息
        /// </summary>
        public const string CommonObjectSuccessMsg = "操作成功";
        /// <summary>
        /// 通用Form验证失败错误码
        /// </summary>
        public const int CommonModelStateInvalidCode = 101;
        /// <summary>
        /// 通用Form验证失败错误码
        /// </summary>
        public const string CommonModelStateInvalidMsg = "请求数据校验失败";
        #endregion

        #region 文件错误
        /// <summary>
        /// 文件类型参数错误代码
        /// </summary>
        public const int HttpFileTypeInvalidCode = 600;
        /// <summary>
        /// 文件类型参数错误信息
        /// </summary>
        public const string HttpFileTypeInvalidMsg = "文件类型参数错误";
        /// <summary>
        /// 不支持此文件类型代码
        /// </summary>
        public const int HttpFileInvalidCode = 601;
        /// <summary>
        /// 不支持此文件类型代码信息
        /// </summary>
        public const string HttpFileInvalidMsg = "不支持的文件类型，如有需要请联系管理员开通";
        /// <summary>
        /// 不支持此文件类型代码
        /// </summary>
        public const int HttpFileNotFoundCode = 604;
        /// <summary>
        /// 不支持此文件类型代码信息
        /// </summary>
        public const string HttpFileNotFoundMsg = "未检索到需要上传的文件";
        #endregion
    }
}
