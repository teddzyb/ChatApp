using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp
{
    public class ContactModel : INotifyPropertyChanged
    {
        string _id { get; set; }
        string[] _contactID { get; set; }
        string[] _contactName { get; set; }
        string[] _contactEmail { get; set; }
        DateTime _created_at { get; set; }
        

        public string id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(id)); } }
        public string[] contactID { get { return _contactID; } set { _contactID = value; OnPropertyChanged(nameof(contactID)); } }
        public string[] contactName { get { return _contactName; } set { _contactName = value; OnPropertyChanged(nameof(contactName)); } }
        public string[] contactEmail { get { return _contactEmail; } set { _contactEmail = value; OnPropertyChanged(nameof(contactEmail)); } }
        public DateTime created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
