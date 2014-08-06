using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NUte.Utils.Json;

namespace NUte.Utils.Tests.Json
{
  public sealed class JsonReaderBaseFixture
  {
    private sealed class JsonReaderStub
      : JsonReaderBase
    {
      public readonly IList<JsonObject> Objects = new List<JsonObject>();
      public readonly IList<JsonProperty> Properties = new List<JsonProperty>();

      protected override void OnObjectParsed(JsonObject jsonObject)
      {
        Objects.Add(jsonObject);
      }

      protected override void OnPropertyParsed(JsonProperty jsonProperty)
      {
        Properties.Add(jsonProperty);
      }

      public JsonObject ReadJson(string json)
      {
        return Read(json);
      }
    }

    public sealed class ReadMethod
    {
      private static JsonReaderStub _reader;
      private static JsonObject _result;

      [Subject(typeof(JsonReaderBase), "Read")]
      public sealed class when_invoked_with_a_null_json_value
      {
        private Establish context = () => _reader = new JsonReaderStub();
        private Because of = () => _result = _reader.ReadJson(null);
        private It should_return_a_null_value = () => _result.ShouldBeNull();
      }

      [Subject(typeof(JsonReaderBase), "Read")]
      public sealed class when_invoked_with_a_whitespace_json_value
      {
        private Establish context = () => _reader = new JsonReaderStub();
        private Because of = () => _result = _reader.ReadJson("     ");
        private It should_return_a_null_value = () => _result.ShouldBeNull();
      }

      [Subject(typeof(JsonReaderBase), "Read")]
      public sealed class when_invoked_with_an_invalid_json_value
      {
        private static Exception _exception;

        private Establish context = () => _reader = new JsonReaderStub();
        private Because of = () => _exception = Catch.Exception(() => _reader.ReadJson("<INVALID>"));
        private It should_throw_a_JsonException = () => _exception.ShouldBeOfType<JsonException>();
        private It should_set_the_exception_message = () => _exception.Message.ShouldBeEqualIgnoringCase("An error occurred while reading the provided JSON.");
        private It should_set_the_exception_inner_expcetion = () => _exception.InnerException.ShouldNotBeNull();
      }

      [Subject(typeof(JsonReaderBase), "Read")]
      public sealed class when_invoked_with_a_property
      {
        private Establish context = () => _reader = new JsonReaderStub();
        private Because of = () => _result = _reader.ReadJson(@"{""Name"": ""Value""}");
        private It should_parse_the_property = () => VerifyJsonProperty(_result, "Name", "Value");
      }

      [Subject(typeof(JsonReaderBase), "Read")]
      public sealed class when_invoked_with_an_object
      {
        private Establish context = () => _reader = new JsonReaderStub();
        private Because of = () => _result = _reader.ReadJson(@"{""Object"": {""Name"": ""Value""}}");
        private It should_raise_object_parse_event = () => _reader.Objects.ShouldContain(o => o.Name == "Object");
        private It should_raise_property_parse_event = () => _reader.Properties.ShouldContain(p => p.Name == "Name" && p.Value.Equals("Value"));
        private It should_parse_the_object = () =>
          {
            var property = _result.Properties.Single();
            var jsonObject = property.Value as JsonObject;

            VerifyJsonObject(jsonObject, "Object");
            VerifyJsonProperty(jsonObject, "Name", "Value");
          };
      }

      [Subject(typeof(JsonReaderBase), "Read")]
      public sealed class when_invoked_with_an_array
      {
        private Establish context = () => _reader = new JsonReaderStub();
        private Because of = () => _result = _reader.ReadJson(@"{""Array"": [{""Object1"": {""Name1"": ""Value1""}},{""Object2"": {""Name2"": ""Value2""}}]}");
        private It should_raise_object_parse_event = () => _reader.Objects.ShouldContain(o => o.Name == "Object1" || o.Name == "Object2");
        private It should_raise_property_parse_event = () => _reader.Properties.ShouldContain(p => (p.Name == "Name1" && p.Value.Equals("Value1")) || (p.Name == "Name2" && p.Value.Equals("Value2")));
        private It should_parse_the_object = () =>
          {
            var index = 1;
            var items = (from property in _result.Properties
                         let objs = property.Value as List<object>
                         from obj in objs.Cast<JsonObject>()
                         select obj).ToList();

            items.ShouldNotBeEmpty();

            foreach (var item in items)
            {
              var property = item.Properties.Single();
              var jsonObject = property.Value as JsonObject;

              item.Name.ShouldEqual("Array");

              VerifyJsonObject(jsonObject, "Object" + index);
              VerifyJsonProperty(jsonObject, "Name" + index, "Value" + index);
              
              index++;
            }
          };
      }

      private static void VerifyJsonObject(JsonObject jsonObject, string objectName)
      {
        var eventCount = (from obj in _reader.Objects
                          where obj.Name == objectName
                          select obj).Count();

        jsonObject.Name.ShouldEqual(objectName);
        eventCount.ShouldEqual(1);
      }

      private static void VerifyJsonProperty(JsonObject jsonObject, string propertyName, object value)
      {
        var resultCount = (from property in jsonObject.Properties
                           where property.Name == propertyName && property.Value.Equals(value)
                           select property).Count();

        var eventCount = (from property in _reader.Properties
                          where property.Name == propertyName && property.Value.Equals(value)
                          select property).Count();

        resultCount.ShouldEqual(1);
        eventCount.ShouldEqual(1);
      }
    }
  }
}