using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Extensions
{
    public static class StaticFilesExtension
    {
        public static IApplicationBuilder RegisterStaticFiles(this IApplicationBuilder app)
        {
            Dictionary<string, string> _staticPaths = new Dictionary<string, string>
            {
                { "Areas\\Product\\Scripts", "/Product/Scripts" },
            };


            foreach (KeyValuePair<string, string> _staticPath in _staticPaths)
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), _staticPath.Key)),
                    RequestPath = new PathString(_staticPath.Value)
                });
            }

            return app;
        }
    }
}
