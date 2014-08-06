using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUte.Utils.Validation;

namespace NUte.Utils
{
  public static class TypeExtensions
  {
    public static bool IsEnumerable(this Type type, bool allowString = false)
    {
      Argument.NotNull(() => type);

      var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);

      if (!allowString && type == typeof(string))
      {
        isEnumerable = false;
      }

      return isEnumerable;
    }

    public static IEnumerable<Type> GetGenericTypes(this Type type)
    {
      Argument.NotNull(() => type);

      return type.IsGenericType 
        ? type.GetGenericArguments() 
        : Enumerable.Empty<Type>();
    }

    public static Type GetNullableType(this Type type)
    {
      Argument.NotNull(() => type);

      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
        var types = GetGenericTypes(type);
        return types.Single();
      }

      return null;
    }
  }
}