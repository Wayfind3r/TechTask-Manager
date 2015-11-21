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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechTast_Management.Services;

namespace TechTast_Management
{
    /// <summary>
    /// Interaction logic for AddNewType.xaml
    /// </summary>
    public partial class AddNewType : Window
    {
        public AddNewType()
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
        private void CreateUnitTypeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            bool canExecute = (ModelNameTextBox.Text != "" && DescriptionTextBox.Text != "");
            e.CanExecute = canExecute;
        }

        private void CreateUnitTypeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (UnitTypeService.TryCreateUnitType(ModelNameTextBox.Text, DescriptionTextBox.Text))
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
