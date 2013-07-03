using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphicNotes.Workspace
{
    class ToolboxObject:ContentControl
    {

        static ToolboxObject()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolboxObject), new FrameworkPropertyMetadata(typeof(ToolboxObject)));  
        }

        public Enums.Objects Type
        {
            get { return (Enums.Objects)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
          DependencyProperty.Register("Type", typeof(Enums.Objects),
                                      typeof(ToolboxObject),
                                      new FrameworkPropertyMetadata(Enums.Objects.Text));


        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceToolboxProperty); }
            set { SetValue(SourceToolboxProperty, value); }
        }

        public static readonly DependencyProperty SourceToolboxProperty =
          DependencyProperty.Register("Source", typeof(ImageSource),
                                      typeof(ToolboxObject));

        //private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        //{
        //    ToolboxObject cc = dependencyObject as ToolboxObject;
        //    cc.ApplyTemplate();
        //    Image img = cc.Template.FindName("PART_Image", cc) as Image;
        //    Console.WriteLine("Entro con cc:" + cc.ToString());
        //    if (img != null)
        //    {
        //        var uri = new Uri(e.NewValue.ToString());
        //        var bitmap = new BitmapImage(uri);
        //        img.Source = bitmap;
        //    }
        //}

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(String),
                                      typeof(ToolboxObject),
                                      new FrameworkPropertyMetadata(""));

        //private static void OnTextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        //{
        //    ToolboxObject cc = dependencyObject as ToolboxObject;
        //    cc.ApplyTemplate();
        //    TextBlock txt = cc.Template.FindName("PART_TextBlock", cc) as TextBlock;
        //    if (txt != null)
        //    {

        //        txt.Text = e.NewValue.ToString();
        //    }
        //}
    }
}
