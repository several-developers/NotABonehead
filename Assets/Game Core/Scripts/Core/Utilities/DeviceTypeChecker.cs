using UnityEngine;

namespace GameCore.Utilities
{
    public enum EnumDeviceType
    {
        Tablet,
        Phone
    }

    public static class DeviceTypeChecker
    {
        public static bool isTablet;

        static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            return diagonalInches;
        }

        public static EnumDeviceType GetDeviceType()
        {
#if UNITY_IOS
            bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
            if (deviceIsIpad)
            {
                return EnumDeviceType.Tablet;
            }
            bool deviceIsIphone = UnityEngine.iOS.Device.generation.ToString().Contains("iPhone");
            if (deviceIsIphone)
            {
                return EnumDeviceType.Phone;
            }
#elif UNITY_ANDROID

            float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
            bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

            if (isTablet)
                return EnumDeviceType.Tablet;
            
            return EnumDeviceType.Phone;
#endif
            return EnumDeviceType.Phone;
        }
    }
}