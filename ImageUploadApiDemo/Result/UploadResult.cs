using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Result
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 上传结果实体
    /// </summary>
    public class UploadResult:BaseResult
    {
        /// <summary>
        /// 上传实体数据
        /// </summary>
        public UploadEntity Data { get; set; }
    }
}
