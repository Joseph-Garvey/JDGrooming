using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JDGrooming.Classes.Database_Management;
using System.Windows.Controls;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ProjectDBAccess query = new ProjectDBAccess();
        public Database db
        {
            get { return query.db; }
            set { query.db = value; }
        }

        #region Override Events
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                string debugPath = System.IO.Path.GetDirectoryName(Environment.CurrentDirectory);
                string datadirectoryPath = System.IO.Path.GetDirectoryName(debugPath);
                AppDomain.CurrentDomain.SetData("DataDirectory", datadirectoryPath);
                //// Start Splash Screen and Show
                //Splash splashScreen = new Splash();
                //this.MainWindow = splashScreen;
                //splashScreen.Show();
                //// Show Progress Bar on a new thread
                //Task.Factory.StartNew(() =>
                //{
                //    // report progress each iteration to update progress bar.
                //    for (int i = 1; i <= 110; i++)
                //    {
                //        // simulated loading.
                //        System.Threading.Thread.Sleep(10);
                //        splashScreen.Dispatcher.Invoke(() => splashScreen.Progress = i);
                //    }
                //    // on completion do this
                //    this.Dispatcher.Invoke(() =>
                //    {
                //        //initialize the Welcome Screen window, set as main app window.
                //        //and close the splash screen
                //        UIElements.Login mainWindow = new UIElements.Login();
                //        mainWindow.Show();
                //        splashScreen.Close();
                //    });
                //});
                #region Database Setup
                if (db.Connect()) MessageBox.Show("Database Connection Successful", "Success");
                else
                {
                    MessageBox.Show("Database Connection Unsuccessful", "Error");
                    return;
                }
                if (db.Rdr is null) { }
                else
                {
                    db.Rdr.Close();
                }
                //var lines = System.IO.File.ReadAllLines(@"C:\Users\Joseph Garvey\Desktop\CW\Dog_Breeds.txt");
                //for (var i = 0; i < lines.Length; i += 1)
                //{
                //    query.QueryDatabase("INSERT INTO [Breed] (Name) VALUES ('" + lines[i] + "');");
                //}
                query.QueryDatabase("DELETE FROM [Shift]");
                foreach (JDGrooming.Classes.Staff s in query.GetStaff())
                {
                    DateTime startime = DateTime.Today.AddHours(9);
                    DateTime endtime = startime.AddHours(8);
                    if (s.Name == "Jane")
                    {
                        endtime = endtime.AddHours(-4);
                    }
                    for (int i = 1; i <= 5; i++)
                    {
                        String str = $"INSERT INTO [Shift] ([StaffID], [Day], [StartTime], [EndTime]) VALUES ({s.ID}, {i}, '{startime.TimeOfDay}', '{endtime.TimeOfDay}');";
                        query.QueryDatabase(str);
                    }

                }
                #endregion
            }
            catch (TaskCanceledException) { }
        }
        #endregion
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
        #region UI Methods
        private void cmb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            OpenComboBox((ComboBox)sender);
        }
        private void cmb_GotFocus(object sender, RoutedEventArgs e)
        {
            OpenComboBox((ComboBox)sender);
        }
        private void OpenComboBox(ComboBox sender)
        {
            sender.IsDropDownOpen = true;
        }
        #endregion
    }
}
