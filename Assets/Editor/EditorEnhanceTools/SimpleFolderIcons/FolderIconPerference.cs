namespace Cr7Sund.EditorEnhanceTools
{
    using UnityEngine;
    using UnityEditor;

    internal class FolderIconPerference
    {
        [PerferenceSettingMethod]
        public static bool ShowFolder => EditorPrefs.GetBool(nameof(ShowFolder), true);

        [PerferenceSettingMethod]
        public static bool ShowOverlay => EditorPrefs.GetBool(nameof(ShowOverlay), true);
    }
}