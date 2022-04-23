namespace Cr7Sund.EditorUtils
{
    using UnityEngine;
    using UnityEditor;
    using System.Reflection;
    using System;
    using Cr7Sund.CreateWindow;

    public static class ProcessEvent
    {
        /// <summary>
        /// Don not recommend reflection + attribute
        /// </summary>
        [InitializeOnLoadMethod]
        private static void InitOnUnityLoad()
        {
            var field = typeof(GUIUtility).GetField("processEvent",
                 BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            Debug.Assert(field != null, "Cannot find processEvent delegate in GUIUtility. Are you using unsupported version of Unity?");

            var oldProcessEvent = (Func<int, IntPtr, bool>)field.GetValue(null);
            Func<int, IntPtr, bool> newProcessEvent = (a, b) =>
            {
                // return false;
                // return true;
                //ATTENTION: Don't return anything here;

                if (oldProcessEvent == null)
                    return false;
                bool result = oldProcessEvent(a, b);

                if (CreateWindowPerference.EnableCreatePopupWindow) CreateNewContextMenu.RegisterProcessEvent();

                return result;
            };

            field.SetValue(null, newProcessEvent);
        }
    }
}