using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Options
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 连接字符串，库，表(暂时这样理解)
    /// </summary>
    public class MongodbHostOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 库
        /// </summary>
        public string DataBase { get; set; }
        /// <summary>
        /// 表
        /// </summary>
        public string Table { get; set; }
    }
}
