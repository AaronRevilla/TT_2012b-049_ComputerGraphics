using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;





namespace ComputerGraphics
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flagSecClick = false;
        
        public MainWindow()
        {
            InitializeComponent();
          
     
        }

        private void onClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(canvasDraw);
            if (rb1.IsChecked==true)
            {
                canvasDraw.Clear();
                
                double dx = canvasDraw.ActualWidth / 30;
                double dy = canvasDraw.ActualHeight / 30;
                int y = (int)(p.Y/dy);
                int x = (int)(p.X / dx);
                p0x.Text = x.ToString();
                p0y.Text = y.ToString();
                drawRectangle(x * dx, y * dy, dx, dy, Colors.Red);
                    if(p1x.Text != "" && p1y.Text!=""){
                        int x1 = (int)Double.Parse(p1x.Text);
                        int y1 = (int)Double.Parse(p1y.Text);
                        drawRectangle(x1 * dx, y1 * dy, dx, dy, Colors.Blue);
                    }
            }
            else {
                canvasDraw.Clear();
                double dx = canvasDraw.ActualWidth / 30;
                double dy = canvasDraw.ActualHeight / 30;
                int y = (int)(p.Y / dy);
                int x = (int)(p.X / dx);
                p1x.Text = x.ToString();
                p1y.Text = y.ToString();
                drawRectangle(x * dx, y * dy, dx, dy, Colors.Blue);
                if (p0x.Text != "" && p0y.Text != "")
                {
                    int x1 = (int)Double.Parse(p0x.Text);
                    int y1 = (int)Double.Parse(p0y.Text);
                    drawRectangle(x1 * dx, y1 * dy, dx, dy, Colors.Red);
                }
            }

            /*if (flagSecClick)
            {
                dibujaLinea();
                flagSecClick = false;
            }
            else
            {
                flagSecClick = true;
            }*/
            
        }

        private void drawRectangle(double x, double y,double width, double height,Color c) {
            Rectangle myRectangle = new Rectangle();
            SolidColorBrush myBrush = new SolidColorBrush();
            myBrush.Color = c;
            myRectangle.Fill = myBrush;
            myRectangle.Width = width;
            myRectangle.Height = height;
            myRectangle.Stretch = Stretch.Fill;
            myRectangle.StrokeThickness = 3;
            myRectangle.Stroke = Brushes.Black;
            Canvas.SetTop(myRectangle, y);
            Canvas.SetLeft(myRectangle, x);
            canvasDraw.Children.Add(myRectangle);
        }

        private void Canvas_MouseLeftButtonDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dibujaLinea();
        }

        public void dibujaLinea()
        {
            //if (validaCampos()) {
            int x0, x1, y0, y1;
            x0 = (int)Double.Parse(p0x.Text);
            x1 = (int)Double.Parse(p1x.Text);
            y0 = (int)Double.Parse(p0y.Text);
            y1 = (int)Double.Parse(p1y.Text);
            Graficos g = new Graficos();
            LinkedList<System.Windows.Media.Media3D.Point3D> puntos = new LinkedList<System.Windows.Media.Media3D.Point3D>();

            puntos = g.dda(new System.Windows.Media.Media3D.Point3D(x0, y0, 0), new System.Windows.Media.Media3D.Point3D(x1, y1, 0));
            for (int i = 0; i < puntos.Count; i++)
            {
                int x, y;
                double dx = canvasDraw.ActualWidth / 30;
                double dy = canvasDraw.ActualHeight / 30;
                x = (int)(puntos.ElementAt(i).X * dx);
                y = (int)(puntos.ElementAt(i).Y * dy);
                drawRectangle(x, y, dx, dy, Colors.SeaGreen);
            }
            // }*/
            // drawPoint(35, 50);
        }

        private bool validaCampos() {
            if (p0x.Text.Length > 0)
            {
                try
                {
                    Double d = Double.Parse(p0x.Text);

                }
                catch (FormatException E)
                {
                    return false;
                }
            }
            else
                return false;
            if (p0y.Text.Length > 0)
            {
                try
                {
                    Double d = Double.Parse(p0y.Text);
                }
                catch (FormatException E)
                {
                    return false;
                }
            }else 
            return false;
            if (p1x.Text.Length > 0)
            {
                try
                {
                    Double d = Double.Parse(p1x.Text);
                 }
                catch (FormatException E)
                {
                    return false;
                }
            }
            else
                return false;
            if (p1y.Text.Length > 0)
            {
                try
                {
                    Double d = Double.Parse(p1y.Text);
                }
                catch (FormatException E)
                {
                    return false;
                }
            }
            else
                return false;
            return false;
        }

        private void drawPoint(int x,int y) {
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 10;
            myEllipse.Stroke = Brushes.Black;
           
            myEllipse.Width = 5;
            myEllipse.Height = 5;

            Canvas.SetTop(myEllipse, y);
            Canvas.SetLeft(myEllipse, x);
            canvasDraw.Children.Add(myEllipse);
           
        }

        private void canvasDraw_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (p0x.Text != "" && p0y.Text != "" && p1x.Text != "" && p1y.Text != "")
            {
                canvasDraw.Clear();
                double x1 = Double.Parse(p0x.Text);
                double y1 = Double.Parse(p0y.Text);
                double x2 = Double.Parse(p1x.Text);
                double y2 = Double.Parse(p1y.Text);
                double dx = canvasDraw.ActualWidth/30;
                double dy = canvasDraw.ActualHeight/30;
                drawRectangle(x1 * dx, y1 * dy, dx, dy,Colors.Red);
                drawRectangle(x2 * dx, y2 * dy, dx, dy, Colors.Blue);
            } 
           
            
                
          
            width.Text = canvasDraw.ActualWidth + "";
            Height.Text = canvasDraw.ActualHeight + "";
        }
      

        

    }
}

