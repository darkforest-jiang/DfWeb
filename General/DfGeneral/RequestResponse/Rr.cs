using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DfGeneral.RequestResponse;

/// <summary>
/// 响应
/// </summary>
public class Rr<T>
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess { get; set; } = true;

    /// <summary>
    /// 返回消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 返回结果
    /// </summary>
    public T? Result { get; set; }
}
