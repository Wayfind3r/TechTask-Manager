using System.Windows;
using System.Windows.Input;
using TechTast_Management.Services;
using TechTast_Management.ViewModels;


namespace TechTast_Management.Windows
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private bool isEditWindow = false;
        private ActivityViewModel ourModel;
        public ActivityWindow()
        {

            InitializeComponent();
            TechnicianValueComboBox.ItemsSource = TechnicianService.GetTechnicianIDAliasList();
            TypeValueComboBox.ItemsSource = UnitTypeService.GetTypeIDModelList();
        }
        public ActivityWindow(ActivityViewModel thisModel):this()
        {
            ourModel = thisModel;
            isEditWindow = true;
            DateValueBlock.SelectedDate=thisModel.DateReceived;
            SerialValueTextBox.Text = thisModel.SerialNumber;
            TypeValueBlock.Visibility = Visibility.Visible;
            TypeValueBlock.Text = thisModel.Type;
            TypeValueComboBox.Visibility = Visibility.Collapsed;
            PriorityValueComboBox.SelectedIndex = thisModel.Priority-1;
            TechnicianValueComboBox.SelectedValue = ActivityService.GetTechnicianIDAlias(thisModel);

        }
        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }


        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (isEditWindow)
            {
                if (ActivityService.TryUpdateActivity(ourModel, PriorityValueComboBox.SelectedIndex = ourModel.Priority-1, 
                    TechnicianValueComboBox.SelectedValue.ToString(), SerialValueTextBox.Text, DateValueBlock.SelectedDate.ToString()))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Make sure all fields are filled correctly and that the technician is not Busy.");
                }
            }
            else
            {
                if (PriorityValueComboBox.SelectedValue == null || TypeValueComboBox.SelectedValue == null ||
                    TechnicianValueComboBox.SelectedValue == null ||
                    SerialValueTextBox.Text == null || DateValueBlock.SelectedDate == null)
                {
                    MessageBox.Show("Make sure all fields are filled correctly.");
                }
                else
                {
                    if (ActivityService.TryCreateActivity(PriorityValueComboBox.SelectedIndex+1,
                        TypeValueComboBox.SelectedValue.ToString(),
                        TechnicianValueComboBox.SelectedValue.ToString(), SerialValueTextBox.Text,
                        DateValueBlock.SelectedDate.ToString()))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Make sure all fields are filled correctly and that the technician is not Busy.");
                    }
                }
            }
        }
    }
}
