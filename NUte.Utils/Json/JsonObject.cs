using System.Collections.Generic;

namespace NUte.Utils.Json
{
  public sealed class JsonObject
  {
    private readonly IList<JsonProperty> _properties;

    internal JsonObject()
      : this(null)
    { }

    internal JsonObject(string name)
    {
      Name = name;
      _properties = new List<JsonProperty>();
    }

    public string Name { get; private set; }

    public IEnumerable<JsonProperty> Properties
    {
      get { return _properties; }
    }

    internal void AddProperty(string name, object value)
    {
      var property = new JsonProperty(name, value);
      AddProperty(property);
    }

    internal void AddProperty(JsonProperty property)
    {
      _properties.Add(property);
    }
  }
}