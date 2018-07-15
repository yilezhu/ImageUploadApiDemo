using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ImageUploadApiDemo.Options;

namespace ImageUploadApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注入Options
            OptionsConfigure(services);
            var identityServerOptions = new IdentityServerOptions();
            Configuration.Bind("IdentityServerOptions", identityServerOptions);
            services.AddAuthentication(identityServerOptions.IdentityScheme)
                .AddIdentityServerAuthentication(options =>
                    {
                        options.RequireHttpsMetadata = false; //是否启用https
                        options.Authority = $"http://{identityServerOptions.ServerIP}:{identityServerOptions.ServerPort}";//配置授权认证的地址
                        options.ApiName = identityServerOptions.ResourceName; //资源名称，跟认证服务中注册的资源列表名称中的apiResource一致
                    }
                );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc();
        }

        /// <summary>
        /// yilezhu
        /// 2018.7.10
        /// 注册Options
        /// </summary>
        /// <param name="services">服务容器</param>
        private void OptionsConfigure(IServiceCollection services)
        {
            //MongodbHost信息
            services.Configure<MongodbHostOptions>(Configuration.GetSection("MongodbHost"));
            //图片选项
            services.Configure<PictureOptions>(Configuration.GetSection("PictureOptions"));

        }



    }
}
