using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Cryptography;
namespace bai2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the text to be encrypted: ");
            string plaintext = Console.ReadLine();

            int bitSize = 16;
            RSA1 rsa = new RSA1(bitSize);

            Console.WriteLine("PublicKey (e, N): " + rsa.e + ", " + rsa.N);
            Console.WriteLine("PublicKey (d, N): " + rsa.d + ", " + rsa.N);

            Console.WriteLine("plaintext: " + plaintext);

            BigInteger[] encryptedValues = rsa.Encrypt(plaintext);
            Console.Write("Encrypted values: ");
            foreach (BigInteger encryptedValue in encryptedValues)
            {
                Console.Write(encryptedValue + " ");
            }

            string decryptedText = rsa.Decrypt(encryptedValues);
            Console.WriteLine("\nDecrypted text: " + decryptedText);

            Console.ReadLine();
        }
    }
}
