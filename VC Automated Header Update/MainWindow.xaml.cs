using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
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
using System.Text.RegularExpressions;


namespace VC_Automated_Header_Update
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string pattern = @"(\s\d*[.]\d*)\s";
        private List<string> jovialFileNames = new List<string>();
        public ObservableCollection<HeaderInfo> headerInfo = new ObservableCollection<HeaderInfo>();
        public float VersionNum { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            listFileView.ItemsSource = headerInfo;
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            headerInfo.Clear();

            try
            {

                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = @"C:\Users\14789\Documents\Test Folder";
                //Add in the other File types
                openFileDialog.Filter = "jovial files (*.j73)|*.j73|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {

                    foreach (string file in openFileDialog.FileNames)
                    {
                        try
                        {
                            jovialFileNames.Add(file);
                        }
                        catch (SecurityException ex)
                        {
                            // The user lacks appropriate permissions to read files, discover paths, etc.
                            MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                "Error message: " + ex.Message + "\n\n" +
                                "Details (send to Support):\n\n" + ex.StackTrace
                            );
                        }
                        catch (Exception ex)
                        {
                            // Could not load the image - probably related to Windows file system permissions.
                            MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                                + ". You may not have permission to read the file, or " +
                                "it may be corrupt.\n\nReported error: " + ex.Message);
                        }
                    }

                    tbNumofFiles.Text = "# of Selected files = " + jovialFileNames.Count.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Error w/ OpenFileDialog");
            }

        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.ShowDialog();
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            string fileContent = string.Empty;
            int cnt = 0;

            Regex regex = new Regex(pattern);
            CheckVersionNumber cvnClass = new CheckVersionNumber();
            MatchEvaluator matchCheck = new MatchEvaluator(cvnClass.UpdateVerionNumber);
            dateTime.Text = DateTime.Now.ToString();

            if (jovialFileNames != null)
            {
                foreach (string filePath in jovialFileNames)
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        fileContent = reader.ReadToEnd();
                        fileContent = regex.Replace(fileContent, matchCheck, 1);
                    }

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(fileContent);
                        writer.Write('\n' + txtBoxDoc.Text);
                        writer.Write(@"The Version num is: " + VersionNum.ToString());
                        cnt++;
                    }
                    headerInfo.Add(new HeaderInfo(System.IO.Path.GetFileName(filePath), (VersionNum - 1).ToString("F2"), VersionNum.ToString("F2")));
                }
            }

            tbNumofCompltedFiles.Text = "# of Files Updated = " + cnt.ToString();

        }
    }
}
