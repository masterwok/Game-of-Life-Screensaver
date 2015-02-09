using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class Extensions
    {
        public static Tuple<int, int> GetPosition(this Position position, int row, int col, int rowCount, int colCount)
        {
            switch (position)
            {
                case Position.TopLeft:
                    row = row - 1;
                    col = col - 1;
                    break;
                case Position.Top:
                    row = row - 1;
                    break;
                case Position.TopRight:
                    row = row - 1;
                    col = col + 1;
                    break;
                case Position.Right:
                    col = col + 1;
                    break;
                case Position.BottomRight:
                    row = row + 1;
                    col = col + 1;
                    break;
                case Position.Bottom:
                    row = row + 1;
                    break;
                case Position.BottomLeft:
                    row = row + 1;
                    col = col - 1;
                    break;
                case Position.Left:
                    col = col - 1;
                    break;
            }

            row = row > -1 ? (row < rowCount ? row : row - rowCount) : rowCount - 1;
            col = col > -1 ? (col < colCount ? col : col - colCount) : colCount - 1;

            return new Tuple<int, int>(row, col);
        }
    }
}
