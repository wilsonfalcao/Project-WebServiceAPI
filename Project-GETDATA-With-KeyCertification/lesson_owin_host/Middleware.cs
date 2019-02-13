using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;


namespace lesson_owin_host
{
    public class Middleware
    {
         private Func<IDictionary<string, object>, Task> _next;
            
    public Middleware(Func<IDictionary<string, object>, Task> next)
            {
                _next = next;
            }
    public async Task Invoke(IDictionary<string, object> dict)
            {
                // Contexto é um dicionário que contém toda a informação sobre a requisição.
           using (var sw = new StreamWriter((Stream)dict["owin.ResponseBody"]))
           {
                    await sw.WriteAsync("usando a Definicao <br>");
           }
           await _next.Invoke(dict);
            }
    }
}
