using System;

namespace NUte.Utils.Json
{
  public sealed class JsonException
    : Exception
  {
    internal JsonException(string message, Exception innerException = null)
      : base(message, innerException)
    { }
  }
}