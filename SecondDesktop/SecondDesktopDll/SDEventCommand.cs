using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SecondDesktopDll
{
    public class SDEventCommand : TriggerAction<DependencyObject>
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
 
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(SDEventCommand), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(SDEventCommand), new PropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            if (CommandParameter != null)
                parameter = CommandParameter;
            var cmd = Command;
            if (cmd != null)
                cmd.Execute(parameter);
        }
    }
}
