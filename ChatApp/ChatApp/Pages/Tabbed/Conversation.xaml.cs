﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Conversation : ContentView
    {
        public Conversation()
        {
            InitializeComponent();
        }

        private void GoBack(object sender, EventArgs e)
        {

        }
    }
}