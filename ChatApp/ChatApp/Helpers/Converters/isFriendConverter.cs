using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChatApp
{
    public class IsFriendConverter : IValueConverter
    {
        DataClass dataClass = DataClass.GetInstance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
         
            string uid = value as string;

            if (dataClass.loggedInUser.uid == uid)
            {
                return true;
            }
        
            if (dataClass.loggedInUser.contacts.Find(x => x == uid) != null)
            {
                return true;
            }
            
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
