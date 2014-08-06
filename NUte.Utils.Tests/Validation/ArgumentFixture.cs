using System;
using System.Collections;
using System.Collections.Generic;
using Machine.Specifications;
using NUte.Utils.Validation;

namespace NUte.Utils.Tests.Validation
{
  public sealed class ArgumentFixture
  {
    public sealed class NotNullMethod
    {
      [Subject(typeof(Argument), "NotNull")]
      public sealed class when_invoked_with_null
        : ValidationFixtureBase<object>
      {
        private Establish context = () => Value = null;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNull(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotNull")]
      public sealed class when_invoked_with_an_object
        : ValidationFixtureBase<object>
      {
        private Establish context = () => Value = new object();
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNull(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class NotDefaultMethod
    {
      [Subject(typeof(Argument), "NotDefault")]
      public sealed class when_invoked_with_a_default_int
        : ValidationFixtureBase<int>
      {
        private Establish context = () => Value = default(int);
        private Because of = () => Exception = Catch.Exception(() => Argument.NotDefault(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotDefault")]
      public sealed class when_invoked_with_a_non_default_int
        : ValidationFixtureBase<int>
      {
        private Establish context = () => Value = 10;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotDefault(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class NotNullOrEmptyMethod_String
    {
      [Subject(typeof(Argument), "NotNullOrEmpty<string>")]
      public sealed class when_invoked_with_null
        : ValidationFixtureBase<string>
      {
        private Establish context = () => Value = null;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrEmpty(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotNullOrEmpty<string>")]
      public sealed class when_invoked_with_an_empty_string
        : ValidationFixtureBase<string>
      {
        private Establish context = () => Value = string.Empty;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrEmpty(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.EmptyMessage);
      }

      [Subject(typeof(Argument), "NotNullOrEmpty<string>")]
      public sealed class when_invoked_with_a_string
        : ValidationFixtureBase<string>
      {
        private Establish context = () => Value = "Test";
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrEmpty(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class NotNullOrWhiteSpaceMethod
    {
      [Subject(typeof(Argument), "NotNullOrWhiteSpace")]
      public sealed class when_invoked_with_null
        : ValidationFixtureBase<string>
      {
        private Establish context = () => Value = null;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrWhiteSpace(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotNullOrWhiteSpace")]
      public sealed class when_invoked_with_a_whitespace_string
        : ValidationFixtureBase<string>
      {
        private Establish context = () => Value = "   ";
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrWhiteSpace(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.WhiteSpaceMessage);
      }

      [Subject(typeof(Argument), "NotNullOrWhiteSpace")]
      public sealed class when_invoked_with_a_string
        : ValidationFixtureBase<string>
      {
        private Establish context = () => Value = "Test";
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrWhiteSpace(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class NotNullOrEmptyMethod_Enumerable
    {
      [Subject(typeof(Argument), "NotNullOrEmpty<IEnumerable>")]
      public sealed class when_invoked_with_null
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = null;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrEmpty(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotNullOrEmpty<IEnumerable>")]
      public sealed class when_invoked_with_an_empty_IEnumerable
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = new string[0];
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrEmpty(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.EmptyMessage);
      }

      [Subject(typeof(Argument), "NotNullOrEmpty<IEnumerable>")]
      public sealed class when_invoked_with_an_IEnumerable
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = new[] { "Test1", "Test2" };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrEmpty(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class NotNullOrNullElementsMethod
    {
      [Subject(typeof(Argument), "NotNullOrNullElements")]
      public sealed class when_invoked_with_null
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = null;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrNullElements(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotNullOrNullElements")]
      public sealed class when_invoked_with_an_IEnumerable_containing_null_elements
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = new[] { "Test", null };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrNullElements(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.NullElementsMessage);
      }

      [Subject(typeof(Argument), "NotNullOrNullElements")]
      public sealed class when_invoked_with_an_IEnumerable_containing_non_null_elements
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = new[] { "Test1", "Test2" };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullOrNullElements(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class NotNullEmptyOrNullElementsMethod
    {
      [Subject(typeof(Argument), "NotNullEmptyOrNullElements")]
      public sealed class when_invoked_with_null
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = null;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullElements(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotNullEmptyOrNullElements")]
      public sealed class when_invoked_with_an_empty_IEnumerable
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = new string[0];
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullElements(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.EmptyMessage);
      }

      [Subject(typeof(Argument), "NotNullEmptyOrNullElements")]
      public sealed class when_invoked_with_an_IEnumerable_containing_null_elements
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = new[] { "Test", null };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullElements(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.NullElementsMessage);
      }

      [Subject(typeof(Argument), "NotNullEmptyOrNullElements")]
      public sealed class when_invoked_with_an_IEnumerable_containing_non_null_elements
        : ValidationFixtureBase<IEnumerable>
      {
        private Establish context = () => Value = new[] { "Test1", "Test2" };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullElements(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class NotNullEmptyOrNullWhiteSpaceElementsMethod
    {
      [Subject(typeof(Argument), "NotNullEmptyOrNullWhiteSpaceElements")]
      public sealed class when_invoked_with_null
        : ValidationFixtureBase<IEnumerable<string>>
      {
        private Establish context = () => Value = null;
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullWhiteSpaceElements(() => Value));
        private It should_throw_an_ArgumentNullException = () => Exception.ShouldBeOfType<ArgumentNullException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentNullExceptionParamName();
      }

      [Subject(typeof(Argument), "NotNullEmptyOrNullWhiteSpaceElements")]
      public sealed class when_invoked_with_an_empty_IEnumerable
        : ValidationFixtureBase<IEnumerable<string>>
      {
        private Establish context = () => Value = new List<string>();
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullWhiteSpaceElements(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.EmptyMessage);
      }

      [Subject(typeof(Argument), "NotNullEmptyOrNullWhiteSpaceElements")]
      public sealed class when_invoked_with_an_IEnumerable_containing_null_elements
        : ValidationFixtureBase<IEnumerable<string>>
      {
        private Establish context = () => Value = new List<string> { "Test", null };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullWhiteSpaceElements(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.NullElementsMessage);
      }

      [Subject(typeof(Argument), "NotNullEmptyOrNullWhiteSpaceElements")]
      public sealed class when_invoked_with_an_IEnumerable_containing_whitespace_elements
        : ValidationFixtureBase<IEnumerable<string>>
      {
        private Establish context = () => Value = new List<string> { "Test", "     " };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullWhiteSpaceElements(() => Value));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_parameter_name = () => VerifyArgumentExceptionParamName();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.WhiteSpaceElementsMessage);
      }

      [Subject(typeof(Argument), "NotNullEmptyOrNullWhiteSpaceElements")]
      public sealed class when_invoked_with_an_IEnumerable_containing_non_null_and_non_whitespace_elements
        : ValidationFixtureBase<IEnumerable<string>>
      {
        private Establish context = () => Value = new[] { "Test1", "Test2" };
        private Because of = () => Exception = Catch.Exception(() => Argument.NotNullEmptyOrNullWhiteSpaceElements(() => Value));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class VerifyMethod
    {
      [Subject(typeof(Argument), "Verify")]
      public sealed class when_invoked_with_a_false_condition
        : ValidationFixtureBase<Func<bool>>
      {
        private Establish context = () => Value = () => false;
        private Because of = () => Exception = Catch.Exception(() => Argument.Verify(Value, "Test message"));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => VerifyExceptionMessage("Test message");
      }

      [Subject(typeof(Argument), "Verify")]
      public sealed class when_invoked_with_a_true_condition
        : ValidationFixtureBase<Func<bool>>
      {
        private Establish context = () => Value = () => true;
        private Because of = () => Exception = Catch.Exception(() => Argument.Verify(Value, "Test message"));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }

    public sealed class VerifyMethod_WithArgumentName
    {
      [Subject(typeof(Argument), "Verify_WithArgumentName")]
      public sealed class when_invoked_with_a_false_condition
        : ValidationFixtureBase<Func<bool>>
      {
        private Establish context = () => Value = () => false;
        private Because of = () => Exception = Catch.Exception(() => Argument.Verify(Value, "Value", "Test message"));
        private It should_throw_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => VerifyExceptionMessage(ValidationMessages.MessageFormat, "Test Message");
      }

      [Subject(typeof(Argument), "Verify_WithArgumentName")]
      public sealed class when_invoked_with_a_true_condition
        : ValidationFixtureBase<Func<bool>>
      {
        private Establish context = () => Value = () => true;
        private Because of = () => Exception = Catch.Exception(() => Argument.Verify(Value, "Value", "Test message"));
        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();
      }
    }
  }
}