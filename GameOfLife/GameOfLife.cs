using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameOfLife : IEnumerator<byte[,]>, IEnumerable<byte[,]>
    {
        private int _currentGenerationCellCount { get; set; }
        private Random _random = new Random();
        private Grid _grid { get; set; }

        // Graphics vars
        public Graphics CurrentGraphics { get; set; }
        private Color _cellColor { get; set; }
        private int ColorIteration { get; set; }

        // Circle vars
        private const int MAX_CIRCLE_RADIUS = 75;
        private const double CIRCLE_DROP_THRESHOLD = 0.35;
        private int _dropCircleAtCount { get; set; }

        public GameOfLife(int windowWidth, int windowHeight, int cellWidth, double ratioAlive)
        {
            _grid = new Grid(windowWidth, windowHeight, cellWidth);
            _dropCircleAtCount = (int)((_grid.RowCount * _grid.ColumnCount * ratioAlive) * CIRCLE_DROP_THRESHOLD);
            GenerateSeedGeneration(ratioAlive);
        }

        public void GenerateSeedGeneration(double ratioAlive)
        {
            int seedAliveCellCount = (int)(_grid.RowCount * _grid.ColumnCount * ratioAlive);
            int n = 0;

            while (n < seedAliveCellCount)
            {
                // Pick random position on the grid to set alive
                int r = _random.Next(0, _grid.RowCount);
                int c = _random.Next(0, _grid.ColumnCount);

                // If the current cell isn't already alive, set it to alive
                if (!IsAlive(_grid.GetCellValue(r, c)))
                {
                    SetCellAlive(r, c);
                    n++;
                }
            }
        }

        private bool IsAlive(byte cell)
        {
            return (cell & 1) == 1;
        }

        private byte GetCellNeighborCount(byte cell)
        {
            // Get number in neighbor count area
            return (byte)((cell & 14) >> 1);
        }

        private byte SetCellNeighborCount(byte cell, byte count)
        {
            // Clears the neighbor count with AND and then sets with OR
            return ((byte)((cell & 241) | (count << 1)));
        }

        private void SetCellAlive(int row, int col)
        {
            Tuple<int, int> rowCol;

            // Do nothing if the cell is already alive
            if (IsAlive(_grid.GetCellValue(row, col)))
                return;

            // Set alive bit to 1
            _grid.SetCellValue(row, col, (byte)(_grid.GetCellValue(row, col) | 1));

            // Increment neighbor cells
            foreach (Position position in Enum.GetValues(typeof(Position)).Cast<Position>())
            {
                rowCol = position.GetPosition(row, col, _grid.RowCount, _grid.ColumnCount);
                _grid.SetCellValue(rowCol.Item1, rowCol.Item2, SetCellNeighborCount(_grid.GetCellValue(rowCol.Item1, rowCol.Item2),
                    (byte)(GetCellNeighborCount(_grid.GetCellValue(rowCol.Item1, rowCol.Item2)) + 1)));

            }
        }

        private void SetCellDead(int row, int col)
        {
            Tuple<int, int> rowCol;

            // Do nothing if the cell is already dead
            if (!IsAlive(_grid.GetCellValue(row, col)))
                return;

            // Set alive bit to 0
            _grid.SetCellValue(row, col, (byte)(_grid.GetCellValue(row, col) & 254));

            // Increment neighbor cells
            foreach (Position position in Enum.GetValues(typeof(Position)).Cast<Position>())
            {
                rowCol = position.GetPosition(row, col, _grid.RowCount, _grid.ColumnCount);
                _grid.SetCellValue(rowCol.Item1, rowCol.Item2, SetCellNeighborCount(_grid.GetCellValue(rowCol.Item1, rowCol.Item2),
                    (byte)(GetCellNeighborCount(_grid.GetCellValue(rowCol.Item1, rowCol.Item2)) - 1)));
            }
        }

        #region Circle

        /// <summary>
        /// Draw a circlce on the grid at position (r, c) with specified radius.
        /// Midpoint Circle Algorithm (http://en.wikipedia.org/wiki/Midpoint_circle_algorithm)
        /// </summary>
        /// <param name="r">Row of the center of the circle.</param>
        /// <param name="c">Column of the center of the circle.</param>
        /// <param name="radius">Radius of the circle</param>
        public void DrawCircle(int r, int c, int radius)
        {
            Tuple<int, int> pos = null;
            int x = radius;
            int y = 0;
            int radiusError = 1 - x;

            //r = c = 0;

            while (x >= y)
            {
                pos = GetWrappedPosition(x + r, y + c);
                _grid.SetCellColorAt(pos.Item1, pos.Item2, _cellColor);
                SetCellAlive(pos.Item1, pos.Item2);
                pos = GetWrappedPosition(-y + r, x + c);
                _grid.SetCellColorAt(pos.Item1, pos.Item2, _cellColor);
                SetCellAlive(pos.Item1, pos.Item2);
                pos = GetWrappedPosition(-x + r, -y + c);
                _grid.SetCellColorAt(pos.Item1, pos.Item2, _cellColor);
                SetCellAlive(pos.Item1, pos.Item2);
                pos = GetWrappedPosition(-y + r, -x + c);
                _grid.SetCellColorAt(pos.Item1, pos.Item2, _cellColor);
                SetCellAlive(pos.Item1, pos.Item2);
                pos = GetWrappedPosition(x + r, -y + c);
                _grid.SetCellColorAt(pos.Item1, pos.Item2, _cellColor);
                SetCellAlive(pos.Item1, pos.Item2);
                pos = GetWrappedPosition(y + r, -x + c);
                _grid.SetCellColorAt(pos.Item1, pos.Item2, _cellColor);
                SetCellAlive(pos.Item1, pos.Item2);
                y++;

                if (radiusError < 0)
                {
                    radiusError += 2 * y + 1;
                }
                else
                {
                    x--;
                    radiusError += 2 * (y - x + 1);
                }
            }
        }

        public void DropCircleIfCellCountHitsThreshhold()
        {
            int row, col, radius;

            if (_currentGenerationCellCount < _dropCircleAtCount)
            {
                radius = _random.Next(MAX_CIRCLE_RADIUS) + 1;
                row = _random.Next(_grid.RowCount - 1) + 1;
                col = _random.Next(_grid.ColumnCount - 1) + 1;
                DrawCircle(row, col, radius);
                DrawCircle(row, col, radius + 2);
            }
        }

        #endregion

        private Tuple<int, int> GetWrappedPosition(int row, int col)
        {
            row = row % _grid.RowCount;
            row = row < 0 ? row + _grid.RowCount : row;
            col = col % _grid.ColumnCount;
            col = col < 0 ? col + _grid.ColumnCount : col;

            return new Tuple<int, int>(row, col);
        }

        #region Color Wheel

        /// <summary>
        /// Convert a given HSV (Hue Saturation Value) to RGB(Red Green Blue) and set the led to the color
        /// Source: (http://eduardofv.com/read_post/179-Arduino-RGB-LED-HSV-Color-Wheel-)
        /// </summary>
        /// <param name="h">Hue value, integer between 0 and 360</param>
        /// <param name="s">Saturation value, double between 0 and 1</param>
        /// <param name="v">Value, double between 0 and 1</param>
        /// <returns></returns>
        public Color GetRgbFromHsv(int h, double s, double v)
        {
            double r = 0, g = 0, b = 0;
            double hf = h / 60.0;
            int i = (int)Math.Floor(hf);
            double f = hf - i;
            double pv = v * (1 - s);
            double qv = v * (1 - s * f);
            double tv = v * (1 - s * (1 - f));

            switch (i)
            {
                case 0:
                    r = v;
                    g = tv;
                    b = pv;
                    break;
                case 1:
                    r = qv;
                    g = v;
                    b = pv;
                    break;
                case 2:
                    r = pv;
                    g = v;
                    b = tv;
                    break;
                case 3:
                    r = pv;
                    g = qv;
                    b = v;
                    break;
                case 4:
                    r = tv;
                    g = pv;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = pv;
                    b = qv;
                    break;
                case 6:
                    r = v;
                    g = tv;
                    b = pv;
                    break;
                case -1:
                    r = v;
                    g = pv;
                    b = qv;
                    break;
                default:
                    r = g = b = v; // Just pretend its black/white
                    break;
            }

            return Color.FromArgb(
                Constrain((int)(255.0 * r), 0, 255),
                Constrain((int)(255.0 * g), 0, 255),
                Constrain((int)(255.0 * b), 0, 255)
            );
        }

        private int Constrain(int n, int min, int max)
        {
            if (n > max) return max;
            if (n < min) return min;
            return n;
        }

        #endregion

        public void UpdateCellColor()
        {
            _cellColor = GetRgbFromHsv(ColorIteration++, 1, 1);
            if (ColorIteration > 360) ColorIteration = 0;
        }

        #region IEnumerator

        public byte[,] Current
        {
            get { return _grid.Cells; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        object IEnumerator.Current
        {
            get { return _grid.Cells; }
        }

        public bool MoveNext()
        {
            byte[,] previousGenerationCells = new byte[_grid.RowCount, _grid.ColumnCount];
            
            _grid.ClearBitmap();
            UpdateCellColor();

            // Drop circle in previous generation
            //DropCircleIfCellCountHitsThreshhold();

            // Copy generation into new grid
            Array.Copy(_grid.Cells, previousGenerationCells, _grid.Cells.Length);
            _currentGenerationCellCount = 0;

            // Draw normal generation
            for (int r = 0; r < _grid.RowCount; r++)
            {
                for (int c = 0; c < _grid.ColumnCount; c++)
                {
                    // Only look at non zero cells
                    if (previousGenerationCells[r, c] != 0)
                    {
                        byte count = GetCellNeighborCount(previousGenerationCells[r, c]);
                        if (IsAlive(previousGenerationCells[r, c]))
                        {
                            if ((count != 2) && (count != 3))
                                SetCellDead(r, c); // clear cell
                            else
                            {
                                _grid.SetCellColorAt(r, c, _cellColor);
                                _currentGenerationCellCount++;
                            }
                        }
                        else
                        {
                            if (count == 3)
                            {
                                SetCellAlive(r, c); // set sell
                                _grid.SetCellColorAt(r, c, _cellColor);
                                _currentGenerationCellCount++;
                            }
                        }
                    }

                }
            }

            DropCircleIfCellCountHitsThreshhold();
            _grid.DrawBitmap(CurrentGraphics);

            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable

        public IEnumerator<byte[,]> GetEnumerator()
        {
            return (IEnumerator<byte[,]>)this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion


    }
}
