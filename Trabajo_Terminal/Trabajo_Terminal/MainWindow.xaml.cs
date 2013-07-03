using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphicNotes.Objects.Views;
using GraphicNotes.Workspace;
using System.Threading;
using DevExpress.Xpf.Docking;

namespace GraphicNotes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetLanguageDictionary();
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "es-MX":
                    dict.Source = new Uri("..\\Languages\\StringResources.xaml", UriKind.Relative);
                    break;
                case "en-GB":
                    dict.Source = new Uri("..\\Languages\\StringResources.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Languages\\StringResources.xaml", UriKind.Relative);
                    break;
            }
            //this.Resources.MergedDictionaries.Add(dict);
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        //private void button1_Click_1(object sender, RoutedEventArgs e)
        //{
        //    BaseObject ob = new BaseObject();
        //    Image content = new Image();
        //    BitmapImage bi = new BitmapImage(new Uri("./Icons/delete_object.png", UriKind.Relative));
        //    content.Source = bi;
        //    content.Stretch = Stretch.None;

        //    ob.Content = content;

        //    CanvasWorkspace.SetLeft(ob, 10);
        //    CanvasWorkspace.SetTop(ob, 10);
        //    canvasWorkspace1.Children.Add(ob);
        //    ob.IsSelected = true;
        //    Console.WriteLine("DataContext:" + ob.DataContext);
        //}

        private void btnEnglish_Click(object sender, RoutedEventArgs e)
        {
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            SetLanguageDictionary();
        }

        private void btnFrench_Click(object sender, RoutedEventArgs e)
        {
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("es-MX");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            SetLanguageDictionary();
        }

        private void bNew_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            DocumentContainer dc = new DocumentContainer();
            documentGroup1.Items.Add(dc.DocumentPanel);
            this.dockManager1.DockController.Activate(dc.DocumentPanel);
        }

        private void groupFile_CaptionButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("This is File Open dialog", "File Open Dialog");
        }
    }
}
