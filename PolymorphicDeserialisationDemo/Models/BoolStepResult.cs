namespace PolymorphicDeserialisationDemo
{
    public class BoolStepResult : ISurveyStepResult
    {
        public string Id { get; set; }
        public string TypeDiscriminator => nameof(BoolStepResult);

        public bool Value { get; set; }
    }
}
