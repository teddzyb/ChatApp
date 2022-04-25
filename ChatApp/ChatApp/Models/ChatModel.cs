using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp
{
    class ChatModel : INotifyPropertyChanged
    {
        int _id { get; set; }
        string _senderEmail { get; set; }
        string _receiverEmail { get; set; }
        string _message { get; set; }

        public int id { get { return _id; }                            set { _id = value; OnPropertyChanged(nameof(id)); } }
        public string senderEmail { get { return _senderEmail; }       set { _senderEmail = value; OnPropertyChanged(nameof(senderEmail)); } }
        public string receiverEmail { get { return _receiverEmail; }   set { _receiverEmail = value; OnPropertyChanged(nameof(receiverEmail)); } }
        public string message { get { return _message; }               set { _message = value; OnPropertyChanged(nameof(message)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
