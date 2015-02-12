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
        public Point TopLeft { get; private set; }
        public byte[,] Cells { get; private set; }
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }
        public int CellSideLength { get; private set; }
        public int Padding { get; private set; }

        private Graphics _bitmapGraphics { get; set; }
        private SolidBrush _brush { get; set; }
        private Bitmap _bitmap { get; set; }
        private int _windowHeight { get; set; }
        private int _windowWidth { get; set; }
        private int _boxHeight { get; set; }
        private int _boxWidth { get; set; }
        
        public Grid(int windowWidth, int windowHeight, int cellWidth)
        {
            this._windowHeight = windowHeight;
            this._windowWidth = windowWidth;
            this.CellSideLength = cellWidth;
            this._brush = new SolidBrush(Color.CornflowerBlue);
            this.SetDimensions();
            this.Cells = new byte[RowCount, ColumnCount];
        }

        #region Sizing

        private int GetCellCountInSize(int cellSideLength, int size)
        {
            return (size - (size % cellSideLength)) / cellSideLength;
        }

        private void SetDimensions()
        {
            // Find row and column count based upon the size of the cell
            ColumnCount = GetCellCountInSize(CellSideLength, _windowWidth);
            RowCount = GetCellCountInSize(CellSideLength, _windowHeight);

            _boxWidth = ColumnCount * CellSideLength;
            _boxHeight = RowCount * CellSideLength;

            var paddingHorizontal = (_windowWidth - _boxWidth) / 2;
            var paddingVertical = (_windowHeight - _boxHeight) / 2;

            TopLeft = new Point(paddingHorizontal, paddingVertical);
        }

        #endregion

        #region Graphics

        public void ClearBitmap()
        {
            _bitmap = new Bitmap(_boxWidth, _boxHeight);
            if (_bitmapGraphics != null)
                _bitmapGraphics.Dispose();
            _bitmapGraphics = Graphics.FromImage(_bitmap);
        }

        public void SetCellColorAt(int row, int col, Color color)
        {
            _brush.Color = color;
            int x = (col * CellSideLength);
            int y = (row * CellSideLength);

            _bitmapGraphics.FillRectangle(_brush, x, y, CellSideLength, CellSideLength);
        }

        public void DrawBitmap(Graphics g)
        {
            g.DrawImage(_bitmap, TopLeft);
        }

        #endregion

        public void SetCellValue(int row, int col, byte value)
        {
            Cells[row, col] = value;
        }

        public byte GetCellValue(int row, int col)
        {
            return Cells[row, col];
        }
    }
}
