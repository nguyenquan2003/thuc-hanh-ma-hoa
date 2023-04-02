#include <iostream>
#include <string>
#include <algorithm>
#include <cmath>
using namespace std;
string encrypt(string plaintext, int a, int b)
{
    string ciphertext = "";
    for (int i = 0; i < plaintext.length(); i++)
    {
        if (plaintext[i] != ' ')
        {
            ciphertext += (char)((((a * (plaintext[i] - 'A')) + b) % 26) + 'A');
        }
        else
        {
            ciphertext += plaintext[i];
        }
    }
    return ciphertext;
}
int main()
{
    string plaintext = "CON CAO NAU NHANH CHONG NHAY QUA CON CHO LUOI";
    string ciphertext = encrypt(plaintext, 5, 8);
    cout << "Plaintext : " << plaintext << endl;
    cout << "Ciphertext : " << ciphertext << endl;
    return 0;
}
