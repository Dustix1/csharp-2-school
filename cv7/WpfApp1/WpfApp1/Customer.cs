using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Customer : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string firstName;
        public string FirstName { get { return firstName; }
            set { 
                SetValue(ref firstName, value);
                //firstName = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(firstName));
            }}

        private string lastName;
        public string LastName { get { return lastName; }
            set {
                SetValue(ref lastName, value);
            }}
        public int Age { get; set; }
        
        private void SetValue(ref string prop, string value, [CallerMemberName] string name = null) // pičo černá magia
        {
            prop = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
