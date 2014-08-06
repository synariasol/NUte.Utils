using System;
using System.Linq;
using System.Reflection;
using NUte.Utils.Validation;

namespace NUte.Utils.Enum
{
  public static class EnumExtensions
  {
    #region ToData

    public static string GetEnumData<TEnum>(this TEnum value)
        where TEnum : struct
    {
      Argument.NotNull(() => value);

      return GetEnumData(typeof(TEnum), value);
    }

    public static string GetEnumData<TEnum>(this System.Enum enumType, TEnum value)
        where TEnum : struct
    {
      Argument.NotNull(() => enumType);

      return GetEnumData(typeof(TEnum), value);
    }

    public static string GetEnumData(this System.Enum enumType, object value)
    {
      Argument.NotNull(() => enumType);

      var baseType = enumType.GetType();

      return GetEnumData(baseType, value);
    }

    public static string GetEnumData<TEnum>(this Type enumType, TEnum value)
        where TEnum : struct
    {
      Argument.NotNull(() => enumType);

      Argument.Verify(() => enumType == typeof(TEnum), "The specified enumeration type is invalid.");

      return GetEnumData(enumType, (object)value);
    }

    public static string GetEnumData(this Type enumType, object value)
    {
      Argument.NotNull(() => enumType);
      Argument.NotNull(() => value);

      Argument.Verify(() => enumType.IsEnum, "The specified type is not an enumeration.");

      var valueName = value.ToString();

      return (from member in enumType.GetMember(valueName)
              let attribute = member.GetCustomAttribute<EnumDataAttribute>()
              where attribute != null
              select attribute.Data).SingleOrDefault();
    }

    #endregion

    #region FromData

    public static TEnum? GetEnumFromData<TEnum>(this Type enumType, string data)
        where TEnum : struct
    {
      var value = GetEnumFromData(enumType, data);

      return value == null
        ? (TEnum?)null
        : (TEnum)value;
    }

    public static object GetEnumFromData(this Type enumType, string data)
    {
      Argument.NotNull(() => enumType);
      Argument.NotNull(() => data);

      Argument.Verify(() => enumType.IsEnum, "The specified type is not an enumeration.");

      return (from field in enumType.GetFields()
              let attribute = field.GetCustomAttribute<EnumDataAttribute>()
              where attribute != null && attribute.Data.IsEqual(data)
              select field.GetValue(null)).SingleOrDefault();
    }

    #endregion
  }
}