

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Cr7Sund.EditorUtils
{
    [InitializeOnLoad]
    public class NewCustomSettingsHandler
    {
        private const string customBool1Key = "customSettings.customBool1";
        private const string customBool2Key = "customSettings.customBool2";

        public class NewCustomSettings
        {
            public bool customBool1;
            public bool customBool2;
        }

        public static NewCustomSettings GetEditorSettings()
        {
            return new NewCustomSettings
            {
                customBool1 = EditorPrefs.GetBool(customBool1Key, true),
                customBool2 = EditorPrefs.GetBool(customBool2Key, true),
            };
        }

        public static void SetEditorSettings(NewCustomSettings settings)
        {
            EditorPrefs.SetBool(customBool1Key, settings.customBool1);
            EditorPrefs.SetBool(customBool2Key, settings.customBool2);
        }
    }

    internal class SettingsGUIContent
    {
        private static GUIContent enableCustomBool1 = new GUIContent("Enable Custom Setting 1", "Tooltip for custom setting 1 goes here");
        private static GUIContent enableCustomBool2 = new GUIContent("Enable Custom Setting 2", "Tooltip for custom setting 2 goes here");

        public static void DrawSettingsButtons(NewCustomSettingsHandler.NewCustomSettings settings)
        {
            EditorGUI.indentLevel += 1;

            settings.customBool1 = EditorGUILayout.ToggleLeft(enableCustomBool1, settings.customBool1);
            settings.customBool2 = EditorGUILayout.ToggleLeft(enableCustomBool2, settings.customBool2);

            EditorGUI.indentLevel -= 1;
        }
    }
    
    static class NewCustomSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("Preferences/New Custom Settings Menu", SettingsScope.User)
            {
                label = "New Custom Settings Menu",

                guiHandler = (searchContext) =>
                {
                    NewCustomSettingsHandler.NewCustomSettings settings = NewCustomSettingsHandler.GetEditorSettings();

                    EditorGUI.BeginChangeCheck();
                    SettingsGUIContent.DrawSettingsButtons(settings);

                    if (EditorGUI.EndChangeCheck())
                    {
                        NewCustomSettingsHandler.SetEditorSettings(settings);
                    }

                },

                // Keywords for the search bar in the Unity Preferences menu
                keywords = new HashSet<string>(new[] { "New", "Custom", "Settings" })
            };

            return provider;
        }
    }


} // namespace
