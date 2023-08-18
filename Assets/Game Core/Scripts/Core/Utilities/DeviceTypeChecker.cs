using GameCore.Enums;
using UnityEngine;

namespace GameCore.Utilities
{
    public static class DeviceTypeChecker
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static DeviceName GetDeviceType()
        {
#if UNITY_IOS
            bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
            if (deviceIsIpad)
            {
                return DeviceName.Tablet;
            }
            bool deviceIsIphone = UnityEngine.iOS.Device.generation.ToString().Contains("iPhone");
            if (deviceIsIphone)
            {
                return DeviceName.Phone;
            }
#elif UNITY_ANDROID

            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            
            float aspectRatio = Mathf.Max(screenWidth, screenHeight) / Mathf.Min(screenWidth, screenHeight);
            bool isTablet = DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f;

            if (isTablet)
                return DeviceName.Tablet;
            
            return DeviceName.Phone;
#endif
            // ReSharper disable once HeuristicUnreachableCode
            return DeviceName.Phone;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            return diagonalInches;
        }
    }
}