using System;
using NUte.Utils.Validation;

namespace NUte.Utils.Enum
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public sealed class EnumDataAttribute
    : Attribute
  {
    public EnumDataAttribute(string data)
    {
      Argument.NotNullOrWhiteSpace(() => data);

      Data = data;
    }

    public string Data { get; private set; }
  }
}