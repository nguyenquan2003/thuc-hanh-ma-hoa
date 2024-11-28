using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Cryptography
{
    public class RSA1
    {
        public BigInteger p;
        public BigInteger q;
        public BigInteger N;
        public BigInteger phi;
        public BigInteger e;
        public BigInteger d;
        private static Random random = new Random();

        public RSA1(int keySize)
        {
            GenerateKeys(keySize);
        }
        //sinh cap khoa RSA
        public void GenerateKeys(int bitSize)
        {
            List<BigInteger> primes = GeneratePrimes(bitSize);
            p = primes[0];
            q = primes[1];
            N = p * q;
            phi = (p - 1) * (q - 1);
            e = FindE(phi);
            d = CalculateD(e, phi);
        }
        //
        public BigInteger[] Encrypt(string plainText)
        {
            char[] chars = plainText.ToCharArray();
            BigInteger[] encryptedValues = new BigInteger[chars.Length];

            for (int i = 0; i < chars.Length; i++)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(chars[i].ToString());

                BigInteger encryptedValue = BigInteger.ModPow(new BigInteger(asciiBytes), e, N);
                encryptedValues[i] = encryptedValue;
            }
            return encryptedValues;
        }
        //
        public string Decrypt(BigInteger[] encryptedValues)
        {
            byte[] bytes = new byte[encryptedValues.Length];
            for (int i = 0; i < encryptedValues.Length; i++)
            {
                BigInteger decryptedValue = BigInteger.ModPow(encryptedValues[i], d, N);
                byte decryptedByte = decryptedValue.ToByteArray()[0];
                bytes[i]= decryptedByte;
            }
            string message = Encoding.ASCII.GetString(bytes);
            return message;
        }
        //
        private List<BigInteger> GeneratePrimes(int bitSize)
        {
            List<BigInteger> primes = new List<BigInteger>();
            while (primes.Count < 2)
            {
                BigInteger prime = GeneratePrime(bitSize);
                if (!primes.Contains(prime))
                    primes.Add(prime);
            }
            return primes;
        }
        //
        private BigInteger GeneratePrime(int bitSize)
        {
            BigInteger prime;
            do
            {
                byte[] buffer = new byte[bitSize / 8];
                random.NextBytes(buffer);
                prime = new BigInteger(buffer);
                prime |= BigInteger.One;
            } while (!IsPrime(prime));
            return prime;
        }
        //
        private bool IsPrime(BigInteger number)
        {
            if (number <= BigInteger.One)
                return false;
            if (number == 2 || number == 3)
                return true;
            if (number % 2 == 0 || number % 3 == 0)
                return false;
            BigInteger sqrt = Sqrt(number);
            for (BigInteger i = 5; i <= sqrt; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }
            return true;
        }
        //
        private BigInteger Sqrt(BigInteger number)
        {
            if (number == BigInteger.Zero)
                return BigInteger.Zero;

            BigInteger sqrt = number;
            BigInteger prev;
            do
            {
                prev = sqrt;
                sqrt = (prev + number / prev) / 2;
            } while (sqrt < prev);

            return prev;
        }
        //
        private BigInteger FindE(BigInteger phi)
        {
            BigInteger e = 5;
            while (e < phi)
            {
                if (IsCoprime(e, phi))
                    return e;
                e++;
            }
            return BigInteger.Zero;
        }
        //
        private bool IsCoprime(BigInteger a, BigInteger b)
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(a, b);
            return gcd == BigInteger.One;
        }
        //
        private BigInteger CalculateD(BigInteger e, BigInteger phi)
        {
            BigInteger d = ExtendedEuclideanAlgorithm(e, phi);
            d = (d % phi + phi) % phi;
            return d;
        }
        //
        private BigInteger ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
        {
            BigInteger oldR = a;
            BigInteger r = b;
            BigInteger oldS = BigInteger.One;
            BigInteger s = BigInteger.Zero;
            BigInteger oldT = BigInteger.Zero;
            BigInteger t = BigInteger.One;

            while (r != BigInteger.Zero)
            {
                BigInteger quotient = oldR / r;
                BigInteger temp = oldR;
                oldR = r;
                r = temp - quotient * r;

                temp = oldS;
                oldS = s;
                s = temp - quotient * s;

                temp = oldT;
                oldT = t;
                t = temp - quotient * t;
            }
            return oldS;
        }
    }
}
