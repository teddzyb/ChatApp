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
        DateTimeOffset _createdAt { get; set; }
        List<string> _contacts { get; set; }
        
        public string uid { get { return _uid; }    set { _uid = value; OnPropertyChanged(nameof(uid)); } }
        public string username { get { return _username; }  set { _username = value; OnPropertyChanged(nameof(username)); } }
        public string email { get { return _email; }    set { _email = value; OnPropertyChanged(nameof(email)); } }
        public int userType { get { return _userType; } set { _userType = value; OnPropertyChanged(nameof(userType)); } }
        public DateTimeOffset createdAt { get { return _createdAt; } set { _createdAt = value; OnPropertyChanged(nameof(createdAt)); } }
        public List<string> contacts { get { return _contacts; } set { _contacts = value; OnPropertyChanged(nameof(contacts)); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
