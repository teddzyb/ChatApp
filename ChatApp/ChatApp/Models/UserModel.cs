using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp
{
    public class UserModel : INotifyPropertyChanged
    {
        string _uid { get; set; }
        string _username { get; set; }
        string _email { get; set; }
        int _userType { get; set; }
        DateTime _created_at { get; set; }
        List<string> _contacts { get; set; }
        
        //bool _isVerified { get; set; }
        //string _password { get; set; }

        public string uid { get { return _uid; }    set { _uid = value; OnPropertyChanged(nameof(uid)); } }
        public string username { get { return _username; }  set { _username = value; OnPropertyChanged(nameof(username)); } }
        public string email { get { return _email; }    set { _email = value; OnPropertyChanged(nameof(email)); } }
        public int userType { get { return _userType; } set { _userType = value; OnPropertyChanged(nameof(userType)); } }
        public DateTime created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }
        public List<string> contacts { get { return _contacts; } set { _contacts = value; OnPropertyChanged(nameof(contacts)); } }

        //public bool isVerified { get { return _isVerified; } set { _isVerified = value; OnPropertyChanged(nameof(isVerified)); } }
        //public string password { get { return _password; }  set { _password = value; OnPropertyChanged(nameof(password)); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
