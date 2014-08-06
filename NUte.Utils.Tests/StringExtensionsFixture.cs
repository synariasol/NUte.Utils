using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace NUte.Utils.Tests
{
  public sealed class StringExtensionsFixture
  {
    public sealed class IsEqualMethod
    {
      private static bool _result;

      [Subject(typeof(StringExtensions), "IsEqual")]
      public sealed class when_invoked_with_a_null_source_and_a_null_value
      {
        private Because of = () => _result = StringExtensions.IsEqual(null, null);
        private It should_return_true = () => _result.ShouldBeTrue();
      }

      [Subject(typeof(StringExtensions), "IsEqual")]
      public sealed class when_invoked_with_a_null_source_and_a_non_null_value
      {
        private Because of = () => _result = StringExtensions.IsEqual(null, "Value");
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(StringExtensions), "IsEqual")]
      public sealed class when_invoked_with_a_non_null_source_and_a_null_value
      {
        private Because of = () => _result = StringExtensions.IsEqual("Source", null);
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(StringExtensions), "IsEqual")]
      public sealed class when_invoked_with_a_case_insensitive_matching_source_and_value
      {
        private Because of = () => _result = StringExtensions.IsEqual("Match", "match");
        private It should_return_true = () => _result.ShouldBeTrue();
      }

      [Subject(typeof(StringExtensions), "IsEqual")]
      public sealed class when_invoked_with_a_case_sensitive_matching_source_and_value
      {
        private Because of = () => _result = StringExtensions.IsEqual("Match", "match", false);
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(StringExtensions), "IsEqual")]
      public sealed class when_invoked_with_a_non_matching_source_and_value
      {
        private Because of = () => _result = StringExtensions.IsEqual("Source", "Value");
        private It should_return_false = () => _result.ShouldBeFalse();
      }
    }

    public sealed class FormatObjectMethod
    {
      private const string Pattern = "something/{key1}/else/{key2}";
      private static readonly object Values = new
          {
            key1 = "value1",
            key2 = "value2"
          };

      private static string _result;

      [Subject(typeof(StringExtensions), "Format<object>")]
      public sealed class when_invoked_with_a_null_pattern
      {
        private Because of = () => _result = StringExtensions.Format(null, Values);
        private It should_return_null = () => _result.ShouldBeNull();
      }

      [Subject(typeof(StringExtensions), "Format<object>")]
      public sealed class when_invoked_with_a_null_values_object
      {
        private static Exception _exception;

        private Because of = () => _exception = Catch.Exception(() => StringExtensions.Format(Pattern, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(StringExtensions), "Format<object>")]
      public sealed class when_invoked_with_a_pattern_and_a_values_object
      {
        private Because of = () => _result = StringExtensions.Format(Pattern, Values);
        private It should_return_a_formatted_string = () => _result.ShouldBeEqualIgnoringCase("something/value1/else/value2");
      }
    }

    public sealed class FormatDictionaryMethod
    {
      private const string Pattern = "something/{key1}/else/{key2}";
      private static readonly IDictionary<string, string> Values = new Dictionary<string, string>
        {
          {"key1", "value1"},
          {"key2", "value2"}
        };

      private static string _result;

      [Subject(typeof(StringExtensions), "Format<Dictionary>")]
      public sealed class when_invoked_with_a_null_pattern
      {
        private Because of = () => _result = StringExtensions.Format(null, Values);
        private It should_return_null = () => _result.ShouldBeNull();
      }

      [Subject(typeof(StringExtensions), "Format<Dictionary>")]
      public sealed class when_invoked_with_a_null_values_dictionary
      {
        private static Exception _exception;

        private Because of = () => _exception = Catch.Exception(() => StringExtensions.Format(Pattern, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(StringExtensions), "Format<Dictionary>")]
      public sealed class when_invoked_with_a_pattern_and_a_values_dictionary
      {
        private Because of = () => _result = StringExtensions.Format(Pattern, Values);
        private It should_return_a_formatted_string = () => _result.ShouldBeEqualIgnoringCase("something/value1/else/value2");
      }
    }

    public sealed class ToUpperCamelCaseMethod
    {
      private static string _result;

      [Subject(typeof(StringExtensions), "ToUpperCamelCase")]
      public sealed class when_invoked_with_a_null_value
      {
        private Because of = () => _result = StringExtensions.ToUpperCamelCase(null);
        private It should_return_null = () => _result.ShouldBeNull();
      }

      [Subject(typeof(StringExtensions), "ToUpperCamelCase")]
      public sealed class when_invoked_with_a_whitespace_value
      {
        private const string Value = "     ";

        private Because of = () => _result = StringExtensions.ToUpperCamelCase(Value);
        private It should_return_whitespace = () => _result.ShouldEqual(Value);
      }

      [Subject(typeof(StringExtensions), "ToUpperCamelCase")]
      public sealed class when_invoked_with_a_single_character
      {
        private Because of = () => _result = StringExtensions.ToUpperCamelCase("a");
        private It should_return_upper_camel_case_value = () => _result.ShouldEqual("A");
      }

      [Subject(typeof(StringExtensions), "ToUpperCamelCase")]
      public sealed class when_invoked_with_a_multiple_character_value
      {
        private Because of = () => _result = StringExtensions.ToUpperCamelCase("testValue");
        private It should_return_upper_camel_case_value = () => _result.ShouldEqual("TestValue");
      }

      [Subject(typeof(StringExtensions), "ToUpperCamelCase")]
      public sealed class when_invoked_with_a_padded_value
      {
        private Because of = () => _result = StringExtensions.ToUpperCamelCase("    test  ");
        private It should_return_trimmed_upper_camel_case_value = () => _result.ShouldEqual("Test");
      }
    }
  }
}