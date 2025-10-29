namespace Features.Input.App
{
    internal interface IRepeatHeldInputStrategy
    {
        void ProcessTimePassed(float timeDelta, out bool repeat);
        void Reset();
    }
}