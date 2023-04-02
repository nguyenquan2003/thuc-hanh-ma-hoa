#include <iostream>
#include <string>
#include <cmath>
#include <cstdlib>
#include <ctime>
using namespace std;
//hàm kiểm tra một số có phải là số nguyên tố hay không
bool isPrime(int n)
{
    if (n <= 1)
        return false;
    if (n <= 3)
        return true;
    if (n % 2 == 0 || n % 3 == 0)
        return false;
    for (int i = 5; i * i <= n; i = i + 6)
        if (n % i == 0 || n % (i + 2) == 0)
            return false;
    return true;
}
//hàm tìm ước chung lớn nhất của 2 số
int gcd(int a, int b)
{
    if (a == 0)
        return b;
    return gcd(b % a, a);
}
//hàm tìm nghịch đảo nhân mô đun của hai số
int modInverse(int a, int m)
{
    a = a % m;
    for (int x = 1; x < m; x++)
        if ((a * x) % m == 1)
            return x;
}
//hàm tìm lũy thừa của một số
int power(int x, unsigned int y, int p)
{
    int res = 1;
    x = x % p;
    while (y > 0)
    {
        if (y & 1)
            res = (res * x) % p;
        y = y >> 1;
        x = (x * x) % p;
    }
    return res;
}
//hàm mã hóa tin nhắn
string encrypt(string msg, int q, int h, int g)
{
    int x = rand() % q;
    int y = power(g, x, q);
    int k = power(h, x, q);
    string cipher = "";
    for (int i = 0; i < msg.length(); i++)
    {
        cipher = cipher + (char)(msg[i] * k);
    }
    return cipher;
}
//hàm giải mã tin nhắn
string decrypt(string cipher, int p, int q, int h, int y)
{
    int k = power(y, p - 1 - q, p);
    string msg = "";
    for (int i = 0; i < cipher.length(); i++)
    {
        msg = msg + (char)(cipher[i] * k);
    }
    return msg;
}
int main()
{
    srand(time(0)); //ham random sau moi lan chay chuong trinh
    int p = 23;
    int q = 11;
    int n = p * q;
    int phi = (p - 1) * (q - 1);
    int e = 2;
    while (e < phi)
    {
        if (gcd(e, phi) == 1)
            break;
        else
            e++;
    }
    int d = modInverse(e, phi);
    int h = power(2, e, n);
    int g = power(2, d, n);
    string msg = "Hello World";
    string cipher = encrypt(msg, q, h, g);
    string decrypted_msg = decrypt(cipher, p, q, h, g);
    cout << "Original Message: " << msg << endl; //tin nhắn ban đầu
    cout << "Encrypted Message: " << cipher << endl; //tin nhắn được mã hóa
    cout << "Decrypted Message: " << decrypted_msg << endl; //tin nhan duoc giai ma
    return 0;
}
