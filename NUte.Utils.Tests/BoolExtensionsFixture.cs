using Machine.Specifications;

namespace NUte.Utils.Tests
{
  public sealed class BoolExtensionsFixture
  {
    public sealed class ToLowerStringMethod
    {
      private static string _result;

      [Subject(typeof(BoolExtensions), "ToLowerString")]
      public sealed class when_invoked_with_a_true_value
      {
        private Because of = () => _result = BoolExtensions.ToLowerString(true);
        private It should_return_a_lower_case_true_string = () => _result.ShouldEqual("true");
      }

      [Subject(typeof(BoolExtensions), "ToLowerString")]
      public sealed class when_invoked_with_a_false_value
      {
        private Because of = () => _result = BoolExtensions.ToLowerString(false);
        private It should_return_a_lower_case_false_string = () => _result.ShouldEqual("false");
      }
    }
  }
}