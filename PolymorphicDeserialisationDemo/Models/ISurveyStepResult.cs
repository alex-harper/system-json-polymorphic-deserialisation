namespace PolymorphicDeserialisationDemo
{
    public interface ISurveyStepResult
    {
        abstract string TypeDiscriminator { get; }
        string Id { get; set; }
    }
}
