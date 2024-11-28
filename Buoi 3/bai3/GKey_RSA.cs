using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bai3
{
    public class GKey_RSA
    {
        public static void Run()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                string publicKey = Convert.ToBase64String(rsa.ExportCspBlob(false));
                string privateKey = Convert.ToBase64String(rsa.ExportCspBlob(true));
                string currentDirectory = Path.GetDirectoryName(typeof(GKey_RSA).Assembly.Location);

                File.WriteAllText(Path.Combine(currentDirectory, "public_key.txt"), publicKey);
                File.WriteAllText(Path.Combine(currentDirectory, "private_key.txt"), privateKey);

            }
        }
    }
}
