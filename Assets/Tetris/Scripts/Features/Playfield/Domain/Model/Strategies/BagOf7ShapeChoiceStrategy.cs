using System.Collections.Generic;
using System.Linq;
using Libs.Core.DotNetUtils;

namespace Features.Playfield.Domain.Model
{
    internal class BagOf7ShapeChoiceStrategy : IShapeChoiceStrategy
    {
        private readonly List<Shape> _allShapes = Shape.DefaultShapes.ToList();
        private int _currentShapeIndex = -1;

        public BagOf7ShapeChoiceStrategy() => 
            _allShapes.Shuffle();

        public Shape GetNext()
        {
            if (++_currentShapeIndex >= _allShapes.Count)
            {
                _currentShapeIndex = 0;
                _allShapes.Shuffle();
            }
            return _allShapes[_currentShapeIndex];
        }
    }
}