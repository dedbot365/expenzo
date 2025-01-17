using System;
using System.Security.Cryptography;
using System.Text;

namespace expenzo.Services;
public class EncryptionService
{
    private static byte[] key = Array.Empty<byte>();
    private static byte[] iv = Array.Empty<byte>();
    private static readonly string keyPath;
    private static readonly string ivPath;

    static EncryptionService()
    {
        // Define paths for key and IV
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var folderPath = Path.Combine(appDataPath, "expenzo");
        Directory.CreateDirectory(folderPath);
        keyPath = Path.Combine(folderPath, "key.bin");
        ivPath = Path.Combine(folderPath, "iv.bin");

        // Load key and IV from storage
        LoadKeyAndIV();
    }

    private static void LoadKeyAndIV()
    {
        if (File.Exists(keyPath) && File.Exists(ivPath))
        {
            key = File.ReadAllBytes(keyPath);
            iv = File.ReadAllBytes(ivPath);
        }
        else
        {
            using (var aes = Aes.Create())
            {
                key = aes.Key;
                iv = aes.IV;
                File.WriteAllBytes(keyPath, key);
                File.WriteAllBytes(ivPath, iv);
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