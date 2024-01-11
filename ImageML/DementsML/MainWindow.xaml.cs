using Microsoft.Win32;
using MLModel_ConsoleApp1;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace DementsML
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

        private void ChoiseImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files| *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageBrush im = new ImageBrush();
                Photo.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute));

                PredictBtn.Visibility = Visibility.Visible;
                ResultTxtBlock.Text = string.Empty;

            }
            Score1.Background = new SolidColorBrush(Colors.White);
            Score2.Background = new SolidColorBrush(Colors.White);
            Score3.Background = new SolidColorBrush(Colors.White);
            Score4.Background = new SolidColorBrush(Colors.White);
            Score1.Text = "";
            Score2.Text = "";
            Score3.Text = "";
            Score4.Text = "";

        }
        private void Predict_Click(object sender, RoutedEventArgs e)
        {
            PredictBtn.Visibility = Visibility.Collapsed;
            var a = 7;
            //Load sample data
            var patern = "file:///";
            var startIndex = Photo.Source.ToString().IndexOf(patern) + patern.Length;
            var path = Photo.Source.ToString().Substring(startIndex);


            var imageBytes = File.ReadAllBytes(path);
            MLModel.ModelInput sampleData = new MLModel.ModelInput()
            {
                ImageSource = imageBytes,
            };

            // Make a single prediction on the sample data and print results.
            var predictionResult = MLModel.Predict(sampleData);

            Score1.Text = predictionResult.Score[0].ToString();
            Score2.Text = predictionResult.Score[1].ToString();
            Score3.Text = predictionResult.Score[2].ToString();
            Score4.Text = predictionResult.Score[3].ToString();
            var s1 = predictionResult.Score[0];
            var s2 = predictionResult.Score[1];
            var s3 = predictionResult.Score[2];
            var s4 = predictionResult.Score[3];
            var max = Math.Max(Math.Max(Math.Max(s1, s2), s3),s4);
            var res = "Не розпізнано";
            if (max > 0)
            {
                res = predictionResult.PredictedLabel;
            }
            if (max == s1) Score1.Background = new SolidColorBrush(Colors.Green);
            if (max == s2) Score2.Background = new SolidColorBrush(Colors.Green);
            if (max == s3) Score3.Background = new SolidColorBrush(Colors.Green);
            if (max == s4) Score4.Background = new SolidColorBrush(Colors.Green);




            ResultTxtBlock.Text = res;

        }


    }
}