using System.Collections.Generic;

namespace PolymorphicDeserialisationDemo
{
    public class SurveyRootModel
    {
        public SurveyRootModel()
        {
        }

        public string Id { get; set; }
        public string SurveyId { get; set; }
        public List<ISurveyStepResult> Steps { get; set; }
    }
}
