using System.Windows;
using System.Windows.Input;
using TechTast_Management.Services;


namespace TechTast_Management
{
    /// <summary>
    /// Interaction logic for AddTechnician.xaml
    /// </summary>
    public partial class AddTechnician : Window
    {
        public AddTechnician()
        {
            InitializeComponent();
        }
        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        private void CreateTechnicianCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            bool canExecute = (AliasTextBox.Text!="" && FullNameTextBox.Text!="" && TelephoneNumberTextBox.Text!="");
            e.CanExecute = canExecute;
        }

        private void CreateTechnicianCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (TechnicianService.TryCreateTechnician(AliasTextBox.Text, FullNameTextBox.Text,
                TelephoneNumberTextBox.Text))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Make sure no fields are empty.");
            }
        }
    }
}
