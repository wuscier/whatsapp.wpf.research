using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace whatsapp.wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }




        private void SetWindowAsNoneStyle(IntPtr handle)
        {
            //int result = SetWindowLong(handle, -16, 369164288);

            int result2 = MoveWindow(handle, 0, 0, 320, 480, true);

        }

        private int HookProcMethod(int nCode, int wParam, IntPtr lParam)
        {

            Console.WriteLine($"code :{nCode}, wParam:{wParam}, lParam:{lParam}");

            return 0;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, int cmdShow);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);


        ContainerWindow _containerWindow;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MyHook.Start(HookProcMethod);

            _containerWindow = new ContainerWindow();
            _containerWindow.Show();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MyHook.Stop();
        }


        private void btnManualStart_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            StackPanel sp = button.Parent as StackPanel;

            StackPanel sp2 = sp.Parent as StackPanel;

            string newParentHandleTextName = $"txtNewParentHandle{sp2.Tag}";

            TextBox tb = LogicalTreeHelper.FindLogicalNode(sp, newParentHandleTextName) as TextBox;




            IntPtr handle = new IntPtr(Convert.ToInt32(tb.Text, 16));

            //IntPtr Handle = new WindowInteropHelper(this).Handle;

            var wp = _containerWindow.wp;

            WindowsFormsHost wfh = LogicalTreeHelper.FindLogicalNode(wp, $"host{sp2.Tag}") as WindowsFormsHost;



            long result = SetParent(handle, wfh.Child.Handle);

            int result2 = MoveWindow(handle, 0, 0, 320,480, true);

            //SetWindowAsNoneStyle(handle);

        }

        private void btnHideOldParent_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            StackPanel sp = button.Parent as StackPanel;
            StackPanel sp2 = sp.Parent as StackPanel;

            string txtOldParentHandleName = $"txtOldParentHandle{sp2.Tag}";

            TextBox tb = LogicalTreeHelper.FindLogicalNode(sp, txtOldParentHandleName) as TextBox;

            IntPtr handle = new IntPtr(Convert.ToInt32(tb.Text, 16));

            ShowWindow(handle, 0);
        }
    }
}
