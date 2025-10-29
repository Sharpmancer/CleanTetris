using Features.Input.App.Api;
using Sharpmancer.Libs.Bitmasks;

namespace Features.Input.App.Internals
{
    internal class LastPressedWinsResolveStrategy : IResolveInputStrategy
    {
        public InboundCommands Resolve(InboundCommands activeFlag, InboundCommands lastMask, InboundCommands currentMask)
        {
            // no change - return as is
            if(currentMask == lastMask)
                return activeFlag;
            
            // zero or only one active input - return it
            if(currentMask.EmptyOrExactlyOneFlag())
                return currentMask;

            var newCommands = currentMask.RemoveFlags(lastMask);
            
            if(newCommands.HasAnyFlags())
                // if multiple new keys were pressed in the same frame (extremely rare) - take the first one by enum value
                return newCommands.FirstSetFlag();

            return currentMask.HasAnyFlags(activeFlag) 
                // no new buttons, but the active one is still being held
                ? activeFlag :
                // no new buttons, but the active one released - take the first one from those still held
                currentMask.FirstSetFlag();
        }
    }
}