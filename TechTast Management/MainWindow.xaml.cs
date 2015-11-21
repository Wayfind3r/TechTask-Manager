
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechTast_Management;
using TechTast_Management.Services;
using TechTast_Management.ViewModels;
using TechTast_Management.Windows;

namespace TechTask_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateDataGrids();
            PopulateTechniciansGrid();
            PopulateUnitTypeGrid();
        }

        private void PendingSearchCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PendingSearchCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)PendingSearchByComboBox.SelectedItem;
            string value = typeItem.Content.ToString();
            PendingDataGrid.ItemsSource =
                ActivityService.SearchByActivityValueAndStatus(value,
                    PendingSearchValueBox.Text, PendingDataGrid);
        }
        private void QASearchCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void QASearchCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)QASearchByComboBox.SelectedItem;
            string value = typeItem.Content.ToString();
            QaDataGrid.ItemsSource =
                ActivityService.SearchByActivityValueAndStatus(value,
                    PendingSearchValueBox.Text, QaDataGrid);
        }

        public void PopulateDataGrids()
        {
            PendingDataGrid.ItemsSource = ActivityService.GetActivityViewModelCollection(ActivityStatus.Pending);
            QaDataGrid.ItemsSource = ActivityService.GetActivityViewModelCollection(ActivityStatus.QA);
            ArchiveDataGrid.ItemsSource = ActivityService.GetActivityViewModelCollection(ActivityStatus.Archive);
        }

        public void PopulateTechniciansGrid()
        {
            TechniciansDataGrid.ItemsSource = TechnicianService.GetTechnicianViewModelCollection();
        }

        public void PopulateUnitTypeGrid()
        {
            UnitTypesDataGrid.ItemsSource = UnitTypeService.GetUnitTypeCollection();
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            PopulateDataGrids();
            PopulateTechniciansGrid();
            PopulateUnitTypeGrid();
        }

        private void DeletePendingActivityCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { e.CanExecute = true;}

        private void DeletePendingActivityCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ActivityService.DeleteActivityFromDB((ActivityViewModel)PendingDataGrid.SelectedItem);
            PendingDataGrid.ItemsSource = ActivityService.GetActivityViewModelCollection(ActivityStatus.Pending);
            PopulateTechniciansGrid();
        }
        private void QABackToTechCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { e.CanExecute = true;}

        private void QABackToTechCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ActivityService.QATryReturnToTechnician((ActivityViewModel)QaDataGrid.SelectedItem);
            PopulateDataGrids();
            PopulateTechniciansGrid();
        }

        private void DeleteUnitTypeCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DeleteUnitTypeCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            bool success = UnitTypeService.TryDeleteUnitType((UnitTypeViewModel)UnitTypesDataGrid.SelectedItem);
            if (success)
            {
                PopulateDataGrids();
                PopulateUnitTypeGrid();
            }
        }
        private void DeleteTechnicianCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { e.CanExecute = true;}

        private void DeleteTechnicianCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            bool success = TechnicianService.TryDeleteTechnician((TechnicianViewModel)TechniciansDataGrid.SelectedItem);
            if (success)
            {
                PopulateDataGrids();
                PopulateTechniciansGrid();
            }
        }
        private void MoveToQACommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { e.CanExecute = true; }

        private void MoveToQACommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            bool success = ActivityService.TryMovePendingActivityToQA((ActivityViewModel)TechniciansDataGrid.SelectedItem);
            if (success)
            {
                PopulateDataGrids();
                PopulateTechniciansGrid();
            }
        }

        private void PendingCreateActivityButton_OnClick(object sender, RoutedEventArgs e)
        {
            ActivityWindow newWindow = new ActivityWindow();
            newWindow.Owner = this;
            newWindow.ShowDialog();
            PopulateDataGrids();
            PopulateTechniciansGrid();
        }

        private void EditActivityButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PendingDataGrid.SelectedItem == null) return;
            var newWindow = new ActivityWindow((ActivityViewModel)PendingDataGrid.SelectedItem);
            newWindow.Owner = this;
            newWindow.ShowDialog();
                PopulateDataGrids();
                PopulateTechniciansGrid();
        }

        private void AddTechnicianButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddTechnician();
            newWindow.Owner = this;
            newWindow.ShowDialog();
            PopulateDataGrids();
            PopulateTechniciansGrid();
        }

        private void AddUnitTypeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddNewType();
            newWindow.Owner = this;
            newWindow.ShowDialog();
            PopulateUnitTypeGrid();
        }
    }
}
