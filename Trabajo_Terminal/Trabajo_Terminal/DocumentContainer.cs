using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Docking;
using System.Windows;


namespace GraphicNotes
{
    class DocumentContainer
    {
        public DocumentPanel DocumentPanel{
         get;
         set;
        }

        public DocumentContainer()
        {
            DocumentPanel = new DocumentPanel();
            DocumentPanel.SetResourceReference(DocumentPanel.StyleProperty, "DocumentPanelStyle");
        }
    }
}
