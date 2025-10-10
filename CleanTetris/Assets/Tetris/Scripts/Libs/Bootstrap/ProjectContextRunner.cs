using System;

namespace Libs.Bootstrap
{
    internal class ProjectContextRunner : ContextRunner
    {
        private static ProjectContextRunner _instance;

        protected override void Awake()
        {
            if (_instance != null)
                throw new InvalidOperationException("Only one instance of ProjectContextRunner can be active at a time.");
            _instance = this;
            base.Awake();
        }

        protected override ContextRunner GetParentContextRunner() => 
            null;

        internal static ProjectContextRunner GetOrCreateInstance(ProjectContextRunner projectContextPrefab)
        {
            if(_instance != null)
                return _instance;
            Instantiate(projectContextPrefab);
            DontDestroyOnLoad(_instance);
            return _instance;
        }
    }
}