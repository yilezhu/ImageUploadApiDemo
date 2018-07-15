using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Models
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 图片存储信息类，跟MongoDB里面表名一致
    /// </summary>
    public class Images_Mes : MongoEntity
    {
        /// <summary>
        /// 上传的文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件名后缀
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; } 
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTimeOffset AddTime { get; set; } 
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTimeOffset ModifyTime { get; set; } 
        /// <summary>
        /// 上传人员
        /// </summary>
        public string AddUser { get; set; } 
        /// <summary>
        /// 上传的的文件流
        /// </summary>
        public byte[] FileCon { get; set; } 
    }
}
