using System.Collections.Generic;
using System.Linq;
using Libs.Core.Rng;

namespace Features.Playfield.Domain.Model
{
    internal class RandomShapeChoiceStrategy : IShapeChoiceStrategy
    {
        private readonly List<Shape> _allShapes = Shape.DefaultShapes.ToList();
        private readonly IRng _rng;

        public RandomShapeChoiceStrategy(IRng rng) => 
            _rng = rng;

        public Shape GetNext() => 
            _allShapes[_rng.RandomInt(0, _allShapes.Count)];
    }
}