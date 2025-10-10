using System;

namespace Features.Gameplay.App
{
    public interface IGameOverDialogueView
    {
        void Show(Action onRestartClicked, Action onMainMenuClicked);
    }
}