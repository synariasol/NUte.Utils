using System;
using System.Linq;
using Machine.Specifications;

namespace NUte.Utils.Tests.Validation
{
  public abstract class ValidationFixtureBase<TValue>
  {
    protected static TValue Value;
    protected static Exception Exception;

    protected static void VerifyExceptionMessage(string message, params object[] arguments)
    {
      Exception.ShouldNotBeNull();

      if (arguments.Any())
      {
        message = string.Format(message, arguments);
      }

      ValidationMessages.VerifyMessage(Exception.Message, message, "Value");
    }

    protected static void VerifyArgumentNullExceptionParamName()
    {
      var exception = Exception as ArgumentNullException;

      VerifyExceptionParamName(exception, e => exception.ParamName);
    }

    protected static void VerifyArgumentExceptionParamName()
    {
      var exception = Exception as ArgumentException;

      VerifyExceptionParamName(exception, e => exception.ParamName);
    }

    private static void VerifyExceptionParamName<TException>(TException exception, Func<TException, string> name)
      where TException : Exception
    {
      // Check the exception cast
      exception.ShouldNotBeNull();

      // Check the exception paramName value
      var paramName = name.Invoke(exception);

      paramName.ShouldBeEqualIgnoringCase("Value");
    }
  }
}