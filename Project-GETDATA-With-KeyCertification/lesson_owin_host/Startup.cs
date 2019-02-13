using Microsoft.Owin;
using System.Threading.Tasks;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;

namespace lesson_owin_host
{
     using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Startup
    {

            public void Configuration(IAppBuilder app)
            {
                app.Use<Middleware>();
                app.Use(new Func<AppFunc, AppFunc>(MetodoTrataRequisicaoMiddleware));
                app.Use(new Func<AppFunc, AppFunc>(InterfaceMiddleware));
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("usando o metodo Run <br>");
                });
     
            }

        private static AppFunc InterfaceMiddleware(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> dic) =>
            {
                IOwinContext context = new OwinContext(dic);
                await context.Response.WriteAsync("usando a IOwinContext <br>");
                await next.Invoke(dic);
            };
     
            return appFunc;
        }
            
        // Este método é um middleware. 
        private static AppFunc MetodoTrataRequisicaoMiddleware(AppFunc next)
        {
            return (
                // Contexto é um dicionário que contém toda a informação sobre a requisição.
                    async context =>
                    {
                        using (var sw = new StreamWriter((Stream)context["owin.ResponseBody"]))
                        {
                            await sw.WriteAsync("usando o metodo Use <br>");
                        }
                        await next.Invoke(context);
                    }
                );
        }
    }
    }
