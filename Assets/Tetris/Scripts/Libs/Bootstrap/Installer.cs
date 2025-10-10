using UnityEngine;

namespace Libs.Bootstrap
{
    public abstract class Installer : MonoBehaviour
    {
        public abstract void Install(IInstallableContext context);
    }
}