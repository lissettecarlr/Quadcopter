using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuaternionView
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class View3D : UserControl
    {

        private string model3DPath;

        public string Model3DPath
        {
            get { return model3DPath; }
            set { model3DPath = value; loadModel(); }
        }

        public View3D()
        {
            InitializeComponent();
            view1.RotateGesture.MouseAction = MouseAction.LeftClick;
            view1.PanGesture.MouseAction = MouseAction.RightClick;
            view1.PanGesture.Modifiers = ModifierKeys.None;

        }

        private void loadModel()
        {
            var mi = new ModelImporter();
            var fileModel = mi.Load(Model3DPath, null, true);
            Model3DGroup modelGroup = fileModel as Model3DGroup;
            modelVisual3D.Content = modelGroup;
        }

        public void SetQuaternion(double x,double y,double z,double w)
        {
            Quaternion q = new Quaternion(x, y, z, w);
            qRotation.Quaternion = q;
        }

        private void view1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width > 30 && e.NewSize.Height > 30)
            {
                 view1.ZoomExtents();
            }
        }

    }
}
