using Libs.Core;
using UnityEngine;

namespace Libs.Bootstrap
{
    [DisallowMultipleComponent]
    internal abstract class ContextRunner : MonoBehaviour
    {
        private readonly ExceptionHandler _exceptionHandler = new();
        private ContextRunner _parent;
        private RunnableContext _context;

        protected virtual void Awake()
        {
            _parent = GetParentContextRunner();
            _context = new RunnableContext(_parent?._context);
            _exceptionHandler.RunSafely(() => _context.RunInstallers(GetComponents<Installer>()));
            _exceptionHandler.ThrowIfAnyAndReset();
        }

        private void Start()
        {
            _exceptionHandler.RunSafely(_context.RunInitializables);
            _exceptionHandler.ThrowIfAnyAndReset();
        }

        private void Update()
        {
            _exceptionHandler.RunSafely(Tick);
            _exceptionHandler.ThrowIfAnyAndReset();
            return;

            void Tick() => 
                _context.RunTickables(Time.deltaTime);
        }

        protected abstract ContextRunner GetParentContextRunner();

        private void OnDestroy()
        {
            _exceptionHandler.RunSafely(_context.RunDisposables);
            _exceptionHandler.ThrowIfAnyAndReset();
        }
    }
}