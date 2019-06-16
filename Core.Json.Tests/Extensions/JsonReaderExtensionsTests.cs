using Core.Json.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Core.Json.Tests.Extensions
{
    /// <summary> See <see cref="JsonReaderExtensions"/>. </summary>
    [TestClass]
    public class JsonReaderExtensionsTests
    {
        #region Tests: Deserialize()

        [TestMethod]
        public void Deserialize_NoDuplicates()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var arrayName1 = "Array1";
            var arrayValue1 = new int[] { 1, 2 };
            var jsonObjectText = "\r\n{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n\"" + arrayName1 + "\":\r\n[\r\n" + arrayValue1[0] + ",\r\n" + arrayValue1[1] + "\r\n]\r\n}";
            var jsonObject = default(JObject);

            // act
            using (var jsonReader = jsonObjectText.CreateJsonReader())
                jsonObject = jsonReader.Deserialize() as JObject;

            var property1 = jsonObject[propertyName1];
            var array1 = jsonObject[arrayName1];

            // assert
            jsonObject.Properties().Count().Should().Be(2);

            property1.Value<int>().Should().Be(propertyValue1);
            array1.Count().Should().Be(arrayValue1.Count());

            for (var i = 0; i < array1.Count(); i++)
                array1[i].Value<int>().Should().Be(arrayValue1[i]);
        }

        [TestMethod]
        public void Deserialize_DuplicatesIgnored()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var propertyValue1d = 42;
            var jsonObjectText = "{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n\"" + propertyName1 + "\": " + propertyValue1d + ",\r\n}";
            var jsonObject = default(JObject);

            // act
            using (var jsonReader = jsonObjectText.CreateJsonReader())
                jsonObject = jsonReader.Deserialize() as JObject;

            var property1 = jsonObject[propertyName1];

            // assert
            jsonObject.Properties().Count().Should().Be(1);
            property1.Value<int>().Should().Be(propertyValue1);
        }

        #endregion Tests: Deserialize()
        #region Tests: DeserializeAndCombineDuplicatesLoosely()

        [TestMethod]
        public void DeserializeAndCombineDuplicatesLoosely_NoDuplicates()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var arrayName1 = "Array1";
            var arrayValue1 = new int[] { 1, 2 };
            var jsonObjectText = "\r\n{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n\"" + arrayName1 + "\":\r\n[\r\n" + arrayValue1[0] + ",\r\n" + arrayValue1[1] + "\r\n]\r\n}";
            var jsonObject = default(JObject);

            // act
            using (var jsonReader = jsonObjectText.CreateJsonReader())
                jsonObject = jsonReader.DeserializeAndCombineDuplicatesLoosely() as JObject;

            var property1 = jsonObject[propertyName1];
            var array1 = jsonObject[arrayName1];

            // assert
            jsonObject.Properties().Count().Should().Be(2);

            property1.Value<int>().Should().Be(propertyValue1);
            array1.Count().Should().Be(arrayValue1.Count());

            for (var i = 0; i < array1.Count(); i++)
                array1[i].Value<int>().Should().Be(arrayValue1[i]);
        }

        [TestMethod]
        public void DeserializeAndCombineDuplicatesLoosely_DuplicatesAggregatedIntoArray()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var propertyValue1d = 42;
            var jsonObjectText = "{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n\"" + propertyName1 + "\": " + propertyValue1d + ",\r\n}";
            var jsonObject = default(JObject);

            // act
            using (var jsonReader = jsonObjectText.CreateJsonReader())
                jsonObject = jsonReader.DeserializeAndCombineDuplicatesLoosely() as JObject;

            var property1 = jsonObject[propertyName1];

            // assert
            jsonObject.Properties().Count().Should().Be(1);

            property1[0].Value<int>().Should().Be(propertyValue1);
            property1[1].Value<int>().Should().Be(propertyValue1d);
        }

        [TestMethod]
        public void DeserializeAndCombineDuplicatesLoosely_DuplicatesAggregatedIntoArrays_IndependentlyFromOtherObjects()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var propertyValue1d = 42;
            var jsonObjectText1 = "{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n\"" + propertyName1 + "\": " + propertyValue1d + ",\r\n}";
            var jsonObjectText2 = "{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n}";
            var jsonText = "[\r\n" + jsonObjectText1 + ",\r\n" + jsonObjectText2 + "\r\n]";
            var jsonArray = default(JArray);

            // act
            using (var jsonReader = jsonText.CreateJsonReader())
                jsonArray = jsonReader.DeserializeAndCombineDuplicatesLoosely() as JArray;

            var jsonObject1 = jsonArray[0] as JObject;
            var jsonObject2 = jsonArray[1] as JObject;

            var property11 = jsonObject1[propertyName1];
            var property21 = jsonObject2[propertyName1];

            // assert
            jsonObject1.Properties().Count().Should().Be(1);
            jsonObject2.Properties().Count().Should().Be(1);

            property11[0].Value<int>().Should().Be(propertyValue1);
            property11[1].Value<int>().Should().Be(propertyValue1d);

            property21.Value<int>().Should().Be(propertyValue1);
        }

        #endregion Tests: DeserializeAndCombineDuplicatesLoosely()
    }
}
