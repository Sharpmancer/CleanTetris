namespace Features.Playfield.Domain.States
{
    internal class CompactBoardState : PlayfieldState
    {
        internal override void Enter(Playfield gameplay)
        {
            var board = gameplay.Board;
            var write = board.Rows - 1;
            var anyChange = false;

            for (var read = board.Rows - 1; read >= 0; read--)
            {
                if (board.RowIsEmpty(read)) 
                    continue;

                if (read != write)
                {
                    board.SwapRows(read, write);
                    anyChange = true;
                }

                write--;
            }

            if (anyChange)
                gameplay.HandleBoardStateChanged();

            gameplay.ChangeState<IdleState>();
        }
    }
}