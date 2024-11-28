using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Cryptography;

namespace RASDS 
{
    class Program
    {
        static void Main(string[] args)
        {
            string plaintext = "hello";
            int bitSize = 16;

            RSA1 rsa = new RSA1(bitSize);

            Console.WriteLine("p: " + rsa.p);
            Console.WriteLine("q: " + rsa.q);
            Console.WriteLine("N: " + rsa.N);
            Console.WriteLine("phi: " + rsa.phi);
            Console.WriteLine("e: " + rsa.e);
            Console.WriteLine("d: " + rsa.d);
            Console.WriteLine("PublicKey (e,N): " + rsa.e + ", " + rsa.N);
            Console.WriteLine("PrivateKey (d,N): " + rsa.d + ", " + rsa.N);

            Console.WriteLine("Plainttext: " + plaintext);

            BigInteger[] encryptedValues = rsa.Encrypt(plaintext);
            Console.Write("Encrypted values: ");
            foreach (BigInteger encrptedValue in encryptedValues)
            {
                Console.Write(encrptedValue + " ");
            }

            string decryptedText = rsa.Decrypt(encryptedValues);
            Console.WriteLine("\n Decrypted text: " + decryptedText);

            Console.ReadLine();
        }
    }
}
