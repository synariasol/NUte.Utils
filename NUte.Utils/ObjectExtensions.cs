using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NUte.Utils
{
  public static class ObjectExtensions
  {
    public static bool IsEnumerable(this object value, bool allowString = false)
    {
      if (value == null)
      {
        return false;
      }

      var type = value.GetType();

      return type.IsEnumerable(allowString);
    }

    public static IDictionary<string, object> ToDictionary(this object value)
    {
      if (value == null)
      {
        return new Dictionary<string, object>();
      }

      return (from PropertyDescriptor property in TypeDescriptor.GetProperties(value)
              select property).ToDictionary(property => property.Name, property => property.GetValue(value));
    }
  }
}