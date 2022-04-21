using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Cr7Sund.FolderIcons
{
    [InitializeOnLoad]
    internal static class FolderIconsReplacer
    {
        // References
        private static Object[] allFolderIcons;
        private static FolderIconSettings folderIcons;

        public static bool showFolder;
        public static bool showOverlay;


        static FolderIconsReplacer()
        {
            CheckPreferences();

            EditorApplication.projectWindowItemOnGUI -= ReplaceFolders;
            EditorApplication.projectWindowItemOnGUI += ReplaceFolders;
        }

        private static void ReplaceFolders(string guid, Rect selectionRect)
        {
            // Does the folder asset exist at all?
            if (allFolderIcons == null)
                allFolderIcons = GetAllInstances<FolderIconSettings>();

            if (folderIcons == null)
            {
                if (allFolderIcons.Length > 0)
                    folderIcons = allFolderIcons[0] as FolderIconSettings;
            }

            if (folderIcons == null)
                return;

            if (!folderIcons.showCustomFolder && !folderIcons.showOverlay)
                return;

            string path = AssetDatabase.GUIDToAssetPath(guid);
            Object folderAsset = AssetDatabase.LoadAssetAtPath(path, typeof(DefaultAsset));

            if (folderAsset == null) return;
            if (!AssetDatabase.IsValidFolder(path)) return;
            if (folderIcons.IconDict == null) folderIcons.GetConfigs();

            if (folderIcons.IconDict.TryGetValue(folderAsset.name, out var iconInfo)
            || folderIcons.IconDict.TryGetValue(folderAsset.name.ToLower(), out iconInfo))
            {
                DrawTextures(selectionRect, iconInfo, folderAsset, guid);
            }

        }

        private static void DrawTextures(Rect rect, FolderIconSettings.FolderIcon icon, Object folderAsset, string guid)
        {
            bool isTreeView = rect.width > rect.height;
            bool isSideView = FolderIconGUI.IsSideView(rect);

            // Vertical Folder View
            if (isTreeView)
            {
                rect.width = rect.height = FolderIconConstants.MAX_TREE_HEIGHT;

                //Add small offset for correct placement
                if (!isSideView)
                    rect.x += 3f;
            }
            else
            {
                rect.height -= 14f;
            }

            if (showFolder && icon.folderIcon)
                FolderIconGUI.DrawFolderTexture(rect, icon.folderIcon, guid);

            if (showOverlay && icon.overlayIcon)
                FolderIconGUI.DrawOverlayTexture(rect, icon.overlayIcon);
        }

        #region Initialize 

        private static void CheckPreferences()
        {
            string prefFolder = FolderIconConstants.FOLDER_TEXTURE_PATH;
            string prefIcon = FolderIconConstants.ICONS_TEXTURE_PATH;

            if (!EditorPrefs.HasKey(prefFolder))
                EditorPrefs.SetBool(prefFolder, true);

            if (!EditorPrefs.HasKey(prefIcon))
                EditorPrefs.SetBool(prefIcon, true);

            showFolder = EditorPrefs.GetBool(prefFolder);
            showOverlay = EditorPrefs.GetBool(prefIcon);
        }

        private static bool FindOrCreateFolder(string path, string folderCreateName)
        {
            if (AssetDatabase.IsValidFolder(path))
                return true;

            string parentFolder = path.Substring(0, path.LastIndexOf('/'));
            return AssetDatabase.CreateFolder(parentFolder, folderCreateName) != "";
        }

        private static T[] GetAllInstances<T>() where T : Object
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            T[] instances = new T[guids.Length];

            //probably could get optimized 
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }


            return instances;

        }

        #endregion

    }


}
