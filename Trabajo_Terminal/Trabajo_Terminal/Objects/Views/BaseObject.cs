using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using GraphicNotes.Workspace;
using System.Windows.Media;
using GraphicNotes.Adorners;

namespace GraphicNotes.Objects.Views
{
    class BaseObject: ContentControl
    {
    

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected", typeof(bool),
                                      typeof(BaseObject),
                                      new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty MoveThumbTemplateProperty =
            DependencyProperty.RegisterAttached("MoveThumbTemplate", typeof(ControlTemplate), typeof(BaseObject));

        public static ControlTemplate GetMoveThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(MoveThumbTemplateProperty);
        }

        public static void SetMoveThumbTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(MoveThumbTemplateProperty, value);
        }

        static BaseObject()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseObject), new FrameworkPropertyMetadata(typeof(BaseObject)));
        }

        public BaseObject()
        {
            this.Loaded += new RoutedEventHandler(this.BaseObject_Loaded);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            CanvasWorkspace canvasWorkspace = VisualTreeHelper.GetParent(this) as CanvasWorkspace;

            if (canvasWorkspace != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                {
                    this.IsSelected = !this.IsSelected;
                }
                else
                {
                    if (!this.IsSelected)
                    {
                        canvasWorkspace.DeselectAll();
                        this.IsSelected = true;
                    }
                }
            }

            e.Handled = false;
        }

        


        private void BaseObject_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Base Object Loaded");
            if (this.Template != null)
            {
                ContentPresenter contentPresenter =
                    this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;

                MoveThumb thumb =
                    this.Template.FindName("PART_MoveThumb", this) as MoveThumb;

                if (contentPresenter != null && thumb != null)
                {
                    UIElement contentVisual =
                        VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;

                    if (contentVisual != null)
                    {
                        ControlTemplate template =
                            BaseObject.GetMoveThumbTemplate(contentVisual) as ControlTemplate;

                        if (template != null)
                        {
                            thumb.Template = template;
                        }
                    }
                }
                ResizeDecorator resizeDecorator =
                    this.Template.FindName("PART_BaseObjectDecorator", this) as ResizeDecorator;
                if (resizeDecorator != null)
                    resizeDecorator.ShowAdorner();
            }
                
        }
       

    }
}
