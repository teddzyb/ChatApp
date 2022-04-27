using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using AppEntryTest.Android;
using ChatApp;
using ChatApp.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace ChatApp.Droid
{
    [Obsolete]
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);
                var searchView = Control;

                //int searchViewCloseButtonId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                //var searchIcon = searchView.FindViewById(searchViewCloseButtonId);
                //(searchIcon as ImageView).SetImageResource(Resource.Drawable.search);

                //var searchView = base.Control as SearchView;
                //int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                //ImageView searchViewIcon = (ImageView)searchView.FindViewById<ImageView>(searchIconId);
                //searchViewIcon.SetImageDrawable(null);
            }
        }
    }
}