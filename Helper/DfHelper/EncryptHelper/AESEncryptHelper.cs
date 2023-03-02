using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DfHelper.EncryptHelper;

public class AESEncryptHelper
{
    private string _key;
    private string _iv;
    //默认的CipherMode.CBC     PaddingMode.PKCS7 模式
    //与SymmetryEncryptHelper的加密解密效果一样
    private CipherMode _cipherMode = CipherMode.CBC;
    private PaddingMode _paddingMode = PaddingMode.PKCS7;

    public AESEncryptHelper(string key, string iv)
    {
        _key = key;
        _iv = iv;
    }

    public AESEncryptHelper(string key, string iv, CipherMode cipherMode, PaddingMode paddingMode)
    {
        _key = key;
        _iv = iv;
        _cipherMode = cipherMode;
        _paddingMode = paddingMode;
    }

    public string Encrypt(string oriStr)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = GetLegalKey(aes);
            aes.IV = GetLegalIV(aes);
            aes.Mode = _cipherMode;
            aes.Padding = _paddingMode;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(oriStr);
                }
                byte[] bytes = msEncrypt.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
    }

    public string Decrypt(string secretStr)
    {
        try
        {
            using (AesCryptoServiceProvider aesCryptoService = new AesCryptoServiceProvider())
            {
                aesCryptoService.Key = GetLegalKey(aesCryptoService);
                aesCryptoService.IV = GetLegalIV(aesCryptoService);
                aesCryptoService.Mode = _cipherMode;
                aesCryptoService.Padding = _paddingMode;

                ICryptoTransform decryptor = aesCryptoService.CreateDecryptor(aesCryptoService.Key, aesCryptoService.IV);

                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(secretStr)))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                {
                    string result = streamReader.ReadToEnd();
                    return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(result));
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    #region 私有方法

    /// <summary>
    /// 获取初始密钥
    /// </summary>
    /// <returns></returns>
    private byte[] GetLegalKey(Aes aes)
    {
        string strTemp = _key;
        aes.GenerateKey();
        byte[] bytTemp = aes.Key;
        int KeyLength = bytTemp.Length;
        if (strTemp.Length > KeyLength)
            strTemp = strTemp.Substring(0, KeyLength);
        else if (strTemp.Length < KeyLength)
            strTemp = strTemp.PadRight(KeyLength, ' ');

        return ASCIIEncoding.UTF8.GetBytes(strTemp);
    }

    /// <summary>    
    /// 获得初始向量IV    
    /// </summary>    
    /// <returns>初试向量IV</returns>    
    private byte[] GetLegalIV(Aes aes)
    {
        string strTemp = _iv;
        aes.GenerateIV();
        byte[] bytTemp = aes.IV;
        int IVLength = bytTemp.Length;
        if (strTemp.Length > IVLength)
            strTemp = strTemp.Substring(0, IVLength);
        else if (strTemp.Length < IVLength)
            strTemp = strTemp.PadRight(IVLength, ' ');

        return ASCIIEncoding.UTF8.GetBytes(strTemp);
    }

    #endregion

}
