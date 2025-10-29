using Features.Input.App.Api;

namespace Features.Input.App.Internals
{
    /// <summary>
    /// Repeats held input indefinitely after a config-sourced delay each config-sourced interval   
    /// </summary>
    internal class ConfigurableRepeatInputStrategy : IRepeatHeldInputStrategy
    {
        private readonly IRepeatInputStrategyConfig _config;
        private float _heldTime;
        private int _repeatCount;
        
        internal ConfigurableRepeatInputStrategy(IRepeatInputStrategyConfig config) => 
            _config = config;

        public void ProcessTimePassed(float timeDelta, out bool repeat)
        {
            repeat = false;
            _heldTime += timeDelta;
            if (_heldTime < _config.StartRepeatDelay + _config.RepeatInterval * _repeatCount) 
                return;
            repeat = true;
            _repeatCount++;
        }

        public void Reset()
        {
            _heldTime = 0;
            _repeatCount = 0;
        }
    }
}