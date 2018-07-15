using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Dtos
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 文件上传的信息
    /// </summary>
    public class FileDtos
    {
        /// <summary>
        /// 文件类型
        /// image/file/video
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 文件名称，包含扩展名
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// base64String
        /// </summary>
        public string Base64String { get; set; }
    }
}
