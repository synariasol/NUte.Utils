using System.Collections.Generic;
using Machine.Specifications;

namespace NUte.Utils.Tests
{
  public sealed class ObjectExtensionsFixture
  {
    public sealed class IsEnumerableMethod
    {
      private static bool _result;

      [Subject(typeof(ObjectExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_null_value_object
      {
        private Because of = () => _result = ObjectExtensions.IsEnumerable(null);
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(ObjectExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_non_enumerable_value_object
      {
        private Because of = () => _result = ObjectExtensions.IsEnumerable(100);
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(ObjectExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_an_enumerable_value_object
      {
        private Because of = () => _result = ObjectExtensions.IsEnumerable(new[] {100, 200});
        private It should_return_true = () => _result.ShouldBeTrue();
      }

      [Subject(typeof(ObjectExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_string_value_object_and_not_allowed
      {
        private Because of = () => _result = ObjectExtensions.IsEnumerable("Test");
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(ObjectExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_string_value_object_and_allowed
      {
        private Because of = () => _result = ObjectExtensions.IsEnumerable("Test", true);
        private It should_return_true = () => _result.ShouldBeTrue();
      }
    }

    public sealed class ToDictionaryMethod
    {
      private static IDictionary<string, object> _result;

      [Subject(typeof(ObjectExtensions), "ToDictionary")]
      public sealed class when_invoked_with_a_null_values_object
      {
        private Because of = () => _result = ObjectExtensions.ToDictionary(null);
        private It should_return_an_empty_dictionary = () => _result.ShouldBeEmpty();
      }

      [Subject(typeof(ObjectExtensions), "ToDictionary")]
      public sealed class when_invoked_with_a_values_object
      {
        private Because of = () => _result = ObjectExtensions.ToDictionary(new { prop1 = "Test", prop2 = 100 });
        private It should_return_a_properties_dictionary = () => _result.ShouldContain(
            new KeyValuePair<string, object>("prop1", "Test"),
            new KeyValuePair<string, object>("prop2", 100));
      }
    }
  }
}