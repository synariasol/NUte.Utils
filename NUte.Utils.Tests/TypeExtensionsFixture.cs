using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace NUte.Utils.Tests
{
  public sealed class TypeExtensionsFixture
  {
    public sealed class IsEnumerableMethod
    {
      private static bool _result;

      [Subject(typeof(TypeExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_null_type
      {
        private static Exception _exception;

        private Because of = () => _exception = Catch.Exception(() => TypeExtensions.IsEnumerable(null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(TypeExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_non_enumerable_type
      {
        private Because of = () => _result = TypeExtensions.IsEnumerable(typeof(int));
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(TypeExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_an_enumerable_type
      {
        private Because of = () => _result = TypeExtensions.IsEnumerable(typeof(List<int>));
        private It should_return_true = () => _result.ShouldBeTrue();
      }

      [Subject(typeof(TypeExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_string_type_and_not_allowed
      {
        private Because of = () => _result = TypeExtensions.IsEnumerable(typeof(string));
        private It should_return_false = () => _result.ShouldBeFalse();
      }

      [Subject(typeof(TypeExtensions), "IsEnumerable")]
      public sealed class when_invoked_with_a_string_type_and_allowed
      {
        private Because of = () => _result = TypeExtensions.IsEnumerable(typeof(string), true);
        private It should_return_true = () => _result.ShouldBeTrue();
      }
    }

    public sealed class GetGenericTypesMethod
    {
      private static IEnumerable<Type> _result;

      [Subject(typeof(TypeExtensions), "GetGenericTypes")]
      public sealed class when_invoked_with_a_null_type
      {
        private static Exception _exception;

        private Because of = () => _exception = Catch.Exception(() => TypeExtensions.GetGenericTypes(null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(TypeExtensions), "GetGenericTypes")]
      public sealed class when_invoked_with_a_non_generic_type
      {
        private Because of = () => _result = TypeExtensions.GetGenericTypes(typeof(int));
        private It should_return_an_empty_list = () => _result.ShouldBeEmpty();
      }

      [Subject(typeof(TypeExtensions), "GetGenericTypes")]
      public sealed class when_invoked_with_a_generic_type
      {
        private Because of = () => _result = TypeExtensions.GetGenericTypes(typeof(Dictionary<int, string>));
        private It should_return_a_list_of_types = () => _result.ShouldContainOnly(typeof(int), typeof(string));
      }
    }

    public sealed class GetNullableTypeMethod
    {
      private static Type _result;

      [Subject(typeof(TypeExtensions), "GetNullableType")]
      public sealed class when_invoked_with_a_null_type
      {
        private static Exception _exception;

        private Because of = () => _exception = Catch.Exception(() => TypeExtensions.GetNullableType(null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(TypeExtensions), "GetNullableType")]
      public sealed class when_invoked_with_a_non_generic_type
      {
        private Because of = () => _result = TypeExtensions.GetNullableType(typeof(int));
        private It should_return_an_empty_list = () => _result.ShouldBeNull();
      }

      [Subject(typeof(TypeExtensions), "GetNullableType")]
      public sealed class when_invoked_with_a_non_nullable_generic_type
      {
        private Because of = () => _result = TypeExtensions.GetNullableType(typeof(List<int>));
        private It should_return_an_empty_list = () => _result.ShouldBeNull();
      }

      [Subject(typeof(TypeExtensions), "GetNullableType")]
      public sealed class when_invoked_with_a_nullable_generic_type
      {
        private Because of = () => _result = TypeExtensions.GetNullableType(typeof(int?));
        private It should_return_nullable_generic_type = () => _result.ShouldEqual(typeof(int));
      }
    }
  }
}