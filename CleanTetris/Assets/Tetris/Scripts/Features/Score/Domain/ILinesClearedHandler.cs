namespace Features.Score.Domain
{
    public interface ILinesClearedHandler
    {
        void HandleLinesCleared(int count);
    }
}