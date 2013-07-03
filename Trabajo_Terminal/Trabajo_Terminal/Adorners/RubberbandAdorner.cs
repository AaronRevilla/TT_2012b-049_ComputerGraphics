using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Shapes;
using GraphicNotes.Workspace;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using GraphicNotes.Objects.Views;


namespace GraphicNotes.Adorners
{
    class RubberbandAdorner:Adorner
    {
        private Point? startPoint, endPoint;
        private Rectangle rubberband;
        private CanvasWorkspace canvasWorkspace;
        private VisualCollection visuals;
        private Canvas adornerCanvas;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        public RubberbandAdorner(CanvasWorkspace canvasWorkspace, Point? dragStartPoint)
            : base(canvasWorkspace)
        {
            this.canvasWorkspace = canvasWorkspace;
            this.startPoint = dragStartPoint;

            this.adornerCanvas = new Canvas();
            this.adornerCanvas.Background = Brushes.Transparent;
            this.visuals = new VisualCollection(this);
            this.visuals.Add(this.adornerCanvas);

            this.rubberband = new Rectangle();
            this.rubberband.Stroke = Brushes.Navy;
            this.rubberband.StrokeThickness = 1;
            this.rubberband.StrokeDashArray = new DoubleCollection(new double[] { 2 });

            this.adornerCanvas.Children.Add(this.rubberband);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                {
                    this.CaptureMouse();
                }

                this.endPoint = e.GetPosition(this);
                this.UpdateRubberband();
                this.UpdateSelection();
                e.Handled = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }

            AdornerLayer adornerLayer = this.Parent as AdornerLayer;
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.adornerCanvas.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }

        private void UpdateRubberband()
        {


            double left = Math.Min(this.startPoint.Value.X, this.endPoint.Value.X < 0 ? 0 : this.endPoint.Value.X);
            double top = Math.Min(this.startPoint.Value.Y, this.endPoint.Value.Y < 0 ? 0 : this.endPoint.Value.Y);

            double width = Math.Abs(this.startPoint.Value.X - (this.endPoint.Value.X < 0 ? 0 : this.endPoint.Value.X));
            double height = Math.Abs(this.startPoint.Value.Y - (this.endPoint.Value.Y < 0 ? 0 : this.endPoint.Value.Y));

            if (left + width > canvasWorkspace.ActualWidth)
                this.rubberband.Width = canvasWorkspace.ActualWidth-left;
            else
                this.rubberband.Width = width;

            if (top + height > canvasWorkspace.ActualHeight)
                this.rubberband.Height = canvasWorkspace.ActualHeight - top;
            else
                this.rubberband.Height = height;


            Canvas.SetLeft(this.rubberband, left);
            Canvas.SetTop(this.rubberband, top);
        }

        private void UpdateSelection()
        {
            Rect rubberBand = new Rect(this.startPoint.Value, this.endPoint.Value);
            foreach (BaseObject element in this.canvasWorkspace.Children)
            {
                Rect itemRect = VisualTreeHelper.GetDescendantBounds(element);
                Rect itemBounds = element.TransformToAncestor(canvasWorkspace).TransformBounds(itemRect);

                if (rubberBand.Contains(itemBounds))
                {
                    element.IsSelected = true;
                }
                else
                {
                    element.IsSelected = false;
                }
            }
            
        }
    }
}
