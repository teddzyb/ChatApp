using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp
{
    class FirebaseAuthResponseModel : INotifyPropertyChanged
    {
        bool _Status { get; set; }
        string _Response { get; set; }

        public bool Status { get { return _Status; } set { _Status = value; OnPropertyChanged(nameof(Status)); } }
        public string Response { get { return _Response; } set { _Response = value; OnPropertyChanged(nameof(Response)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
