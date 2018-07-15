using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Result
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// 所有结果的基类，用于书写公共信息
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public int Errcode { get; set; }
        /// <summary>
        /// 结果消息 如果不成功，返回的错误信息
        /// </summary>
        public string Errmsg { get; set; }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BaseResult()
        {

        }

        /// <summary>
        /// 有参数构造函数
        /// </summary>
        /// <param name="_errcode"></param>
        /// <param name="_errmsg"></param>
        public BaseResult(int _errcode, string _errmsg)
        {
            Errcode = _errcode;
            Errmsg = _errmsg;
        }
    }
}
