using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ComputerGraphics
{
    class MyCanvas:Canvas
    {

        public void ClearCanvas() {
            this.Children.Clear();
        }

        private void DrawLines(DrawingContext dc) {
            Pen pen = new Pen(Brushes.WhiteSmoke,1);
            double dx = this.ActualWidth / 30;
            double dy = this.ActualHeight / 30;
            for (int i = 0; i < 31;i++ ) {
                dc.DrawLine(pen, new Point(dx * i, 0), new Point(dx * i, this.ActualHeight));
            }
            for (int i = 0; i < 31;i++ )
            {
                dc.DrawLine(pen, new Point(0, dy * i), new Point(this.ActualWidth, dy * i));
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            DrawLines(dc);
        }

        public void Clear()
        {
            this.Children.Clear();
        }
    }
}
