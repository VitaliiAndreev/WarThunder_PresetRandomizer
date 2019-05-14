﻿using Core.DataBase.Tests;
using Core.Helpers.Logger.Enumerations;
using Core.Json.Helpers;
using Core.Json.Helpers.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Core.Json.Tests.Helpers
{
    /// <summary> See <see cref="JsonHelper"/>. </summary>
    [TestClass]
    public class JsonHelperTests
    {
        #region Fake Classes

        private class TestClass_NoDuplicates
        {
            public int Property1 { get; set; }

            public int[] Array1 { get; set; }
        }

        #endregion Fake Classes

        private IJsonHelper _jsonHelper;

        #region Internal Methods

        [TestInitialize]
        public void Initialize()
        {
            _jsonHelper = new JsonHelper(Presets.Logger);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
        }

        #endregion Internal Methods
        #region Tests: DeserializeObject()

        [TestMethod]
        public void DeserializeObject_NoDuplicates()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var arrayName1 = "Array1";
            var arrayValue1 = new int[] { 1, 2 };
            var jsonObjectText = "\r\n{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n\"" + arrayName1 + "\":\r\n[\r\n" + arrayValue1[0] + ",\r\n" + arrayValue1[1] + "\r\n]\r\n}";

            // act
            var testObject = _jsonHelper.DeserializeObject<TestClass_NoDuplicates>(jsonObjectText);

            // assert
            testObject.Property1.Should().Be(propertyValue1);
            testObject.Array1.Zip(arrayValue1.ToList(), (left, right) => left == right).All(result => result == true).Should().BeTrue();
        }

        #endregion Tests: DeserializeObject()
    }
}
