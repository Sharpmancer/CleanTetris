using Libs.UnityUtils;
using UnityEngine;

namespace Libs.Bootstrap
{
    internal class SceneContextRunner : ContextRunner
    {
        [Header("Fill this if you want to be able to start the game from this scene")]
        [SerializeField, AssetOnly] private ProjectContextRunner _projectContextPrefab; 
        
        protected override ContextRunner GetParentContextRunner() => 
            ProjectContextRunner.GetOrCreateInstance(_projectContextPrefab);
    }
}