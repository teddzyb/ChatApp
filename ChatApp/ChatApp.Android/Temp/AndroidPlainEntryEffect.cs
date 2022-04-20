using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using PlainEntryAndroidSample.Droid;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ResolutionGroupName("PlainEntryGroup")]
[assembly: ExportEffect(typeof(AndroidPlainEntryEffect), "PlainEntryEffect")]

namespace PlainEntryAndroidSample.Droid
{
    public class AndroidPlainEntryEffect : PlatformEffect
    {
        public AndroidPlainEntryEffect()
        {
        }
        protected override void OnAttached()
        {
            try
            {
                if (Control != null)
                {
                    Android.Graphics.Color entryLineColor = Android.Graphics.Color.Rgb(248, 249, 250);
                    Control.BackgroundTintList = ColorStateList.ValueOf(entryLineColor);


                    var nativeEditText = (Android.Widget.EditText)Control;
                    var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                    shape.Paint.Color = Xamarin.Forms.Color.Red.ToAndroid();
                    shape.Paint.SetStyle(Paint.Style.Stroke);
                    nativeEditText.Background = shape;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error... Unable to set property on attached control", ex.Message);
            }
        }
        protected override void OnDetached()
        {
        }
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }
    }
}