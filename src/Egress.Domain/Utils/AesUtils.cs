using System.Security.Cryptography;

namespace Egress.Domain.Utils;

public static class AesUtils
{
    public static string Encrypt(string plainText, string key, string iv)
    {
        using var aesAlg = Aes.Create();
        
        aesAlg.Key = Convert.FromBase64String(key);
        aesAlg.IV = Convert.FromBase64String(iv);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
        
        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string Decrypt(string cipherText, string key, string iv)
    {
        using var aesAlg = Aes.Create();
        
        aesAlg.Key = Convert.FromBase64String(key);
        aesAlg.IV = Convert.FromBase64String(iv);

        var cipherTextBytes = Convert.FromBase64String(cipherText);

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherTextBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        
        return srDecrypt.ReadToEnd();
    }
    
    public static (string, string) CreateAes()
    {
        var aes = Aes.Create();
        
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Padding = PaddingMode.PKCS7;
        
        return (Convert.ToBase64String(aes.Key), Convert.ToBase64String(aes.IV));
    }
}