using System;
using System.Collections.Generic;
using Libs.Core;

namespace Libs.Bootstrap
{
    internal class RunnableContext : IInstallableContext
    {
        private readonly RunnableContext _parent;
        private readonly Dictionary<Type, object> _contracts = new();
        private readonly LifecycleControlledObjects _runnables = new();
        
        internal RunnableContext(RunnableContext parent = null) => 
            _parent = parent;

        public T Get<T>() where T : class =>  
            _contracts.TryGetValue(typeof(T), out var instance) 
                ? (T)instance 
                : _parent?.Get<T>() ?? throw new KeyNotFoundException($"No implementation for {typeof(T).Name} found");

        public void RegisterContract<TContract>(TContract instance) => 
            _contracts.Add(typeof(TContract), instance);

        public void RegisterRunnable(object instance) => 
            _runnables.Add(instance);
        
        internal void RunInstallers(IEnumerable<Installer> installers)
        {
            foreach (var installer in installers) 
                installer.Install(this);
        }

        internal void RunPreInitializables() => 
            _runnables.PreInitialize();

        internal void RunInitializables() => 
            _runnables.Initialize();

        internal void RunTickables(float timeDelta) => 
            _runnables.Tick(timeDelta);

        internal void RunDisposables() => 
            _runnables.Dispose();
    }
}