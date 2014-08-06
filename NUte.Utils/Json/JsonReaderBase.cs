using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NUte.Utils.Json
{
  public abstract class JsonReaderBase
  {
    protected JsonObject Read(string json)
    {
      return Read(json, Encoding.UTF8);
    }

    protected JsonObject Read(string json, Encoding encoding)
    {
      var xml = ConvertToXml(json, encoding);

      if (xml == null || xml.Root == null)
      {
        return null;
      }

      // Create an xml reader and walk the JSON data
      var jsonObject = new JsonObject();
      var reader = xml.Root.CreateReader();

      if (reader.Read())
      {
        Read(reader, jsonObject);
      }

      // Return the root object in the document
      return jsonObject;
    }

    protected virtual void OnObjectParsed(JsonObject jsonObject)
    { }

    protected virtual void OnPropertyParsed(JsonProperty jsonProperty)
    { }

    private void Read(XmlReader reader, JsonObject jsonObject)
    {
      while (reader.Read())
      {
        // Ensure each read starts from an Element node type
        if (reader.NodeType != XmlNodeType.Element)
        {
          continue;
        }

        Parse(reader, jsonObject);
      }
    }

    private void Parse(XmlReader reader, JsonObject jsonObject, string containerName = null)
    {
      var name = reader.LocalName;
      var propertyName = containerName ?? name;
      var depth = reader.Depth;
      var type = GetTypeValue(reader);

      switch (type)
      {
        case "object":
          var objectValue = ParseObject(reader, name, depth, containerName);
          jsonObject.AddProperty(propertyName, objectValue);
          break;

        case "array":
          var arrayValue = ParseArray(reader, name, depth, containerName);
          jsonObject.AddProperty(propertyName, arrayValue);
          break;

        default:
          var value = GetPropertyValue(reader, propertyName);
          var jsonProperty = new JsonProperty(propertyName, value);

          // Inform the implementing object that a property has been parsed
          OnPropertyParsed(jsonProperty);
          jsonObject.AddProperty(jsonProperty);
          break;
      }
    }

    private object ParseObject(XmlReader reader, string name, int depth, string overrideName = null)
    {
      var jsonObject = new JsonObject(overrideName ?? name);

      // Loop all the nodes inside the current until we hit the associated end node
      while (reader.Read() && !IsContainerEndNode(reader, name, depth))
      {
        if (reader.NodeType == XmlNodeType.Element)
        {
          Parse(reader, jsonObject);
        }
      }

      // Inform the implementing object that an object has been parsed
      OnObjectParsed(jsonObject);

      return jsonObject;
    }

    private object ParseArray(XmlReader reader, string name, int depth, string overrideName = null)
    {
      var objects = new List<object>();

      // Loop all the nodes inside the current until we hit the associated end node
      while (reader.Read() && !IsContainerEndNode(reader, name, depth))
      {
        if (reader.NodeType != XmlNodeType.Element)
        {
          continue;
        }

        // Parse each array item as a separate object
        var jsonObject = new JsonObject(name);
        Parse(reader, jsonObject, overrideName ?? name);

        // Then extract the return property values and add them to the array
        var values = jsonObject.Properties.Select(property => property.Value);
        objects.AddRange(values);
      }

      return objects.AsEnumerable();
    }

    private static XDocument ConvertToXml(string json, Encoding encoding)
    {
      if (!string.IsNullOrWhiteSpace(json))
      {
        var data = encoding.GetBytes(json);
        var stream = new MemoryStream(data);
        var quotas = new XmlDictionaryReaderQuotas();
        
        try
        {
          var reader = JsonReaderWriterFactory.CreateJsonReader(stream, quotas);
          return XDocument.Load(reader);
        }
        catch (Exception ex)
        {
          throw new JsonException("An error occurred while reading the provided JSON.", ex);
        }
      }

      return null;
    }

    private static string GetTypeValue(XmlReader reader)
    {
      return reader.MoveToAttribute("type") 
        ? reader.Value 
        : null;
    }

    private static object GetPropertyValue(XmlReader reader, string name)
    {
      if (reader.Read())
      {
        // If we have a text value then just return it
        if (reader.NodeType == XmlNodeType.Text)
        {
          return reader.Value;
        }

        // Alternatively, a matching end node means there is an empty value
        if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == name)
        {
          return null;
        }
      }

      // If we get this far, the generate XML is invalid
      // The null return is needed to satisfy the compiler (as a direct throw is not being made)
      ThrowFormattedException("An error occurred while reading the '{0}' property.", name);
      return null;
    }

    private static bool IsContainerEndNode(XmlReader reader, string name, int depth)
    {
      return reader.NodeType == XmlNodeType.EndElement &&
             reader.LocalName == name && reader.Depth == depth;
    }

    private static void ThrowFormattedException(string format, params object[] args)
    {
      var message = string.Format(format, args);
      throw new JsonException(message);
    }
  }
}