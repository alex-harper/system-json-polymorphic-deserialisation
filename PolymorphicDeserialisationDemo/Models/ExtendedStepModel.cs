using PolymorphicDeserialisationDemo.Models;
using System;
using System.Collections.Generic;

namespace PolymorphicDeserialisationDemo
{
    public class ExtendedStepModel : ISurveyStepResult
    {
        public string Id { get; set; }
        public string TypeDiscriminator => nameof(ExtendedStepModel);
        
        public int Value { get; set; }
        public int OtherIntValue { get; set; }
        public string OtherString { get; set; }
        public DateTime DateTime { get; set; }

        public List<Layer> Layers { get; set; }
    }
}
