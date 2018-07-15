using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadApiDemo.Models
{
    /// <summary>
    /// yilezhu
    /// 2018.7.10
    /// MongoEntity基类
    /// </summary>
    public abstract class MongoEntity
    {
        /// <summary>
        /// BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId
        /// </summary>
        public ObjectId Id { get; set; }
        /// <summary>
        /// 返回给上传者的自编主键
        /// </summary>
        public string GuidID { get; set; }

    }
}
