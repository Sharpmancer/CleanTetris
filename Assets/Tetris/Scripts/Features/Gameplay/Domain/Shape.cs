using System.Collections.Generic;
using System.Linq;
using Flags = System.Reflection.BindingFlags;

namespace Features.Gameplay.Domain
{
    /// <summary>
    /// Each shape is encoded as a 16-bit mask representing a 4×4 grid.<br/>
    /// Bits are read row by row, from top to bottom, left to right:<br/>
    /// 
    /// <para>Row 1: bits 15..12  (0000 .... .... ....)<br/>
    ///       Row 2: bits 11..8   (.... 0000 .... ....)<br/>
    ///       Row 3: bits 7..4    (.... .... 0000 ....)<br/>
    ///       Row 4: bits 3..0    (.... .... .... 0000)</para>
    /// </summary>
    internal sealed class Shape
    {
        // Normalization offsets from the top-left corner
        private int _offsetX;
        private int _offsetY;
        internal int Width {get; private set;}
        internal int Height {get; private set;}
        internal ushort Mask { get; private set; }

        internal static readonly Shape I = new(0x0F00); // 0000 1111 0000 0000
        internal static readonly Shape T = new(0x0E40); // 0000 1110 0100 0000
        internal static readonly Shape O = new(0x0660); // 0000 0110 0110 0000
        internal static readonly Shape S = new(0x06C0); // 0000 0110 1100 0000
        internal static readonly Shape Z = new(0x0C60); // 0000 1100 0110 0000
        internal static readonly Shape L = new(0x08E0); // 0000 1000 1110 0000
        internal static readonly Shape J = new(0x02E0); // 0000 0010 1110 0000

        private static readonly IReadOnlyList<Shape> DefaultShapes = typeof(Shape)
            .GetFields(Flags.NonPublic | Flags.Static)
            .Where(f => f.FieldType == typeof(Shape))
            .Select(f => (Shape)f.GetValue(null)!)
            .ToArray();

        internal static Shape Random => 
            DefaultShapes[UnityEngine.Random.Range(0, DefaultShapes.Count)];
        
        internal Shape() =>
            SetNewValue(0);

        internal Shape(ushort mask) => 
            SetNewValue(mask);

        internal void WriteTo(Shape target) =>
            target.SetNewValue(Mask);

        internal IEnumerable<(int x, int y)> Cells()
        {
            for (var y = 0; y < 4; y++)
            for (var x = 0; x < 4; x++)
                if (ContainsPoint(x, y))
                    yield return new (x - _offsetX, y - _offsetY);
        }

        internal void RotateClockwise()
        {
            ushort resultMask = 0;
            for (var y = 0; y < 4; y++)
            for (var x = 0; x < 4; x++)
            {
                if (!ContainsPoint(x, y)) 
                    continue;

                var ny = 3 - x;
                var bitIndex = 15 - (ny * 4 + y);
                resultMask |= (ushort)(1 << bitIndex);
            }
            SetNewValue(resultMask);
        }

        private bool ContainsPoint(int x, int y) => 
            ((Mask >> 15 - (y * 4 + x)) & 1) != 0;

        private void SetNewValue(ushort mask)
        {
            Mask = mask;
            CalculateOffset();
            return;

            void CalculateOffset()
            {
                int minX = 4, minY = 4;
                int maxX = -1, maxY = -1;

                for (var y = 0; y < 4; y++)
                for (var x = 0; x < 4; x++)
                {
                    if (!ContainsPoint(x, y))
                        continue;

                    if (x < minX) minX = x;
                    if (y < minY) minY = y;
                    if (x > maxX) maxX = x;
                    if (y > maxY) maxY = y;
                }

                // handle empty shape (no occupied bits)
                if (maxX < 0)
                {
                    _offsetX = 0;
                    _offsetY = 0;
                    Width = 0;
                    Height = 0;
                    return;
                }

                _offsetX = minX;
                _offsetY = minY;
                Width = maxX - minX + 1;
                Height = maxY - minY + 1;
            }
        }
    }
}