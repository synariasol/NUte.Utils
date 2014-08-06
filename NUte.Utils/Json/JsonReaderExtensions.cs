using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUte.Utils.Validation;

namespace NUte.Utils.Json
{
  public static class JsonReaderExtensions
  {
    public static JsonObject PropertyAsJsonObject(this JsonObject jsonObject, string name)
    {
      Argument.NotNull(() => jsonObject);
      Argument.NotNullOrWhiteSpace(() => name);

      var property = jsonObject.GetProperty(name);

      return property.AsJsonObject();
    }

    public static string PropertyAsString(this JsonObject jsonObject, string name)
    {
      Argument.NotNull(() => jsonObject);
      Argument.NotNullOrWhiteSpace(() => name);

      var property = jsonObject.GetProperty(name);

      return property.AsString();
    }

    public static IEnumerable<object> PropertyAsEnumerable(this JsonObject jsonObject, string name)
    {
      Argument.NotNull(() => jsonObject);
      Argument.NotNullOrWhiteSpace(() => name);

      var property = jsonObject.GetProperty(name);

      return property.AsEnumerable();
    }

    public static JsonObject AsJsonObject(this JsonProperty jsonProperty)
    {
      Argument.NotNull(() => jsonProperty);
     
      return jsonProperty.Value as JsonObject;
    }

    public static string AsString(this JsonProperty jsonProperty)
    {
      Argument.NotNull(() => jsonProperty);

      return jsonProperty.Value as string;
    }

    public static IEnumerable<object> AsEnumerable(this JsonProperty jsonProperty)
    {
      Argument.NotNull(() => jsonProperty);

      var value = jsonProperty.Value;

      if (value is IEnumerable)
      {
        var enumerable = value as IEnumerable;
        return enumerable.Cast<object>();
      }

      return new List<object>();
    }

    private static JsonProperty GetProperty(this JsonObject jsonObject, string name)
    {
      return (from property in jsonObject.Properties
              where string.Compare(property.Name, name, StringComparison.OrdinalIgnoreCase) == 0
              select property).SingleOrDefault();
    }
  }
}