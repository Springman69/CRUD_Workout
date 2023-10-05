using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using CRUD_Workout.Repositories;
using CRUD_Workout;


namespace CRUD_Workout
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private Workout _workout = new Workout();
        private string _imagePath;
        private readonly IWorkoutRepository _workoutRepository = new WorkoutRepository();

        public UpdateWindow(Workout workout)
        {
            InitializeComponent();
            _workout = workout;
            txtName.Text = _workout.Name;
            txtDuration.Text = _workout.Duration;
            txtDescription.Text = _workout.Description;
            if (_workout.Image != null)
            {
                imgPhoto.Source = Utility.ByteArrayToBitmapImage(_workout.Image);
            }
            else
            {
                imgPhoto.Source = null;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _workout.Duration = txtDuration.Text;
            _workout.Description = txtDescription.Text;
            if (_imagePath != null)
            {
                FileStream fileStream = File.Open(_imagePath, FileMode.Open);
                _workout.Image = Utility.ImageToByteArray(fileStream);
            }

            if (_workoutRepository.Update(_workout))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Wystąpił błąd", "Error", MessageBoxButton.OK);
            }
        }

        private void btnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                _imagePath = openFileDialog.FileName;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(_imagePath);
                image.EndInit();
                imgPhoto.Source = image;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
