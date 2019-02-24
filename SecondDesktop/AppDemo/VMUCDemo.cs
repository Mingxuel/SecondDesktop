using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo
{
    class VMUCDemo : NotifyPropertyChanged
    {
        public MUCDemo Model { get; set; }

        public VMUCDemo()
        {
            Model = new MUCDemo();
            Model.Title = "Hello Snoopy";
        }

        public string Title
        {
            get { return Model.Title; }
            set
            {
                this.SetAndNotify(ref Model.Title, value, () => Model.Title);
            }
        }
    }

    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SetAndNotify<T>(ref T field, T value, Expression<Func<T>> property)
        {
            if (!object.ReferenceEquals(field, value))
            {
                field = value;
                this.OnPropertyChanged(property);
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            if (PropertyChanged != null)
            {
                string name = ((MemberExpression)changedProperty.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}