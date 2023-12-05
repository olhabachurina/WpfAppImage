using Microsoft.Win32;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageSource currentImage;
        private ImageSource nextImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (currentImage == null)
                {
                    currentImage = new BitmapImage(new Uri(openFileDialog.FileName));
                    currentImageView.Source = currentImage;
                }
                else
                {
                    nextImage = new BitmapImage(new Uri(openFileDialog.FileName));
                    nextImageView.Source = nextImage;
                }
            }
        }

        private void AnimateTransitionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null && nextImage != null)
            {
                DoubleAnimation fadeOutAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
                DoubleAnimation fadeInAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(2));

                fadeOutAnimation.Completed += (s, _) =>
                {
                    currentImageView.Source = nextImageView.Source;
                    currentImage = nextImage;
                    nextImageView.Source = null;

                    currentImageView.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                };

                currentImageView.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
            else
            {
                MessageBox.Show("Please select two images to perform the transition.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
