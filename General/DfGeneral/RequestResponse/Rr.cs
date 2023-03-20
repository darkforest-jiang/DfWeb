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
    /// 是否成功 1-成功
    /// </summary>
    public int Code { get; set; } = 1;

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess => Code == 1;

    /// <summary>
    /// 返回消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// 返回结果
    /// </summary>
    public T? Result { get; set; }
}
