using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EMGUCV_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbXpsPath.Text != string.Empty && tbXpsPathOriginal.Text != string.Empty)
            {
                // Use ProcessStartInfo class.
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
#if DEBUG
                startInfo.FileName = @"..\..\bin\Debug\XpsToImageConverterComparer.exe";
#else
                startInfo.FileName = @"..\..\bin\Release\XpsToImageConverterComparer.exe";
#endif
                string nameOfButton = ((Button)sender).Name.ToString();

                if (nameOfButton == "btnConvert")
                {
                    startInfo.Arguments = "convert " + tbXpsPath.Text.Replace(' ', '*') + " " + tbXpsPathOriginal.Text.Replace(' ', '*');

                }
                else if (nameOfButton == "btnCompare")
                {
                    startInfo.Arguments = "compare " + tbXpsPath.Text.Replace(' ', '*') + " " + tbXpsPathOriginal.Text.Replace(' ', '*');
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                Process mainProcess = Process.Start(startInfo);
                mainProcess.WaitForExit();
                sw.Stop();

                MessageBox.Show("Finished in: " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds!");
            }
        }

        private void MouseEnterEvent(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ((TextBox)sender).Text = fbd.SelectedPath;
            }
        }
    }
}
