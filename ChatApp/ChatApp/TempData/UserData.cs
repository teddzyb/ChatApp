using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatApp.TempData
{
    class UserData
    {
        public ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();
        public UserData ()
        {
            userList.Add(new UserModel() { id = "1", username = "User Test-1", email = "utest1@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "2", username = "User Test-2", email = "utest2gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "3", username = "User Test-3", email = "utest3@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "4", username = "User Test-4", email = "utest4@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "5", username = "User Test-5", email = "utest5@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "6", username = "User Test-6", email = "utest6@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "7", username = "User Test-7", email = "utest7@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "8", username = "User Test-8", email = "utest8@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "9", username = "User Test-9", email = "utest9@gmail.com", password = "1234" });
            userList.Add(new UserModel() { id = "10", username = "User Test-10", email = "utest10@gmail.com", password = "1234" });
        }
    }
}
