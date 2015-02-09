using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public class Grid
    {
        private int _windowHeight { get; set; }
        private int _windowWidth { get; set; }
        private Graphics BitmapGraphics { get; set; }
        private SolidBrush Brush { get; set; }
        private Bitmap Bitmap { get; set; }
        public Point TopLeft { get; private set; }
        public int Padding { get; private set; }
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        public int CellSideLength { get; private set; }

        private int BoxHeight { get; set; }
        private int BoxWidth { get; set; }


        public Grid(int windowWidth, int windowHeight, int cellWidth)
        {
            this._windowHeight = windowHeight;
            this._windowWidth = windowWidth;
            this.CellSideLength = cellWidth;
            this.Brush = new SolidBrush(Color.CornflowerBlue);
            this.SetDimensions();
        }

        private int GetCellCountInSize(int cellSideLength, int size)
        {
            return (size - (size % cellSideLength)) / cellSideLength;
        }

        private void SetDimensions()
        {
            // Find row and column count based upon the size of the cell
            ColumnCount = GetCellCountInSize(CellSideLength, _windowWidth);
            RowCount = GetCellCountInSize(CellSideLength, _windowHeight);

            BoxWidth = ColumnCount * CellSideLength;
            BoxHeight = RowCount * CellSideLength;

            var paddingHorizontal = (_windowWidth - BoxWidth) / 2;
            var paddingVertical = (_windowHeight - BoxHeight) / 2;

            TopLeft = new Point(paddingHorizontal, paddingVertical);
        }

        public void ClearBitmap()
        {
            Bitmap = new Bitmap(BoxWidth, BoxHeight);
            if (BitmapGraphics != null)
                BitmapGraphics.Dispose();
            BitmapGraphics = Graphics.FromImage(Bitmap);
        }

        public void SetCellColorAt(int row, int col, Color color)
        {
            Brush.Color = color;
            int x = (col * CellSideLength);
            int y = (row * CellSideLength);

            BitmapGraphics.FillRectangle(Brush, x, y, CellSideLength, CellSideLength);
        }

        public void SetCellColorAt(Tuple<int, int> position, Color color)
        {
            SetCellColorAt(position.Item1, position.Item2, color);
        }

        public void DrawBitmap(Graphics g)
        {
            g.DrawImage(Bitmap, TopLeft);
        }


    }
}
