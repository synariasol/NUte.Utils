using System;
using Machine.Specifications;
using NUte.Utils.Enum;

namespace NUte.Utils.Tests.Enum
{
  public sealed class EnumDataAttributeFixture
  {
    public sealed class Constructor
    {
      [Subject(typeof(EnumDataAttribute), "Constructor")]
      public sealed class when_invoked_with_a_null_data_value
      {
        private static Exception _exception;

        private Because of = () => _exception = Catch.Exception(() => new EnumDataAttribute(null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumDataAttribute), "Constructor")]
      public sealed class when_invoked_with_a_whitespace_data_value
      {
        private static Exception _exception;

        private Because of = () => _exception = Catch.Exception(() => new EnumDataAttribute("     "));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => ValidationMessages.VerifyMessage(_exception.Message, ValidationMessages.WhiteSpaceMessage, "data");
      }

      [Subject(typeof(EnumDataAttribute), "Constructor")]
      public sealed class when_invoked_with_a_data_value
      {
        private static EnumDataAttribute _attribute;

        private Because of = () => _attribute = new EnumDataAttribute("Test Data");
        private It should_set_Data = () => _attribute.Data.ShouldEqual("Test Data");
      }
    }
  }
}