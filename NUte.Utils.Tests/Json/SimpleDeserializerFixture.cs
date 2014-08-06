using System;
using System.Collections.Generic;
using Machine.Specifications;
using NUte.Utils.Json;

namespace NUte.Utils.Tests.Json
{
  public sealed class SimpleDeserializerFixture
  {
    private sealed class TestObject
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime? DeleteDate { get; private set; }
      public IEnumerable<int> Codes { get; internal set; }
      public TestObject Parent { get; set; }
      public IEnumerable<TestObject> Children { get; set; }
    }

    private const string TestJson = @"{
      ""Id"": 101,
      ""Name"": ""Name1"",
      ""DeleteDate"": ""2000-01-01"",
      ""Codes"": [1, 2, 3],
      ""Parent"": {
        ""Id"": 102,
        ""Name"": ""Name2"",
        ""DeleteDate"": ""2000-01-02"",
        ""Codes"": [2, 3, 4],
        ""Children"": [{
          ""Id"": 103,
          ""Name"": ""Name3"",
          ""DeleteDate"": ""2000-01-03"",
          ""Codes"": [3, 4, 5]
        }, {
          ""Id"": 104,
          ""Name"": ""Name4"",
          ""DeleteDate"": ""2000-01-04"",
          ""Codes"": [4, 5, 6]
        }]
      }
    }";

    public sealed class DeserializeMethod
    {
      private static SimpleDeserializer _deserializer;
      private static TestObject _result;

      [Subject(typeof(SimpleDeserializer), "Deserialize_Default")]
      public sealed class when_invoked_with_json
      {
        private Establish context = () => _deserializer = new SimpleDeserializer();
        private Because of = () => _result = _deserializer.Deserialize<TestObject>(TestJson);
        private It should_return_the_deserialized_instance = () => VerifyTestObject(1, _result);
      }
    }

    private static void VerifyTestObject(int index, TestObject testObject)
    {
      testObject.Id.ShouldEqual(100 + index);
      testObject.Name.ShouldEqual("Name" + index);
      testObject.DeleteDate.ShouldEqual(new DateTime(2000, 1, index));
      testObject.Codes.ShouldContain(code => code > 0 && code < index + 3);

      if (index == 1)
      {
        VerifyTestObject(index + 1, testObject.Parent); 
      }
      else
      {
        testObject.Parent.ShouldBeNull();
      }

      if (index == 2)
      {
        var childIndex = index;

        foreach (var child in testObject.Children)
        {
          childIndex++;
          VerifyTestObject(childIndex, child);
        }
      }
      else
      {
        testObject.Children.ShouldBeNull();
      }
    }
  }
}