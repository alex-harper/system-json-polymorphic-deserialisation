namespace PolymorphicDeserialisationDemo
{
    public interface ISurveyStepResult : ITypeDiscriminator
    {
        string Id { get; set; }
    }
}
