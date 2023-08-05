using System;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;

namespace GameCore.Utilities
{
    public static class Extensions
    {
        public static Tweener VisibilityState(this CanvasGroup canvasGroup, bool show, float fadeTime = 0,
            bool ignoreScaleTime = false)
        {
            var canvasGroupTN = canvasGroup.DOFade(show ? 1 : 0, fadeTime).SetUpdate(ignoreScaleTime);
            canvasGroup.interactable = show;
            canvasGroup.blocksRaycasts = show;

            return canvasGroupTN;
        }

        public static void VisibilityState(this GameObject gameObject, bool show) =>
            gameObject.SetActive(show);

        /// <summary>
        /// Puts the string into the Clipboard.
        /// </summary>
        public static void CopyToClipboard(this string str) =>
            GUIUtility.systemCopyBuffer = str;

        public static string GetNiceName(this string text) =>
            Regex.Replace(text, "([a-z0-9])([A-Z0-9])", "$1 $2");

        public static bool IsItemKeyOrIDValid(this string itemKey) =>
            !string.IsNullOrEmpty(itemKey);

        public static void ConvertToMinutes(this float time, out string result)
        {
            time = Mathf.Max(time, 0);

            int min = Mathf.FloorToInt(time % 3600 / 60f);
            int sec = Mathf.FloorToInt(time % 60);

            result = $"{min:D2}:{sec:D2}";
        }

        public static void ConvertToHours(this float time, out string result)
        {
            time = Mathf.Max(time, 0);

            int hours = Mathf.FloorToInt(time / 3600f);
            int min = Mathf.FloorToInt(time % 3600 / 60f);
            int sec = Mathf.FloorToInt(time % 60);

            result = $"{hours:D2}:{min:D2}:{sec:D2}";
        }
        
        public static void ConvertToDateTimeNow(this int time, out string result)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);
            DateTime dateTime = dateTimeOffset.LocalDateTime;
            result = dateTime.ToString();
        }
        
        public static void ConvertToDateTimeUtc(this int time, out string result)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);
            DateTime dateTime = dateTimeOffset.UtcDateTime;
            result = dateTime.ToString();
        }
    }
}