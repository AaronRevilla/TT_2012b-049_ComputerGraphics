using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using GraphicNotes.Objects.Views;
using System.Windows.Input;
using System.Windows.Documents;
using GraphicNotes.Adorners;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphicNotes.Workspace
{
    class CanvasWorkspace:Canvas
    {
        private Point? dragStartPoint = null;

        public IEnumerable<BaseObject> SelectedElements
        {
            get
            {
                var selectedElements = from element in this.Children.OfType<BaseObject>()
                                    where element.IsSelected == true
                                    select element;
                
                return selectedElements;
            }
        }

        public void DeselectAll()
        {
            foreach (BaseObject item in this.SelectedElements)
            {
                item.IsSelected = false;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                this.dragStartPoint = new Point?(e.GetPosition(this));
                this.DeselectAll();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                this.dragStartPoint = null;
            }

            if (this.dragStartPoint.HasValue)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, this.dragStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }

                e.Handled = true;
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {

            base.OnDrop(e);
            int? type = (int)(e.Data.GetData("ObjectType"));
            if (type!=null)
            {
                Grid grid = new Grid() { Background = Brushes.Azure, Width=65, Height=65 };
                TextObject ob = new TextObject() { Width = 65, Height = 65 };
                ob.Content = grid;
                Console.WriteLine("Elemento Primario:" +LogicalTreeHelper.GetChildren(ob));
                Point position = e.GetPosition(this);
                CanvasWorkspace.SetLeft(ob, Math.Max(0, position.X - ob.Width / 2));
                CanvasWorkspace.SetTop(ob, Math.Max(0, position.Y - ob.Height / 2));
                this.Children.Add(ob);
                this.DeselectAll();
                ob.IsSelected = true;
                
            }

             e.Handled = true;
            
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();
            foreach (UIElement element in Children)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }

            // add some extra margin
            size.Width += 10;
            size.Height += 10;
            return size;
        }

    }
}
