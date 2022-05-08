using ChatApp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ChatApp
{
    public class DataClass : INotifyPropertyChanged
    {
        static DataClass dataClass;
        public static DataClass GetInstance
        {
            get
            {
                if (dataClass == null)
                {
                    dataClass = new DataClass();
                }
                return dataClass;
            }
        }

        bool _isSignedIn { get; set; }
        public bool isSignedIn
        {
            set
            {
                _isSignedIn = value;
                Application.Current.Properties["isSignedIn"] = _isSignedIn;
                Application.Current.SavePropertiesAsync();
                OnPropertyChanged();
            }
            get
            {
                if (Application.Current.Properties.ContainsKey("isSignedIn"))
                {
                    _isSignedIn = Convert.ToBoolean(Application.Current.Properties["isSignedIn"]);
                }
                return _isSignedIn;
            }
        }

        UserModel _loggedInUser { get; set; }
        public UserModel loggedInUser
        {
            set
            {
                _loggedInUser = value;
                Application.Current.Properties["loggedInUser"] = JsonConvert.SerializeObject(_loggedInUser);
                Application.Current.SavePropertiesAsync();
                OnPropertyChanged();
            }
            get
            {
                if (_loggedInUser == null && Application.Current.Properties.ContainsKey("loggedInUser"))
                {
                    _loggedInUser = JsonConvert.DeserializeObject<UserModel>(Application.Current.Properties["loggedInUser"].ToString());
                }

                return _loggedInUser;
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}