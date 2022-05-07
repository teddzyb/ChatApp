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
        List<MessageModel> _messages { get; set; }
        string[] _converseeID { get; set; }
        DateTime _created_at { get; set; }

        public string id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(id)); } }
        public List<MessageModel> messages { get { return _messages; } set { _messages = value; OnPropertyChanged(nameof(messages)); } }
        public string[] converseeID { get { return _converseeID; } set { _converseeID = value; OnPropertyChanged(nameof(converseeID)); } }
        public DateTime created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
