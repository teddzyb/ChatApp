using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp
{
    public class MessageModel : INotifyPropertyChanged
    {
        string _id { get; set; }
        string _message { get; set; }
        string _converseeID { get; set; }
        DateTimeOffset _created_at { get; set; }

        public string id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(id)); } }
        public string message { get { return _message; } set { _message = value; OnPropertyChanged(nameof(message)); } }
        public string converseeID { get { return _converseeID; } set { _converseeID = value; OnPropertyChanged(nameof(converseeID)); } }
        public DateTimeOffset created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
