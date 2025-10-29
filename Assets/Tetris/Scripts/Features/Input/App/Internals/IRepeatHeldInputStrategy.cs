namespace Features.Input.App.Internals
{
    internal interface IRepeatHeldInputStrategy
    {
        void ProcessTimePassed(float timeDelta, out bool repeat);
        void Reset();
    }
}