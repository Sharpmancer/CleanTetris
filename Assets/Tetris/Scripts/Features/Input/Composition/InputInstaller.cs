using Features.Input.App;
using Features.Input.Infrastructure;
using Libs.Bootstrap;
using UnityEngine;

namespace Features.Input.Composition
{
    public class InputInstaller : Installer
    {
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private UnityInputAdapter _unityInputAdapter;
        
        public override void Install(IInstallableContext context)
        {
            var useCase = new ProcessInputEventsUseCase(new ConfigurableRepeatInputStrategy(_inputConfig), new LastPressedWinsResolveStrategy());
            _unityInputAdapter.Construct(listener: useCase, _inputConfig);
            context.RegisterRunnable(useCase);
            context.RegisterRunnable(_unityInputAdapter);
            context.RegisterContract<IOutboundInputCommandDispatcher>(useCase);
            context.RegisterContract<IInputStateResetter>(useCase);
        }
    }
}