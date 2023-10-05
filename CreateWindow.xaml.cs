using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using CRUD_Workout.Repositories;

namespace CRUD_Workout
{
    public partial class CreateWindow : Window
    {
        private string? _imagePath;
        private readonly IWorkoutRepository _workoutRepository = new WorkoutRepository();

        public CreateWindow()
        {
            InitializeComponent();
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Workout workout = new Workout();
            workout.Name = txtName.Text;
            workout.Duration = txtDuration.Text;
            workout.Description = txtDescription.Text;

            if (_imagePath != null)
            {
                using (FileStream fileStream = File.Open(_imagePath, FileMode.Open))
                {
                    workout.Image = Utility.ImageToByteArray(fileStream);
                }
            }

            if (_workoutRepository.Create(workout))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Wystąpił błąd", "Error", MessageBoxButton.OK);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
