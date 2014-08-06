using System;
using System.Collections.Generic;
using Machine.Specifications;
using NUte.Utils.Json;

namespace NUte.Utils.Tests.Json
{
  public sealed class JsonReaderExtensionsFixture
  {
    private static JsonObject _jsonObject;
    private static JsonProperty _jsonProperty;

    public sealed class PropertyAsJsonObjectMethod
    {
      private static JsonObject _result;
      private static Exception _exception;
      
      [Subject(typeof(JsonReaderExtensions), "PropertyAsJsonObject")]
      public sealed class when_invoked_with_a_null_json_object
      {
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsJsonObject(null, "Property1"));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsJsonObject")]
      public sealed class when_invoked_with_a_null_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsJsonObject(_jsonObject, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsJsonObject")]
      public sealed class when_invoked_with_a_whitespace_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsJsonObject(_jsonObject, "     "));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => ValidationMessages.VerifyMessage(_exception.Message, ValidationMessages.WhiteSpaceMessage, "name");
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsJsonObject")]
      public sealed class when_invoked_with_a_json_object_and_an_invalid_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _result = JsonReaderExtensions.PropertyAsJsonObject(_jsonObject, "Property2");
        private It should_return_null = () => _result.ShouldBeNull();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsJsonObject")]
      public sealed class when_invoked_with_a_json_object_and_a_valid_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _result = JsonReaderExtensions.PropertyAsJsonObject(_jsonObject, "Property1");
        private It should_return_json_object = () => _result.ShouldBeOfType<JsonObject>();
      }
    }

    public sealed class PropertyAsStringMethod
    {
      private static string _result;
      private static Exception _exception;

