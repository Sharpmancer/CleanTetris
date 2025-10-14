using Libs.Bootstrap;
using Libs.Persistence;

namespace Features.Persistence.Composition
{
    public class ProjectContextPersistenceInstaller : Installer
    {
        public override void Install(IInstallableContext context)
        {
            var saver = new PersistentDataHandler();
            context.RegisterContract<ISaver>(saver);
            context.RegisterContract<ILoader>(saver);
            context.RegisterContract<ISaveDeleter>(saver);
        }
    }
}