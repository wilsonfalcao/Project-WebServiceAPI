using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace lesson_owin_host
{
    public class Certificado
    {
      public static X509Certificate ExtrairCertificado()
      {
          return X509Certificate.CreateFromCertFile(@"C:\Users\pcr-27929\Documents\wilsoncert.cer");
      }

    }
}
