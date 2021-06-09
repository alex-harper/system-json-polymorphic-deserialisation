namespace PolymorphicDeserialisationDemo
{
    public class BoolStepResult : ISurveyStepResult
    {
        public string Id { get; set; }
        public string TypeDiscriminator => "b";

        public bool Value { get; set; }
    }
}
