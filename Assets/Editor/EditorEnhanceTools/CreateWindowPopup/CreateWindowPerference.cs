using UnityEditor;

namespace Cr7Sund.EditorEnhanceTools
{
    internal class CreateWindowPerference
    {

        [PerferenceSettingMethod(nameof(RefreshCreateWindoContent))]
        public static bool ShowNormalOrder => EditorPrefs.GetBool(nameof(ShowNormalOrder), false);

        [PerferenceSettingMethod]
        public static bool EnableCreatePopupWindow => EditorPrefs.GetBool(nameof(EnableCreatePopupWindow), true);

        public static void RefreshCreateWindoContent()
        {
            CreateItemsProvider.RefreshEditor();
            CreateNewContextMenu.CreateCreateWindowContent(true);
        }

    }
}