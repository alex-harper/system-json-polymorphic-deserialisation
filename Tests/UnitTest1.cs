using NUnit.Framework;
using PolymorphicDeserialisationDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Tests
{
    /// <summary>
    /// Cloned from: https://stackoverflow.com/questions/58074304/is-polymorphic-deserialization-possible-in-system-text-json/59744873#59744873
    /// </summary>
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SerializeAndDeserializeTest()
        {
            var surveyResult = new SurveyRootModel()
            {
                Id = "id",
                SurveyId = "surveyId",
                Steps = new List<ISurveyStepResult>()
                {
                    new BoolStepResult(){ Id = "1", Value = true},
                    new TextStepResult(){ Id = "2", Value = "some text"},
                    new ExtendedStepModel()
                    { 
                        Id = "3", 
                        Value = 5, 
                        OtherIntValue = 10,
                        OtherString = "Oh hi",
                        DateTime = DateTime.Now
                    },                    
                }
            };

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                Converters = { new TypeDiscriminatorConverter<ISurveyStepResult>() },
                WriteIndented = true
            };
            var result = JsonSerializer.Serialize(surveyResult, jsonSerializerOptions);

            var back = JsonSerializer.Deserialize<SurveyRootModel>(result, jsonSerializerOptions);

            var result2 = JsonSerializer.Serialize(back, jsonSerializerOptions);

            Assert.IsTrue(back.Steps.Count == 3
                          && back.Steps.Any(x => x is BoolStepResult)
                          && back.Steps.Any(x => x is TextStepResult)
                          && back.Steps.Any(x => x is ExtendedStepModel)
                          );
            Assert.AreEqual(result2, result);
        }
    }
}