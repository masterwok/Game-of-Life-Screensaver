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
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }
        private Random random = new Random();
        public Grid Grid { get; set; }
        public Graphics CurrentGraphics { get; set; }
        public Color CellColor { get; private set; }
        public Color GridColor { get; private set; }
        private int ColorIteration { get; set; }

        public GameOfLife(int windowWidth, int windowHeight, int cellWidth, double ratioAlive)
        {
            Grid = new Grid(windowWidth, windowHeight, cellWidth);
            RowCount = Grid.RowCount;
            ColumnCount = Grid.ColumnCount;
            GenerateSeedGeneration(ratioAlive);
        }

        public void GenerateSeedGeneration(double ratioAlive)
        {
            Cells = new byte[RowCount, ColumnCount];
            int aliveCells = (int)(RowCount * ColumnCount * ratioAlive);
            int n = 0;


            if (aliveCells > RowCount * ColumnCount)
                throw new Exception("Number of alive cells must be less or equal to number of total cells");

            while (n < aliveCells)
            {
                int r = random.Next(0, RowCount);
                int c = random.Next(0, ColumnCount);

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

        public void DebugGame()
        {
            for (var r = 0; r < RowCount; r++)
            {
                for (var c = 0; c < ColumnCount; c++)
                    //Console.Write(Cells[r, c] + " ");
                    //Console.Write(GetCellNeighborCount(Cells[r, c]) + " ");
                    Console.Write((IsAlive(Cells[r, c]) ? "1" : "0") + " ");
                Console.WriteLine();
            }
        }

        private void SetCellAlive(int row, int col)
        {
            Tuple<int, int> rowCol;
            Cells[row, col] = (byte)(Cells[row, col] | 1);

            // Increment neighbor cells
            foreach (Position position in Enum.GetValues(typeof(Position)).Cast<Position>())
            {
                rowCol = position.GetPosition(row, col, RowCount, ColumnCount);
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
                rowCol = position.GetPosition(row, col, RowCount, ColumnCount);
                Cells[rowCol.Item1, rowCol.Item2] = SetCellNeighborCount(Cells[rowCol.Item1, rowCol.Item2],
                    (byte)(GetCellNeighborCount(Cells[rowCol.Item1, rowCol.Item2]) - 1));
            }
        }

        public void DrawCircle(int r, int c, int radius)
        {
            int x = radius;
            int y = 0;
            int radiusError = 1 - x;
            Tuple<int, int> pos = null;

            while (x >= y)
            {
                pos = GetWrappedPosition(x + r, y + c);
                Grid.SetCellColorAt(pos, CellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(y + r, x + c);
                Grid.SetCellColorAt(pos, CellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-x + r, y + c);
                Grid.SetCellColorAt(pos, CellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-y + r, x + c);
                Grid.SetCellColorAt(pos, CellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-x + r, -y + c);
                Grid.SetCellColorAt(pos, CellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(-y + r, -x + c);
                Grid.SetCellColorAt(pos, CellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(x + r, -y + c);
                Grid.SetCellColorAt(pos, CellColor);
                SetCellAlive(pos);
                pos = GetWrappedPosition(y + r, -x + c);
                Grid.SetCellColorAt(pos, CellColor);
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
            //int rowDiff = RowCount - row;
            //int colDiff = ColumnCount - col;
            //return new Tuple<int, int>(
            //    row > -1 ? (row < RowCount ? row : row - RowCount) : RowCount - Math.Abs(row)
            //    , col > -1 ? (col < ColumnCount ? col : col - ColumnCount) : ColumnCount - Math.Abs(col)
            //);

            if (col < 0 || col >= ColumnCount - 1)
                col = Math.Abs(col % ColumnCount);
            if (row < 0 || row >= RowCount - 1)
                row = Math.Abs(row % RowCount);

            if (col == ColumnCount || row == RowCount)
                return null;

            return new Tuple<int, int>(col, row);

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
            CellColor = GetRgbFromHsv(ColorIteration++, 1, 1);
            if (ColorIteration > 360) ColorIteration = 0;
        }

        public void HandleCircleDrop()
        {
            //int row, col, radius;

            //if (InitialCellCount - (InitialCellCount / 2) > CurrentGenerationCellCount)
            //{
            //    radius = random.Next(MAX_CIRCLE_RADIUS) + 1;
            //    row = random.Next(RowCount - 1) + 1;
            //    col = random.Next(ColumnCount - 1) + 1;
            //    DrawCircle(row, col, radius);
            //    DrawCircle(row, col, radius + 2);
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
            byte[,] tmpCells = new byte[RowCount, ColumnCount];
            Array.Copy(Cells, tmpCells, Cells.Length);

            Grid.ClearBitmap();
            UpdateCellColor();

            // Draw normal generation
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
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
                                Grid.SetCellColorAt(r, c, CellColor);
                                CurrentGenerationCellCount++;
                            }
                        }
                        else
                        {
                            if (count == 3)
                            {
                                SetCellAlive(r, c); // set sell
                                Grid.SetCellColorAt(r, c, CellColor);
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
