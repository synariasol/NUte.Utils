namespace NUte.Utils
{
  public static class BoolExtensions
  {
    public static string ToLowerString(this bool value)
    {
      var result = value.ToString();

      return result.ToLower();
    }
  }
}