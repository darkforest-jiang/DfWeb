using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfHelper;

/// <summary>
/// 枚举Helper
/// </summary>
public class EnumHelper
{
    /// <summary>
    /// 枚举名称 转 枚举
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="enumStr">枚举名称</param>
    /// <returns>返回枚举</returns>
    public static T ConvertToEnum<T>(string enumStr) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumStr);
    }

    /// <summary>
    /// 枚举值 转  枚举
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="enumValue">枚举值</param>
    /// <returns>返回枚举</returns>
    public static T ConvertToEnum<T>(int enumValue) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumValue.ToString());
    }

    /// <summary>
    /// 获取枚举名称
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="obj">枚举</param>
    /// <returns>返回枚举名称</returns>
    public static string GetEnumName<T>(object obj) where T : Enum
    {
        return Enum.GetName(typeof(T), obj);
    }

    /// <summary>
    /// 获取枚举List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetEnumList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).OfType<T>().ToList();
    }
}
