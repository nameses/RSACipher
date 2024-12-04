using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSACipher.Logic
{
    public class RSAEncryption
    {
        private RSACryptoServiceProvider _rsa;

        public RSAEncryption()
        {
            _rsa = new RSACryptoServiceProvider();
        }

        // Generate Public and Private Keys
        public string GenerateKeys(bool includePrivateKey = true)
        {
            return _rsa.ToXmlString(includePrivateKey);
        }

        // Load Keys
        public void LoadKeys(string xmlKey)
        {
            _rsa.FromXmlString(xmlKey);
        }

        // Encrypt Data
        public byte[] Encrypt(string plainText, string publicKey)
        {
            var rsaEncryptor = new RSACryptoServiceProvider();
            rsaEncryptor.FromXmlString(publicKey);

            byte[] dataToEncrypt = Encoding.Unicode.GetBytes(plainText);
            return rsaEncryptor.Encrypt(dataToEncrypt, false);
        }

        // Decrypt Data
        public string Decrypt(byte[] encryptedData)
        {
            byte[] decryptedData = _rsa.Decrypt(encryptedData, false);
            return Encoding.Unicode.GetString(decryptedData);
        }
    }
}
