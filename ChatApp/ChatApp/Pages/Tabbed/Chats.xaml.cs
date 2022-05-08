﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChatApp.TempData;
using Plugin.CloudFirestore;
using Newtonsoft.Json;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chats : ContentView
    {
        ObservableCollection<ContactModel> contactList = new ObservableCollection<ContactModel>();
        DataClass dataClass = DataClass.GetInstance;
        public Chats()
        {
            InitializeComponent();
            FetchContacts();

            //MessagingCenter.Subscribe<MainPage>(this, "RefreshMainPage", (sender) =>
            //{
            //    FetchContacts();
            //});


            ContactListView.RefreshCommand = new Command(() =>
            {
                FetchContacts();
            });

            ContactListView.IsRefreshing = false;
        }

        private async void Frame_GoToConvo(object sender, EventArgs e)
        {
            var receiverID = (string)((TappedEventArgs)e).Parameter;


            var firestoreContact = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("users")
                                     .WhereEqualsTo("uid", receiverID)
                                     .GetAsync();

            var contact = firestoreContact.ToObjects<UserModel>().ToArray();

            var user = new UserModel
            {
                username = contact[0].username,
            };

            var conversation = new Conversation(receiverID)
            {
                BindingContext = user
            };
            await Navigation.PushAsync(conversation, true);
        }


        private void FetchContacts()
        {
            string id = dataClass.loggedInUser.uid;

            CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .WhereArrayContains("contactID", id)
                .AddSnapshotListener((snapshot, error) =>
                {
                    ContactListView.IsRefreshing = true;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            //var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            //var obj = JsonConvert.DeserializeObject<ContactModel>(json);

                            var obj = documentChange.Document.ToObject<ContactModel>();

                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    contactList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        contactList.Remove(item);
                                    }
                                    break;
                            }
                        }
                    }
                    ContactListView.IsVisible = !(contactList.Count == 0);
                    ContactListView.IsRefreshing = false;
                });

            ContactListView.ItemsSource = contactList;
        }
    }
}