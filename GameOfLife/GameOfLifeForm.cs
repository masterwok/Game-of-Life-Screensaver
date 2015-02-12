using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class GameOfLifeForm : Form
    {
        #region DLL Imports

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        #endregion

        public int CellSide { get; set; }
        public event EventHandler DrawingComplete;

        private GameOfLife _gameOfLife { get; set; }
        private Point _mouseLocation;
        private bool _isPreview { get; set; }
        private const int FRAMES_PER_SECOND = 1000;
        private const int SKIP_TICKS = 1000 / FRAMES_PER_SECOND;
        private int _nextGameTick = Environment.TickCount;

        public GameOfLifeForm(Rectangle bounds)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            Cursor.Hide();
            Bounds = bounds;
            //TopMost = true;
            KeyPreview = true;

            _gameOfLife = new GameOfLife(bounds.Width, bounds.Height, 5, .15);
            DrawingComplete += DrawingEvent;
        }

        public GameOfLifeForm(IntPtr previewHandle)
        {
            Rectangle parentRect;
            _isPreview = true;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.DoubleBuffer, true);

            // Make process DPI aware (solve preview scaling issue)
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            InitializeComponent();

            // Set the preview window as the parent of this window
            SetParent(this.Handle, previewHandle);

            // Make this a child window so it will close when the parent dialog closes
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            GetClientRect(previewHandle, out parentRect);

            Size = parentRect.Size;
            //Size = new Size(parentRect.Width, parentRect.Height);
            Location = new Point(0, 0);
            _gameOfLife = new GameOfLife(parentRect.Width, parentRect.Height, 4, 0.15);

            DrawingComplete += DrawingEvent;
        }

        public void DrawingEvent(Object sender, EventArgs e)
        {
            this.Invalidate();

            // Enforce FPS
            _nextGameTick += SKIP_TICKS;
            var sleepTime = _nextGameTick - Environment.TickCount;
            if (sleepTime > 0)
                Thread.Sleep(sleepTime);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _gameOfLife.CurrentGraphics = e.Graphics;
            _gameOfLife.MoveNext();
            DrawingComplete(this, EventArgs.Empty);
        }

        private void GameOfLifeForm_Click(object sender, EventArgs e)
        {
            if (!_isPreview)
                Application.Exit();
        }

        private void GameOfLifeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_isPreview)
                Application.Exit();
        }

        private void GameOfLifeForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!_isPreview)
                Application.Exit();
        }

        private void GameOfLifeForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isPreview)
                return;

            if (!_mouseLocation.IsEmpty)
            {
                // check for significant difference
                if (Math.Abs(_mouseLocation.X - e.X) > 5 ||
                    Math.Abs(_mouseLocation.Y - e.Y) > 5)
                    Application.Exit();
            }

            // update location
            _mouseLocation = e.Location;
        }

    }
}
