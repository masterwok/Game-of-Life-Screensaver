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
        public byte[,] Cells { get; private set; }
        private int CurrentGenerationCellCount { get; set; }
        private Random random = new Random();
        public Grid Grid { get; set; }

        // Graphics vars
        public Graphics CurrentGraphics { get; set; }
        private Color _cellColor { get; set; }
        private int ColorIteration { get; set; }

        // Circle vars
        private const int MAX_CIRCLE_RADIUS = 40;
        private const double CIRCLE_DROP_THRESHOLD = 0.25;
        private int _dropCircleAtCount { get; set; }

        public GameOfLife(int windowWidth, int windowHeight, int cellWidth, double ratioAlive)
        {
            Grid = new Grid(windowWidth, windowHeight, cellWidth);
            _dropCircleAtCount = (int)((Grid.RowCount * Grid.ColumnCount * ratioAlive) * CIRCLE_DROP_THRESHOLD);
            GenerateSeedGeneration(ratioAlive);
        }

        public void GenerateSeedGeneration(double ratioAlive)
        {
            Cells = new byte[Grid.RowCount, Grid.ColumnCount];
            int aliveCells = (int)(Grid.RowCount * Grid.ColumnCount * ratioAlive);
            int n = 0;

            if (aliveCells > Grid.RowCount * Grid.ColumnCount)
                throw new Exception("Number of alive cells must be less or equal to number of total cells");

            while (n < aliveCells)
            {
                int r = random.Next(0, Grid.RowCount);
                int c = random.Next(0, Grid.ColumnCount);

                if (!IsAlive(Cells[r, c]))
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
            Cells[row, col] = (byte)(Cells[row, col] | 1);

            // Increment neighbor cells
            foreach (Position position in Enum.GetValues(typeof(Position)).Cast<Position>())
            {
                rowCol = position.GetPosition(row, col, Grid.RowCount, Grid.ColumnCount);
                Cells[rowCol.Item1, rowCol.Item2] = SetCellNeighborCount(Cells[rowCol.Item1, rowCol.Item2],
                    (byte)(GetCellNeighborCount(Cells[rowCol.Item1, rowCol.Item2]) + 1));
            }
        }

        private void SetCellAlive(Tuple<int, int> position)
        {
            SetCellAlive(position.Item1, position.Item2);
        }

        private void SetCellDead(int row, int col)
        {
            Tuple<int, int> rowCol;
            Cells[row, col] = (byte)(Cells[row, col] & 254);

            // Increment neighbor cells
            foreach (Position position in Enum.GetValues(typeof(Position)).Cast<Position>())
            {
                rowCol = position.GetPosition(row, col, Grid.RowCount, Grid.ColumnCount);
                Cells[rowCol.Item1, rowCol.Item2] = SetCellNeighborCount(Cells[rowCol.Item1, rowCol.Item2],
                    (byte)(GetCellNeighborCount(Cells[rowCol.Item1, rowCol.Item2]) - 1));
            }
        }

        /// <summary>
        /// Draw a circlce on the grid at position (r, c) with specified radius.
        /// Midpoint Circle Algorithm (http://en.wikipedia.org/wiki/Midpoint_circle_algorithm)
        /// </summary>
        /// <param name="r">Row of the center of the circle.</param>
        /// <param name="c">Column of the center of the circle.</param>
        /// <param name="radius">Radius of the circle</param>
        public void DrawCircle(int r, int c, int radius)
        {
            int x = radius;
            int y = 0;
            int radiusError = 1 - x;
            Tuple<int, int> pos = null;

            while (x >= y)
            {
                pos = GetWrappedPosition(x + r, y + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(y + r, x + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-x + r, y + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-y + r, x + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-x + r, -y + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-y + r, -x + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(x + r, -y + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(y + r, -x + c);
                Grid.SetCellColorAt(pos, _cellColor);
                SetCellAlive(pos);
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

        private Tuple<int, int> GetWrappedPosition(int row, int col)
        {
            row = row % Grid.RowCount;
            row = row < 0 ? row + Grid.RowCount : row;
            col = col % Grid.ColumnCount;
            col = col < 0 ? col + Grid.ColumnCount : col;

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

        public void HandleCircleDrop()
        {
            int row, col, radius;

            //if (CurrentGenerationCellCount < _dropCircleAtCount)
            //{
            //    radius = random.Next(MAX_CIRCLE_RADIUS) + 1;
            //    row = random.Next(Grid.RowCount - 1) + 1;
            //    col = random.Next(Grid.ColumnCount - 1) + 1;
            //    DrawCircle(row, col, radius);
            //}
        }

        #region IEnumerator

        public byte[,] Current
        {
            get { return Cells; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        object IEnumerator.Current
        {
            get { return Cells; }
        }

        public bool MoveNext()
        {
            byte[,] tmpCells = new byte[Grid.RowCount, Grid.ColumnCount];
            Array.Copy(Cells, tmpCells, Cells.Length);

            Grid.ClearBitmap();
            UpdateCellColor();

            // Draw normal generation
            for (int r = 0; r < Grid.RowCount; r++)
            {
                for (int c = 0; c < Grid.ColumnCount; c++)
                {
                    // Only look at non zero cells
                    if (tmpCells[r, c] != 0)
                    {
                        byte count = GetCellNeighborCount(tmpCells[r, c]);
                        if (IsAlive(tmpCells[r, c]))
                        {
                            if ((count != 2) && (count != 3))
                                SetCellDead(r, c); // clear cell
                            else
                            {
                                Grid.SetCellColorAt(r, c, _cellColor);
                                CurrentGenerationCellCount++;
                            }
                        }
                        else
                        {
                            if (count == 3)
                            {
                                SetCellAlive(r, c); // set sell
                                Grid.SetCellColorAt(r, c, _cellColor);
                                CurrentGenerationCellCount++;
                            }
                        }
                    }

                }
            }

            HandleCircleDrop();
            CurrentGenerationCellCount = 0;

            Grid.DrawBitmap(CurrentGraphics);

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
