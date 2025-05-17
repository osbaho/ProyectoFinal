using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Utils
{
    public static class SecureSaveLoad
    {
        // Deben ser exactamente 16 bytes (128 bits) para AES-128, 24 para AES-192, 32 para AES-256
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890ABCDEF"); // 16 bytes
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("FEDCBA0987654321");  // 16 bytes

        public static void SaveEncrypted<T>(string path, T data)
        {
            string json = JsonUtility.ToJson(data);
            byte[] encrypted = EncryptStringToBytes_Aes(json, Key, IV);
            File.WriteAllBytes(path, encrypted);
        }

        public static T LoadEncrypted<T>(string path)
        {
            if (!File.Exists(path))
                return default;

            byte[] encrypted = File.ReadAllBytes(path);
            string json = DecryptStringFromBytes_Aes(encrypted, Key, IV);
            return JsonUtility.FromJson<T>(json);
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                    swEncrypt.Close();
                    return msEncrypt.ToArray();
                }
            }
        }

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
