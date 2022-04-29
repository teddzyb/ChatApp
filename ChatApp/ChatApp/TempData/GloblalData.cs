using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatApp.TempData
{
    class GloblalData
    {
        public static ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();
        public static ObservableCollection<ContactModel> contactList = new ObservableCollection<ContactModel>();
        public static ObservableCollection<ConversationModel> conversationList = new ObservableCollection<ConversationModel>();
    }
}
