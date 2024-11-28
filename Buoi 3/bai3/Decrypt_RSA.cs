using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bai3
{
    public class Decrypt_RSA
    {
        private RSACryptoServiceProvider _rsa;

        public Decrypt_RSA(string privateKey)
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.ImportCspBlob(Convert.FromBase64String(privateKey));
        }

        public string Decrypt(string encryptedText)
        {
            byte[] decryptedData = _rsa.Decrypt(Convert.FromBase64String(encryptedText), true);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public static void Run()
        {
            string encryptedTextFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encrypted.txt");

            string encryptedText = File.ReadAllText(encryptedTextFilePath);
            Console.WriteLine("\nEncrypted Text: " + encryptedText);

            string privateKeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private_key.txt");

            string privateKey = File.ReadAllText(privateKeyFilePath);
            Console.WriteLine("\nPrivate Key");
            Console.WriteLine(privateKey);

            Decrypt_RSA decryptRsa = new Decrypt_RSA(privateKey);
            string decryptedText = decryptRsa.Decrypt(encryptedText);

            Console.WriteLine("\nDecrypted Text: " + decryptedText);
            Console.ReadLine();
        }
    }
}
