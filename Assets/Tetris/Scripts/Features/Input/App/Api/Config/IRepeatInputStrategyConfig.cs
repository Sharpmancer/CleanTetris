namespace Features.Input.App.Api
{
    public interface IRepeatInputStrategyConfig
    {
        float StartRepeatDelay { get; }
        float RepeatInterval { get; }
    }
}