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
                id="1", contactID = new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10" },
                contactName = new string[] { "User Test-1", "User Test-2", "User Test-3", "User Test-4", "User Test-5", "User Test-6", "User Test-7", "User Test-8", "User Test-9", "User Test-10" },
                contactEmail = new string[] { "utest1@gmail.com", "utest2@gmail.com", "utest3@gmail.com", "utest4@gmail.com", "utest5@gmail.com", "utest6@gmail.com", "utest7@gmail.com", "utest8@gmail.com", "utest9@gmail.com", "utest10@gmail.com" },
                created_at = new DateTime()
            });
        }
    }
}
