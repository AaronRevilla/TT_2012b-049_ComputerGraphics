using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace GraphicNotes.Objects.Views
{
    class ObjectBar:ContentControl
    {
        private BaseObject element;
        private Button deleteButton;
        private Button editButton;
        private Button lockButton;

        public ObjectBar()
        {
            Loaded += new RoutedEventHandler(this.ObjectBar_Loaded);
            Unloaded += new RoutedEventHandler(this.ObjectBar_Unloaded);
        }

        private void ObjectBar_Loaded(object sender, RoutedEventArgs e)
        {
            this.element = DataContext as BaseObject;
            if (element != null)
            {
                ObjectBar objectBar = element.Template.FindName("PART_ObjectBar", element) as ObjectBar;
                if (objectBar != null)
                {
                    deleteButton = objectBar.Template.FindName("PART_DeleteButton",objectBar) as Button;
                    if (deleteButton != null)
                    {
                        deleteButton.Click += new RoutedEventHandler(DeleteButton_Click);
                    }
                }
            }
        }

        private void ObjectBar_Unloaded(object sender, RoutedEventArgs e)
        {
            if (deleteButton != null)
            {
                deleteButton.Click -= DeleteButton_Click;
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Relamente desea eliminar este elemento?");
        }
    }
}
