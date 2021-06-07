namespace PolymorphicDeserialisationDemo
{
    public class TextStepResult : ISurveyStepResult
    {
        public string Id { get; set; }
        public string TypeDiscriminator => nameof(TextStepResult);

        public string Value { get; set; }
    }
}
