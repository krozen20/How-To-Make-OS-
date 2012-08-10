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
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Diagnostics;
using System.Reflection;
using Elysium.Theme;

namespace PhilipOS {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow {
        public LoginWindow() {
            InitializeComponent();

            //I like a nice black theme
            ThemeManager.Instance.Dark(ThemeManager.Instance.Accent);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            lblVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            if(String.IsNullOrWhiteSpace(txtUsername.Text) || String.IsNullOrWhiteSpace(txtPassword.Password)){

                //TODO: give error of sorts.

                return;
            }

            //Dynamic usernames and passwords will be covered in another tutorial. 
            if ( txtUsername.Text.Equals("headdetect") && txtPassword.Password.Equals("flipflops") ) {
                Hide();

                MainWindow window = new MainWindow();
                window.ShowDialog();

                //The Main window has been closed so we now show this login window.
                Show();
            }

        }



        #region Native Handles

        [DebuggerStepThrough]
        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            HwndSource hsrc = PresentationSource.FromVisual(this) as HwndSource; //Get the window handle source

            if ( hsrc == null )
                throw new SystemException("Current platform does not support full WPF library");

            hsrc.AddHook(WndProc); //Add a hook to the window processor

        }

        [DebuggerStepThrough]
        private IntPtr WndProc(IntPtr hwnd, int message, IntPtr param, IntPtr lParam, ref bool handled) {
            //If the window message is a system command and the param is a source move param
            if ( message == 0x0112 && ( param.ToInt32() & 0xFFFF0 ) == 0xF010 ) {
                //Form was moved, so do nothing.
                handled = true;
            }

            return IntPtr.Zero;
        }


        #endregion


    }
}
