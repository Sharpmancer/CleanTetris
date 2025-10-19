using Libs.Core.Primitives;

namespace Features.Playfield.Domain.States
{
    internal class TryClearRowsState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay)
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