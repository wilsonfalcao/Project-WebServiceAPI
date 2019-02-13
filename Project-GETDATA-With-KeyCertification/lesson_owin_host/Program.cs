using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http;
using System.Net.Http.Formatting;


namespace lesson_owin_host
{
    public class Program
    {
        static void Main(string[] args)
        {
            sxv sss = new sxv();
            while (1 > 0)
            {
                Console.WriteLine("Digite o q vc quer? ");
                string op = Console.ReadLine();
                switch (op)
                {
                    case ("1"): sss.Add_Token(); break;
                    case ("2"): sss.Autorization(sss.token1); break;
                }
            }
            
        }
    }
        public class sxv
        {
        
        public void Add_Token()
        {
            string baseAddress = "http://localhost:53565/";
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>    
               {    
                   {"grant_type", "password"},    
                   {"username", "carlos"},    
                   {"password", "123456"},    
               };
                var tokenResponse = client.PostAsync(baseAddress + "Token", new FormUrlEncodedContent(form)).Result;
                var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (string.IsNullOrEmpty(token.Error))
                {
                    token1 = token.AccessToken;
                    Console.WriteLine("Token issued is: {0}", token.AccessToken);
                }
                else
                {
                    Console.WriteLine("Error : {0}", token.Error);
                }
                Console.Read();
            }
        }
        public void Autorization(object Token1)
        {

            string baseAddress = "http://localhost:53565/api/me/?ct=asdasd&cd=asdasd";
            WebRequestHandler handler =  new WebRequestHandler();
            handler.ClientCertificates.Add(Certificado.ExtrairCertificado());
                using (var client = new HttpClient(handler))
                {
                   var form = new Dictionary<string, string>    
                    {    
                    {"Authorization", "Bearer "+Token1}    
                    };
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token1.ToString());
                    var nome = client.GetAsync(baseAddress + "api/me").Result;
                    nome.EnsureSuccessStatusCode();
                    Console.WriteLine(nome.Content.ReadAsStringAsync().Result);
                    Console.Read();
                }
        }

        public object token1 { get; set; }
    }
}
