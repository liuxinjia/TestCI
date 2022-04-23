using Cr7Sund.FolderIcons;
using UnityEditor;

namespace Cr7Sund.EditorUtils
{
    [InitializeOnLoad]
    public static class InitializeLoadClass
    {
        static InitializeLoadClass()
        {
            EditorApplication.projectWindowItemOnGUI -= FolderIconsReplacer.ReplaceFolders;
            EditorApplication.projectWindowItemOnGUI += FolderIconsReplacer.ReplaceFolders;
        }
    }
}