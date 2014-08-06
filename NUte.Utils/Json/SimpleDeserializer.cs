using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NUte.Utils.Json
{
  public class SimpleDeserializer
    : JsonReaderBase
  {
    public TObject Deserialize<TObject>(string json)
    {
      var jsonObject = Read(json);
      var objectType = typeof (TObject);
      var instance = ResolveObject(objectType, jsonObject);

      return (TObject)instance;
    }

    private static void BindObject(object instance, JsonObject jsonObject)
    {
      if (instance != null && jsonObject != null)
      {
        foreach (var jsonProperty in jsonObject.Properties)
        {
          BindProperty(instance, jsonProperty);
        }
      }
    }

    private static void BindProperty(object instance, JsonProperty jsonProperty)
    {
      var objectType = instance.GetType();
      var propertyInfo = (from property in objectType.GetProperties()
                          where property.CanWrite && property.Name.IsEqual(jsonProperty.Name)
                          select property).SingleOrDefault();

      if (propertyInfo != null)
      {
        object value = null;

        if (jsonProperty.Value is JsonObject)
        {
          value = ResolveObject(propertyInfo.PropertyType, jsonProperty.Value as JsonObject);
        }
        else if (TypeExtensions.IsEnumerable(propertyInfo.PropertyType))
        {
          var array = jsonProperty.AsEnumerable();
          value = ResolveArray(propertyInfo.PropertyType, array);
        }
        else if (jsonProperty.Value != null)
        {
          value = ResolveValue(propertyInfo.PropertyType, jsonProperty.Value);
        }

        propertyInfo.SetValue(instance, value);
      }
    }

    private static object ResolveObject(Type type, JsonObject jsonObject)
    {
      object instance = null;

      if (jsonObject != null)
      {
        instance = Activator.CreateInstance(type);
        BindObject(instance, jsonObject);
      }

      return instance;
    }

    private static object ResolveArray(Type type, IEnumerable items)
    {
      var genericTypes = type.GetGenericTypes();
      var arrayType = genericTypes.Single();
      var listType = typeof (List<>).MakeGenericType(new[] {arrayType});
      var add = listType.GetMethod("Add");
      var list = Activator.CreateInstance(listType);

      foreach (var item in items)
      {
        object itemValue;

        if (item is JsonObject)
        {
          itemValue = ResolveObject(arrayType, item as JsonObject);
        }
        else if (item.IsEnumerable())
        {
          itemValue = ResolveArray(arrayType, item as IEnumerable);
        }
        else
        {
          itemValue = ResolveValue(arrayType, item);
        }

        add.Invoke(list, new[] {itemValue});
      }

      return list;
    }

    private static object ResolveValue(Type type, object value)
    {
      var nullableType = type.GetNullableType();

      if (nullableType != null)
      {
        type = nullableType;
      }

      return Convert.ChangeType(value, type);
    }
  }
}