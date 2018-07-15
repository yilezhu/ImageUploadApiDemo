using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    /// <summary>
    /// yilezhu
    /// 2018.7.15
    /// 因为此处采用in-memory，所以硬编码一些api，以及client
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// 定义ApiResource   这里的资源（Resources）指的就是我们的API
        /// </summary>
        /// <returns>ApiResource枚举</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("PictureApi", "图片上传的APi")
            };
        }

        /// <summary>
        /// 定义受信任的客户端 Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "MobileUploadPicture",//客户端的标识，要是惟一的
                    ClientSecrets = new [] { new Secret("yilezhu123".Sha256()) },//客户端密码，进行了加密
                    AllowedGrantTypes = GrantTypes.ClientCredentials,//授权方式，这里采用的是客户端认证模式，只要ClientId，以及ClientSecrets正确即可访问对应的AllowedScopes里面的api资源
                    AllowedScopes = new [] { "PictureApi" }//定义这个客户端可以访问的APi资源数组，上面只有一个api
                }
            };
        }
    }
}