      [Subject(typeof(JsonReaderExtensions), "PropertyAsString")]
      public sealed class when_invoked_with_a_null_json_object
      {
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsString(null, "Property2"));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsString")]
      public sealed class when_invoked_with_a_null_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsString(_jsonObject, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsString")]
      public sealed class when_invoked_with_a_whitespace_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsString(_jsonObject, "     "));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => ValidationMessages.VerifyMessage(_exception.Message, ValidationMessages.WhiteSpaceMessage, "name");
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsString")]
      public sealed class when_invoked_with_a_json_object_and_an_invalid_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _result = JsonReaderExtensions.PropertyAsString(_jsonObject, "Property1");
        private It should_return_null = () => _result.ShouldBeNull();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsString")]
      public sealed class when_invoked_with_a_json_object_and_a_valid_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _result = JsonReaderExtensions.PropertyAsString(_jsonObject, "Property2");
        private It should_return_string = () => _result.ShouldBeOfType<string>();
        private It should_return_string_value = () => _result.ShouldEqual("Value");
      }
    }

    public sealed class PropertyAsEnumerableMethod
    {
      private static IEnumerable<object> _result;
      private static Exception _exception;

      [Subject(typeof(JsonReaderExtensions), "PropertyAsEnumerable")]
      public sealed class when_invoked_with_a_null_json_object
      {
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsEnumerable(null, "Property3"));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsEnumerable")]
      public sealed class when_invoked_with_a_null_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsEnumerable(_jsonObject, null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsEnumerable")]
      public sealed class when_invoked_with_a_whitespace_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.PropertyAsEnumerable(_jsonObject, "     "));
        private It should_throw_an_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
        private It should_set_the_exception_message = () => ValidationMessages.VerifyMessage(_exception.Message, ValidationMessages.WhiteSpaceMessage, "name");
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsEnumerable")]
      public sealed class when_invoked_with_a_json_object_and_an_invalid_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _result = JsonReaderExtensions.PropertyAsEnumerable(_jsonObject, "Property1");
        private It should_return_empty_enumerable = () => _result.ShouldBeEmpty();
      }

      [Subject(typeof(JsonReaderExtensions), "PropertyAsEnumerable")]
      public sealed class when_invoked_with_a_json_object_and_a_valid_name
      {
        private Establish context = () => _jsonObject = CreateJsonObject();
        private Because of = () => _result = JsonReaderExtensions.PropertyAsEnumerable(_jsonObject, "Property3");
        private It should_return_enumerable = () => _result.ShouldBeOfType<IEnumerable<object>>();
        private It should_return_enumerable_values = () => _result.ShouldContainOnly(1, 2, 3);
      }
    }

    public sealed class AsJsonObjectMethod
    {
      private static JsonObject _result;
      private static Exception _exception;

      [Subject(typeof(JsonReaderExtensions), "AsJsonObject")]
      public sealed class when_invoked_with_a_null_json_property
      {
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.AsJsonObject(null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "AsJsonObject")]
      public sealed class when_invoked_with_an_invalid_json_property
      {
        private Establish context = () => _jsonProperty = new JsonProperty("JsonObject", "Value");
        private Because of = () => _result = JsonReaderExtensions.AsJsonObject(_jsonProperty);
        private It should_return_null = () => _result.ShouldBeNull();
      }

      [Subject(typeof(JsonReaderExtensions), "AsJsonObject")]
      public sealed class when_invoked_with_a_valid_property
      {
        private Establish context = () => _jsonProperty = new JsonProperty("JsonObject", CreateJsonObject(false));
        private Because of = () => _result = JsonReaderExtensions.AsJsonObject(_jsonProperty);
        private It should_return_json_object = () => _result.ShouldBeOfType<JsonObject>();
      }
    }

    public sealed class AsStringMethod
    {
      private static string _result;
      private static Exception _exception;

      [Subject(typeof(JsonReaderExtensions), "AsString")]
      public sealed class when_invoked_with_a_null_json_object
      {
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.AsString(null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "AsString")]
      public sealed class when_invoked_with_an_invalid_json_property
      {
        private Establish context = () => _jsonProperty = new JsonProperty("String", CreateJsonObject(false));
        private Because of = () => _result = JsonReaderExtensions.AsString(_jsonProperty);
        private It should_return_null = () => _result.ShouldBeNull();
      }

      [Subject(typeof(JsonReaderExtensions), "AsString")]
      public sealed class when_invoked_with_a_valid_property
      {
        private Establish context = () => _jsonProperty = new JsonProperty("String", "Value");
        private Because of = () => _result = JsonReaderExtensions.AsString(_jsonProperty);
        private It should_return_string = () => _result.ShouldBeOfType<string>();
        private It should_return_string_value = () => _result.ShouldEqual("Value");
      }
    }

    public sealed class AsEnumerableMethod
    {
      private static IEnumerable<object> _result;
      private static Exception _exception;

      [Subject(typeof(JsonReaderExtensions), "AsEnumerable")]
      public sealed class when_invoked_with_a_null_json_object
      {
        private Because of = () => _exception = Catch.Exception(() => JsonReaderExtensions.AsEnumerable(null));
        private It should_throw_an_ArgumentNullException = () => _exception.ShouldBeOfType<ArgumentNullException>();
      }

      [Subject(typeof(JsonReaderExtensions), "AsEnumerable")]
      public sealed class when_invoked_with_an_invalid_json_property
      {
        private Establish context = () => _jsonProperty = new JsonProperty("Enumerable", CreateJsonObject(false));
        private Because of = () => _result = JsonReaderExtensions.AsEnumerable(_jsonProperty);
        private It should_return_empty_enumerable = () => _result.ShouldBeEmpty();
      }

      [Subject(typeof(JsonReaderExtensions), "AsEnumerable")]
      public sealed class when_invoked_with_a_valid_property
      {
        private Establish context = () => _jsonProperty = new JsonProperty("Enumerable", new[] {1, 2, 3});
        private Because of = () => _result = JsonReaderExtensions.AsEnumerable(_jsonProperty);
        private It should_return_enumerable = () => _result.ShouldBeOfType<IEnumerable<object>>();
        private It should_return_enumerable_values = () => _result.ShouldContainOnly(1, 2, 3);
      }
    }

    private static JsonObject CreateJsonObject(bool createProperties = true)
    {
      if (createProperties)
      {
        var jsonObject = new JsonObject("Object1");

        jsonObject.AddProperty("Property1", CreateJsonObject(false));
        jsonObject.AddProperty("Property2", "Value");
        jsonObject.AddProperty("Property3", new[] {1, 2, 3});

        return jsonObject;
      }

      return new JsonObject("Object2");
    }
  }
}