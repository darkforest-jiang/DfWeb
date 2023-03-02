using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DfHelper.EncryptHelper;

public class MD5EncryptHelper
{
    public static string Encrypt(string oriStr)
    {
        MD5 md5 = MD5.Create();
        byte[] inputByte = Encoding.UTF8.GetBytes(oriStr);
        byte[] md5Buf = md5.ComputeHash(inputByte);
        return Convert.ToBase64String(md5Buf);
    }
}
