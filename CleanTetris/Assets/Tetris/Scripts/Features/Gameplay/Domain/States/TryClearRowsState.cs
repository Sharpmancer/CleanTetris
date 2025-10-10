using Libs.Core;

namespace Features.Gameplay.Domain.States
{
    internal class TryClearRowsState : GameplayState
    {
        internal override void Enter(GameplayMediator gameplay)
        {
            var rowsCleared = new UpToFourBytes();
            var board = gameplay.Board;

            for (var i = 0; i < board.Rows; i++)
            {
                if(!board.RowIsFull(i))
                    continue;
                board.ClearRow(i);
                rowsCleared.Add((byte)i);
            }

            if (rowsCleared.Count > 0)
            {
                gameplay.HandleBoardStateChanged();
                gameplay.HandleRowsCleared(rowsCleared);
                gameplay.ChangeState<CompactBoardState>();
            }
            else
                gameplay.ChangeState<IdleState>();
        }
    }
}