using System;

namespace SUG.Essentials
{
    /// <summary>
    /// 标记需要由 Essentials 自动注入的字段。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EInjectAttribute : Attribute
    {
    }
}