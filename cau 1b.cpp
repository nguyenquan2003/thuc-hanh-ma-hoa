#include <iostream>
#include <string>
#include <algorithm>
#include <cmath>
#include <cctype>
using namespace std;
string encrypt(string plaintext, int a, int b)
{
	string ciphertext = "";
	for (int i = 0; i < plaintext.length(); i++)
	{
		if (isalpha(plaintext[i]))
		{
			if (isupper(plaintext[i]))
			{
				ciphertext += (char)((((a * (plaintext[i] - 'A')) + b) % 26) + 'A');
			}
			else
			{
				ciphertext += (char)((((a * (plaintext[i] - 'a')) + b) % 26) + 'a');
			}
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
    string plaintext = "CON CAO NAU NHANH CHONG nhay qua CON CHO luoi bieng";
    int a = 5, b = 8;
    cout << "Plaintext : " << plaintext << endl;
    string ciphertext = encrypt(plaintext, a, b);
    cout << "Ciphertext : " << ciphertext << endl;
    return 0;
}
