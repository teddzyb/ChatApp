using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp
{
    class UserModel : INotifyPropertyChanged
    {
        int _id { get; set; }
        string _username { get; set; }
        string _email { get; set; }
        string _password { get; set; }
        //bool isVerified { get; set; }

        public int id { get { return _id; }                 set { _id = value; OnPropertyChanged(nameof(id)); } }
        public string username { get { return _username; }  set { _username = value; OnPropertyChanged(nameof(username)); } }
        public string email { get { return _email; }        set { _email = value; OnPropertyChanged(nameof(email)); } }
        public string password { get { return _password; }  set { _password = value; OnPropertyChanged(nameof(password)); } }

        //public bool isVerified { get { return _isDone; } set { _isDone = value; OnPropertyChanged(nameof(isDone)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
