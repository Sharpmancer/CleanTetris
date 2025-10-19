using System;

namespace Features.Playfield.App
{
    public interface IGameOverDialogueView
    {
        void Show(Action onRestartClicked, Action onMainMenuClicked);
    }
}