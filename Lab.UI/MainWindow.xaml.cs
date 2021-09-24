using Lab1.Security;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab.UI
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
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".audit"; // Required file extension 
            fileDialog.Filter = "Audit files (.audit)|*.audit"; // Optional file extensions

            bool? res = fileDialog.ShowDialog();

            if (res.HasValue && res.Value)
            {
                FileNameTxtBox.Text = fileDialog.FileName;

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (FileNameTxtBox.Text == "You have not uploaded any file yet!")
            {
                var alert = MessageBox.Show("You need to upload an audit file firstly!");
            }
            else
            {
                var openFileDlg = new VistaFolderBrowserDialog();

                bool? res = openFileDlg.ShowDialog();

                if (res.HasValue && res.Value)
                {
                    try
                    {
                        AuditFile audit = new AuditFile(FileNameTxtBox.Text);

                        var result = audit.ResultJSON;


                        // Create a new file     
                        using (StreamWriter sw = File.CreateText(openFileDlg.SelectedPath + "\\parsedFile.audit"))
                        {
                            // Add some text to file    
                            sw.Write(result);
                        }
                        var alert = MessageBox.Show("Success!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex + "you have this problem");
                    }


                }
            }
        }
    }
}
