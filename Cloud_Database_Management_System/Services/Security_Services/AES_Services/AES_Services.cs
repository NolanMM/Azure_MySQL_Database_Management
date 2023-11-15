using System.Security.Cryptography;
using System.Text;

namespace Cloud_Database_Management_System.Services.Security_Services.AES_Services
{
    public class AES_Services
    {
        private const int KeySize = 128;

        public static string Encrypt(string password, string key)
        {
            using AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider();
            aesAlg.KeySize = KeySize;
            aesAlg.Key = Encoding.UTF8.GetBytes(key);

            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using StreamWriter swEncrypt = new StreamWriter(csEncrypt);

            swEncrypt.Write(password);

            return Convert.ToBase64String(aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray());
        }

        public static string Decrypt(string password, string key)
        {
            using AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider();
            aesAlg.KeySize = KeySize;
            aesAlg.Key = Encoding.UTF8.GetBytes(key);

            byte[] iv = Convert.FromBase64String(password).Take(16).ToArray();
            byte[] cipherBytes = Convert.FromBase64String(password).Skip(16).ToArray();

            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(cipherBytes);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
