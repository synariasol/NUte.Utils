using System;
using Machine.Specifications;
using NUte.Utils.Enum;

namespace NUte.Utils.Tests.Enum
{
  public sealed class EnumExtensionsFixture
  {
    private enum EnumValues
    {
      Value1,

      [EnumData("Data")]
      Value2
    }

    private static readonly Type EnumType = typeof(EnumValues);

    private static readonly EnumValues EnumValuesType;
    private static readonly AttributeTargets InvalidEnumType;

    private static string _data;
    private static EnumValues? _enumValue;
    private static object _objectValue;

    private static Exception _exception;

    #region GetEnumData

    public sealed class GetEnumDataMethod_Value
    {
      [Subject(typeof(EnumExtensions), "GetEnumData_Value")]
      public sealed class when_invoked_with_a_value_without_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumValues.Value1);
        private It should_return_null = () => _data.ShouldBeNull();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Value")]
      public sealed class when_invoked_with_a_value_with_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumValues.Value2);
        private It should_return_data = () => _data.ShouldEqual("Data");
      }
    }

    public sealed class GetEnumDataMethod_Enum_TEnum
    {
      [Subject(typeof(EnumExtensions), "GetEnumData_Enum<TEnum>")]
      public sealed class when_invoked_with_a_null_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData((EnumValues?)null, EnumValues.Value1));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum<TEnum>")]
      public sealed class when_invoked_with_a_value_without_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumValuesType, EnumValues.Value1);
        private It should_return_null = () => _data.ShouldBeNull();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum<TEnum>")]
      public sealed class when_invoked_with_a_value_with_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumValuesType, EnumValues.Value2);
        private It should_return_data = () => _data.ShouldEqual("Data");
      }
    }

    public sealed class GetEnumDataMethod_Enum
    {
      [Subject(typeof(EnumExtensions), "GetEnumData_Enum")]
      public sealed class when_invoked_with_a_null_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData((EnumValues?)null, (object)EnumValues.Value1));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum")]
      public sealed class when_invoked_with_a_null_value
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData(EnumValuesType, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum")]
      public sealed class when_invoked_with_a_value_without_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumValuesType, (object)EnumValues.Value1);
        private It should_return_null = () => _data.ShouldBeNull();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum")]
      public sealed class when_invoked_with_a_value_with_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumValuesType, (object)EnumValues.Value2);
        private It should_return_data = () => _data.ShouldEqual("Data");
      }
    }

    public sealed class GetEnumDataMethod_Type_TEnum
    {
      [Subject(typeof(EnumExtensions), "GetEnumData_Enum<TEnum>")]
      public sealed class when_invoked_with_a_null_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData((Type)null, EnumValues.Value1));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum<TEnum>")]
      public sealed class when_invoked_with_an_invalid_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData(typeof(int), EnumValues.Value1));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => _exception.Message.ShouldBeEqualIgnoringCase("The specified enumeration type is invalid.");
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum<TEnum>")]
      public sealed class when_invoked_with_a_value_without_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumType, EnumValues.Value1);
        private It should_return_null = () => _data.ShouldBeNull();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData_Enum<TEnum>")]
      public sealed class when_invoked_with_a_value_with_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumType, EnumValues.Value2);
        private It should_return_data = () => _data.ShouldEqual("Data");
      }
    }

    public sealed class GetEnumDataMethod_Type
    {
      [Subject(typeof(EnumExtensions), "GetEnumData")]
      public sealed class when_invoked_with_a_null_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData((Type)null, (object)EnumValues.Value1));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData")]
      public sealed class when_invoked_with_a_null_value
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData(EnumType, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData")]
      public sealed class when_invoked_with_an_invalid_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumData(typeof(int), (object)EnumValues.Value1));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => _exception.Message.ShouldBeEqualIgnoringCase("The specified type is not an enumeration.");
      }

      [Subject(typeof(EnumExtensions), "GetEnumData")]
      public sealed class when_invoked_with_a_value_without_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumType, (object)EnumValues.Value1);
        private It should_return_null = () => _data.ShouldBeNull();
      }

      [Subject(typeof(EnumExtensions), "GetEnumData")]
      public sealed class when_invoked_with_a_value_with_data
      {
        private Because of = () => _data = EnumExtensions.GetEnumData(EnumType, (object)EnumValues.Value2);
        private It should_return_data = () => _data.ShouldEqual("Data");
      }
    }

    #endregion

    #region GetEnumFromData

    public sealed class GetEnumFromDataMethod_TEnum
    {
      [Subject(typeof(EnumExtensions), "GetEnumFromData<TEnum>")]
      public sealed class when_invoked_with_a_null_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumFromData<EnumValues>(null, "Data"));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData<TEnum>")]
      public sealed class when_invoked_with_a_null_data
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumFromData<EnumValues>(EnumType, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData<TEnum>")]
      public sealed class when_invoked_with_an_invalid_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumFromData<EnumValues>(typeof(int), "Data"));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => _exception.Message.ShouldBeEqualIgnoringCase("The specified type is not an enumeration.");
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData<TEnum>")]
      public sealed class when_invoked_with_a_value_without_data
      {
        private Because of = () => _enumValue = EnumExtensions.GetEnumFromData<EnumValues>(EnumType, "Test");
        private It should_return_null = () => _enumValue.ShouldBeNull();
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData<TEnum>")]
      public sealed class when_invoked_with_a_value_with_data
      {
        private Because of = () => _enumValue = EnumExtensions.GetEnumFromData<EnumValues>(EnumType, "Data");
        private It should_return_data = () => _enumValue.ShouldEqual(EnumValues.Value2);
      }
    }

    public sealed class GetEnumFromDataMethod
    {
      [Subject(typeof(EnumExtensions), "GetEnumFromData")]
      public sealed class when_invoked_with_a_null_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumFromData(null, "Data"));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData")]
      public sealed class when_invoked_with_a_null_data
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumFromData(EnumType, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData")]
      public sealed class when_invoked_with_an_invalid_enumType
      {
        private Because of = () => _exception = Catch.Exception(() => EnumExtensions.GetEnumFromData(typeof(int), "Data"));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => _exception.Message.ShouldBeEqualIgnoringCase("The specified type is not an enumeration.");
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData")]
      public sealed class when_invoked_with_a_value_without_data
      {
        private Because of = () => _objectValue = EnumExtensions.GetEnumFromData(EnumType, "Test");
        private It should_return_null = () => _objectValue.ShouldBeNull();
      }

      [Subject(typeof(EnumExtensions), "GetEnumFromData")]
      public sealed class when_invoked_with_a_value_with_data
      {
        private Because of = () => _objectValue = EnumExtensions.GetEnumFromData(EnumType, "Data");
        private It should_return_data = () => _objectValue.ShouldEqual(EnumValues.Value2);
      }
    }

    #endregion
  }
}