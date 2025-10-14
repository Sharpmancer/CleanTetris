using UnityEngine;

namespace Libs.Bootstrap
{
    [DisallowMultipleComponent]
    internal abstract class ContextRunner : MonoBehaviour
    {
        private ContextRunner _parent;
        private RunnableContext _context;

        protected virtual void Awake()
        {
            _parent = GetParentContextRunner();
            _context = new RunnableContext(_parent?._context);
            _context.RunInstallers(GetComponents<Installer>());
        }

        protected abstract ContextRunner GetParentContextRunner();

        private void Start()
        {
            _context.RunPreInitializables();
            _context.RunInitializables();
        }

        private void Update() => 
            _context.RunTickables(Time.deltaTime);

        private void OnDestroy() => 
            _context.RunDisposables();
    }
}