namespace Features.Score.Domain.Api
{
    public interface IScoreProvider
    {
        int ScorePoints { get; }
    }
}