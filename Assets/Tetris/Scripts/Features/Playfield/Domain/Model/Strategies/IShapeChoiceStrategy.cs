namespace Features.Playfield.Domain.Model
{
    internal interface IShapeChoiceStrategy
    {
        Shape GetNext();
    }
}