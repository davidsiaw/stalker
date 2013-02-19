using System;
using FogBugzNet;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using System.Configuration;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private FogBugz _fb;

        private bool _resizing = false;
        private AutoUpdater _autoUpdate;

        private bool _dragging = false;
        private DateTime _startDragTime;
        private int _mouseDownX;

        private int _mouseDownY;
        private int _dragDistance = 0;

        private int _gripStartX;


        public HoverWindow()
        {
            InitializeComponent();
        }

        private void startDragging(MouseEventArgs e)
        {
            _startDragTime = DateTime.Now;

            _dragDistance = 0;
            _mouseDownX = e.X;
            _mouseDownY = e.Y;
            _dragging = true;
        }
        private bool atScreenEdge(Point p)
        {
            return (p.X <= 0) || (p.Y <= 0);

        }

        private void moveWindow(Point p)
        {

            Rectangle screen = Screen.FromControl(this).WorkingArea;
            int SnapSize = 10;
            if (Screen.FromPoint(p) != null)
            {
                screen = Screen.FromPoint(p).WorkingArea;
            }
            if (p.X < screen.Left + SnapSize)
                p.X = screen.Left;
            if (p.X + Width > screen.Right - SnapSize)
                p.X = screen.Right - Width;

            if (p.Y < screen.Top + SnapSize)
                p.Y = screen.Top;
            if (p.Y + Height > screen.Bottom - SnapSize)
                p.Y = screen.Bottom - Height;

            Location = p;
        }

        private void dragWindow(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point();
                // Measure drag distance
                _dragDistance += (int)Math.Sqrt((e.X - _mouseDownX) * (e.X - _mouseDownX) + (e.Y - _mouseDownY) * (e.Y - _mouseDownY));

                // Measure drag speed
                long ticks = DateTime.Now.Subtract(_startDragTime).Milliseconds;
                if (ticks > 0)
                {
                    double speed = (double)_dragDistance / ticks;

                    // Not doing anything with the drag speed for now
                    // Might use it to implement mouse gestures
                }
                _dragDistance = 0;
                _startDragTime = DateTime.Now;


                p.X = Location.X + (e.X - _mouseDownX);
                p.Y = Location.Y + (e.Y - _mouseDownY);
                moveWindow(p);
            }

        }

        private void MoveWindowToCenter()
        {
            Point p = new Point();
            p.X = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            p.Y = 0;
            Location = p;
        }

        private void ResizeWidth()
        {
            Width += Cursor.Position.X - _gripStartX;
            _gripStartX = Cursor.Position.X;
        }

        private void CloseApplication()
        {
            Utils.Log.Debug("Shutting down...");
            try
            {
                if (_settings.SwitchToNothingWhenClosing)
                    _fb.StopWorking();
                else
                    Utils.Log.Debug("User continues to work on case after closing.");
                saveSettings();
            }
            catch (System.Exception x)
            {
                Utils.Log.Error(x.ToString());
            }
        }
        
    } // class HoverWindow
} // ns FogBugzCaseTracker
