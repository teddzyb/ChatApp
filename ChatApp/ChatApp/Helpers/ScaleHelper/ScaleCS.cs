using ChatApp;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Sample
{
    public static class ScaleCS
    {
        /// <summary>
        /// Scales the height.
        /// </summary>
        /// <returns>The height.</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static float ScaleHeight(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenHeight / 568.0));
        }

        /// <summary>
        /// Scales the height.
        /// </summary>
        /// <returns>The height.</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static float ScaleHeight(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenHeight / 568.0));
        }

        /// <summary>
        /// Scales the height.
        /// </summary>
        /// <returns>The height.</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static float ScaleHeight(this float number, float? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenHeight / 568.0));
        }

        /// <summary>
        /// Scales the width.
        /// </summary>
        /// <returns>The width.</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static float ScaleWidth(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenWidth / 320.0));
        }

        /// <summary>
        /// Scales the width.
        /// </summary>
        /// <returns>The width.</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static float ScaleWidth(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenWidth / 320.0));
        }

        /// <summary>
        /// Scales the width.
        /// </summary>
        /// <returns>The width.</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static float ScaleWidth(this float number, float? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenWidth / 320.0));
        }

        /// <summary>
        /// Scales the font.
        /// </summary>
        /// <returns>The font.</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static double ScaleFont(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (number * (App.screenHeight / 568.0) - (Device.RuntimePlatform == Device.iOS ? 0.5 : 0));
        }

        /// <summary>
        /// Scales the font.
        /// </summary>
        /// <returns>Formatted chips</returns>
        /// <param name="number">Number.</param>
        /// <param name="iOS">Value for iOS.</param>
        public static double ScaleFont(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (number * (App.screenHeight / 568.0) - (Device.RuntimePlatform == Device.iOS ? 0.5 : 0));
        }
    }
}