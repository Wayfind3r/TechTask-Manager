using System.Windows.Input;

namespace TechTast_Management
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand PendingSearch = new RoutedUICommand
               (
                       "PendingSearch",
                       "PendingSearch",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {
                                        
                       }
               );
        
        public static readonly RoutedUICommand QASearch = new RoutedUICommand
               (
                       "QASearch",
                       "QASearch",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {

                       }
               );
        public static readonly RoutedUICommand DeletePendingActivity = new RoutedUICommand
               (
                       "Delete Pending Activity",
                       "Delete Pending Activity",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {

                       }
               );
        public static readonly RoutedUICommand QABackToTech = new RoutedUICommand
               (
                       "QABackToTech",
                       "QABackToTech",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {

                       }
               );
        public static readonly RoutedUICommand DeleteUnitType = new RoutedUICommand
               (
                       "DeleteUnitType",
                       "DeleteUnitType",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {

                       }
               );
        public static readonly RoutedUICommand DeleteTechnician = new RoutedUICommand
               (
                       "DeleteTechnician",
                       "DeleteTechnician",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {

                       }
               );
        public static readonly RoutedUICommand MoveToQA = new RoutedUICommand
               (
                       "MoveToQA",
                       "MoveToQA",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {

                       }
               );
        public static readonly RoutedUICommand Exit = new RoutedUICommand
               (
                       "Exit",
                       "Exit",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt)
                       }
               );
        public static readonly RoutedUICommand CreateTechnician = new RoutedUICommand
               (
                       "Create Technician",
                       "Create Technician",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt)
                       }
               );
        public static readonly RoutedUICommand CreateUnitType = new RoutedUICommand
               (
                       "CreateUnitType",
                       "CreateUnitType",
                       typeof(CustomCommands),
                       new InputGestureCollection()
                       {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt)
                       }
               );

    }
}