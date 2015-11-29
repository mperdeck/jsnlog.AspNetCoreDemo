using Microsoft.AspNet.Builder;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Owin;

// See http://stackoverflow.com/questions/30742028/how-to-use-iappbuilder-based-owin-middleware-in-asp-net-5
// Allows you to use an OWIN middleware in the ASP.NET 5 pipeline

//namespace WebSite
//{
//    internal static class IApplicationBuilderExtensions
//    {
//        public static void UseOwin(
//          this IApplicationBuilder app,
//          Action<IAppBuilder> owinConfiguration)
//        {
//            app.UseOwin(
//              addToPipeline =>
//              {
//                  addToPipeline(
//                    next =>
//                    {
//                        var builder = new AppBuilder();

//                        owinConfiguration(builder);

//                        builder.Run(ctx => next(ctx.Environment));

//                        Func<IDictionary<string, object>, Task> appFunc =
//                          (Func<IDictionary<string, object>, Task>)
//                          builder.Build(typeof(Func<IDictionary<string, object>, Task>));

//                        return appFunc;
//                    });
//              });
//        }
//    }
//}
