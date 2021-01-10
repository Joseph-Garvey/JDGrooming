using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Window Management
        /// <summary>
        /// Listens for key presses on window.
        /// F11 = Toggle Fullscreen
        /// </summary>
        /// <param name="sender">Window</param>
        /// <param name="e">Key Pressed</param>
        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F11) { ChangeWindowState((Window)sender); }
            }
            catch (InvalidCastException) { }
            catch (Exception) { }
        }
        /// <summary>
        /// Toggles
        /// </summary>
        /// <param name="window"></param>
        public void ChangeWindowState(Window window)
        {
            try
            {
                if (RetrieveWindowState(window))
                {
                    window.ResizeMode = ResizeMode.CanResize;
                    window.WindowState = WindowState.Normal;
                }
                else
                {
                    window.ResizeMode = ResizeMode.NoResize;
                    window.WindowState = WindowState.Maximized;
                }
            }
            catch (NullReferenceException) { }
            catch (InvalidCastException) { }
            catch (Exception) { }
        }
        /// <summary>
        /// Minimises the currently active window.
        /// </summary>
        public void MinimiseWindow()
        {
            try
            {
                RetrieveMainWindow().WindowState = WindowState.Minimized;
            }
            catch (NullReferenceException) { }
            catch (InvalidCastException) { }
            catch (Exception) { }
        }
        /// <summary>
        /// Returns whether the window is fullscreen.
        /// </summary>
        /// <param name="window"></param>
        /// <returns>True if window fullscreen.</returns>
        public bool RetrieveWindowState(Window window)
        {
            if (window.WindowState == WindowState.Maximized) return true;
            else return false;
        }
        /// <summary>
        /// Returns app mainwindow.
        /// </summary>
        private Window RetrieveMainWindow()
        {
            return Application.Current.MainWindow;
        }
        #endregion
    }
}
