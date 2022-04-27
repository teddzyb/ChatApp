using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatApp.TempData
{
    class ContactData
    {
        public ObservableCollection<ContactModel> contactList =  new ObservableCollection<ContactModel>();
        public ContactData()
        {
            contactList.Add(new ContactModel() { 
                id="1", contactID = new string[] {"1", "2", "3", "4" }, 
                contactName = new string[] { "User Test-1", "User Test-2", "User Test-3", "User Test-4" },
                contactEmail = new string[] { "utest1@gmail.com", "utest2@gmail.com", "utest3@gmail.com", "utest4@gmail.com" },
                created_at = new DateTime()
            });
        }
    }
}
