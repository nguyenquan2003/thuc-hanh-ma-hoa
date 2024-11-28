using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bai3
{
    public class Encrypt_RSA
    {
        private RSACryptoServiceProvider _rsa;

        public Encrypt_RSA(string publickey)
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.ImportCspBlob(Convert.FromBase64String(publickey));
        }

        public byte[] EncryptWithRSAPublicKey(byte[] data)
        {
            return _rsa.Encrypt(data, true);
        }

        public string Encrypt(string plaintext)
        {
            byte[] encrpytedData = EncryptWithRSAPublicKey(Encoding.UTF8.GetBytes(plaintext));
            return Convert.ToBase64String(encrpytedData);
        }
        public static void Run()
        {
            string plainText = "Hello world!";
            Console.WriteLine("Plain Text: " + plainText);

            string publicKeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public_key.txt");

            string publicKey = File.ReadAllText(publicKeyFilePath);
            Console.WriteLine("\nPublic Key:");
            Console.WriteLine(publicKey);

            Encrypt_RSA encryptRsa = new Encrypt_RSA(publicKey);
            string encryptedText = encryptRsa.Encrypt(plainText);

            string encryptedTextFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encrypted.txt");
            File.WriteAllText(encryptedTextFilePath, encryptedText);

            Console.WriteLine("\nEncrypted Text: " + encryptedText);
        }
    }
}
