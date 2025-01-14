using System;
using System.Security.Cryptography;
using System.Text;

namespace expenzo.Services;
public class EncryptionService
{
    private static byte[] key = Array.Empty<byte>();
    private static byte[] iv = Array.Empty<byte>();

    static EncryptionService()
    {
        // Load key and IV from storage
        LoadKeyAndIV();
    }

    private static void LoadKeyAndIV()
    {
        
        if (File.Exists("key.bin") && File.Exists("iv.bin"))
        {
            key = File.ReadAllBytes("key.bin");
            iv = File.ReadAllBytes("iv.bin");
        }
        else
        {
            using (var aes = Aes.Create())
            {
                key = aes.Key;
                iv = aes.IV;
                File.WriteAllBytes("key.bin", key);
                File.WriteAllBytes("iv.bin", iv);
            }
        }
    }

    public string Encrypt(string plainText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}