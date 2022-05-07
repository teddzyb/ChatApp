using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChatApp
{
    public class IsViewerConverter : IValueConverter
    {
        DataClass dataClass = DataClass.GetInstance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool retVal = false;
            
            if (value != null)
            {
                ConversationModel conversation = value as ConversationModel;

                if (conversation.converseeID.Equals(dataClass.loggedInUser.uid))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
