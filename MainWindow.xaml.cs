using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CRUD_Workout.Repositories;
using CRUD_Workout;

namespace CRUD_Workout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Workout> workoutsList = new List<Workout>();
        private readonly IWorkoutRepository _workoutRepository = new WorkoutRepository();

        public MainWindow()
        {
            InitializeComponent();
            RefreshListView();
        }

        public void RefreshListView()
        {
            workoutsList = _workoutRepository.ReadAll();
            cmbWorkoutView.ItemsSource = workoutsList.Select(n => n.Name);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (cmbWorkoutView.SelectedItem != null)
            {
                if (MessageBox.Show("Jesteś pewien?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (_workoutRepository.Delete(workoutsList[cmbWorkoutView.SelectedIndex]))
                    {
                        cmbWorkoutView.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił błąd", "Error", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Wybierz wpis do usunięcia", "Warning", MessageBoxButton.OK);
            }
        }

        private void cmbWorkoutView_DropDownOpened(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void cmbWorkoutView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbWorkoutView.SelectedIndex != -1)
            {
                txtName.Text = workoutsList[cmbWorkoutView.SelectedIndex].Name;
                txtDuration.Text = workoutsList[cmbWorkoutView.SelectedIndex].Duration;
                txtDescription.Text = workoutsList[cmbWorkoutView.SelectedIndex].Description;

                if (workoutsList[cmbWorkoutView.SelectedIndex].Image != null)
                {
                    imgPhoto.Source = Utility.ByteArrayToBitmapImage(workoutsList[cmbWorkoutView.SelectedIndex].Image);
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new CreateWindow();
            createWindow.ShowDialog();
            RefreshListView();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cmbWorkoutView.SelectedItem != null)
            {
                UpdateWindow updateWindow = new UpdateWindow(workoutsList[cmbWorkoutView.SelectedIndex]);
                updateWindow.ShowDialog();
                RefreshListView();
            }
            else
            {
                MessageBox.Show("Wybierz wpis do aktualizacji", "Warning", MessageBoxButton.OK);
            }
        }
    }
}