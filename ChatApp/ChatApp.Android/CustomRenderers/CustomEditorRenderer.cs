using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using ChatApp;
using ChatApp.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace ChatApp.Droid
{
    [Obsolete]
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = (Android.Widget.EditText)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = Xamarin.Forms.Color.FromRgb(255, 255, 255).ToAndroid();
                shape.Paint.SetStyle(Paint.Style.Stroke);
                nativeEditText.Background = shape;

            }
        }
    }
}