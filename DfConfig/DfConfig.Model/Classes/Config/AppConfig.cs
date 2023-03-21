using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.Classes.Config;

/// <summary>
/// 应用配置
/// </summary>
[DataContract]
public class AppConfig
{
    [DataMember]
    private string _key;
    /// <summary>
    /// Key
    /// </summary>
    [DataMember]
    public string Key
    {
        get
        {
            return $"{(string.IsNullOrWhiteSpace(NsKey) ? "" : $"{NsKey}:")}{_key}";
        }
        set
        {
            _key = value;
        }
    }

    /// <summary>
    /// 获取命名空间key和配置key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static (string? nsKey, string realKey) GetRealKey(string key)
    {
        var index = key.IndexOf(":");
        if (index >= 0)
        {
            return (key.Substring(0, index), key.Substring(index + 1, key.Length - index - 1));
        }
        else
        {
            return (null, key);
        }
    }

    /// <summary>
    /// Value
    /// </summary>
    [DataMember]
    public string Value { get; set; } = null!;

    /// <summary>
    /// 命名空间
    /// </summary>
    [DataMember]
    public string? NsKey { get; set; }
}
