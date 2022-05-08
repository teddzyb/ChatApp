using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp
{
    public class ConversationModel : INotifyPropertyChanged
    {
        string _id { get; set; }
        string _message { get; set; }
        string _converseeID { get; set; }
        DateTimeOffset _createdAt { get; set; }

        public string id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(id)); } }
        public string message { get { return _message; } set { _message = value; OnPropertyChanged(nameof(message)); } }
        public string converseeID { get { return _converseeID; } set { _converseeID = value; OnPropertyChanged(nameof(converseeID)); } }
        public DateTimeOffset createdAt { get { return _createdAt; } set { _createdAt = value; OnPropertyChanged(nameof(createdAt)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
