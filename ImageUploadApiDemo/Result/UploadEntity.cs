using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Result
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 上传实体
    /// </summary>
    public class UploadEntity
    {
        /// <summary>
        /// 图片主键
        /// </summary>
        public string Picguid { get; set; }
        /// <summary>
        /// 原始图片地址
        /// </summary>
        public string Originalurl { get; set; }
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string Thumburl { get; set; }
    }
}
