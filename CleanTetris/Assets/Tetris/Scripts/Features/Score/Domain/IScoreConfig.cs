namespace Features.Score.Domain
{
    public interface IScoreConfig
    {
        int PointsForOneRow { get; }
        int PointsForTwoRows { get; }
        int PointsForThreeRows { get; }
        int PointsForFourRows { get; }
    }
}