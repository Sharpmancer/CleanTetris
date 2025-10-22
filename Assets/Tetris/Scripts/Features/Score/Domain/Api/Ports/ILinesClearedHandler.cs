namespace Features.Score.Domain.Api
{
    public interface ILinesClearedHandler
    {
        void HandleLinesCleared(int count);
    }
}