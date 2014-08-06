using Machine.Specifications;

namespace NUte.Utils.Tests
{
  public static class ValidationMessages
  {
    public const string MessageFormat = "{0}\r\nParameter name: {{0}}";

    public static readonly string EmptyMessage = string.Format(MessageFormat, "The parameter value is empty.");
    public static readonly string WhiteSpaceMessage = string.Format(MessageFormat, "The parameter value is whitespace.");
    public static readonly string NullElementsMessage = string.Format(MessageFormat, "The parameter value contains at least one null element.");
    public static readonly string WhiteSpaceElementsMessage = string.Format(MessageFormat, "The parameter value contains at least one whitespace element.");

    public static void VerifyMessage(string message, string format, params object[] arguments)
    {
      if (format == null)
      {
        message.ShouldBeNull();
      }
      else
      {
        var formattedMessage = string.Format(format, arguments);

        message.ShouldBeEqualIgnoringCase(formattedMessage);
      }
    }
  }
}