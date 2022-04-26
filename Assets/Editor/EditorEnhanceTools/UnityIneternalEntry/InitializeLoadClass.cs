using Cr7Sund.EditorEnhanceTools;
using UnityEditor;

namespace Cr7Sund.EditorEnhanceTools
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