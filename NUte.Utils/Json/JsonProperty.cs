namespace NUte.Utils.Json
{
  public sealed class JsonProperty
  {
    internal JsonProperty(string name, object value)
    {
      Name = name;
      Value = value;
    }

    public string Name { get; private set; }
    public object Value { get; private set; }
  }
}