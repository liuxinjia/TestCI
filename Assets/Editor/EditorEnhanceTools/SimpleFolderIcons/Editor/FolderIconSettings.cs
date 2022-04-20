using System.IO;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cr7Sund.FolderIcons
{
    [CreateAssetMenu(fileName = "Folder Icon Manager", menuName = "Scriptables/Others/Folder Manager")]
    public class FolderIconSettings : ScriptableObject
    {
        [Serializable]
        public class FolderIcon
        {
            public DefaultAsset folder;

            public Texture2D folderIcon;
            public Texture2D overlayIcon;
        }

        //Global Settings
        public bool showOverlay = true;
        public bool showCustomFolder = true;

        public FolderIcon[] icons;


        public Dictionary<string, FolderIcon> IconDict;

        public void GetConfigs()
        {
            var overLayGuids = AssetDatabase.FindAssets("t:texture", new[] { FolderIconConstants.OverLayPath });

            var defaultIconDict = new Dictionary<string, FolderIcon>();
            if (!Directory.Exists(FolderIconConstants.TmpDirectoryPath)) Directory.CreateDirectory(FolderIconConstants.TmpDirectoryPath);

            foreach (var guid in overLayGuids)
            {
                bool isValidAsset = Cr7Sund.EditorUtils.NiceIO.TryGetAsseetNameViaGUID(guid, out var folderName);
                if (!isValidAsset) continue;
                string childFolderPath = $"{FolderIconConstants.TmpDirectoryPath}/{folderName}";
                if (!defaultIconDict.TryGetValue(folderName, out var iconSetting))
                {
                    iconSetting = new FolderIcon();
                    defaultIconDict.Add(folderName, iconSetting);
                }

                string directoryGUID = string.Empty;
                if (!AssetDatabase.IsValidFolder(childFolderPath))
                {
                    directoryGUID = AssetDatabase.CreateFolder(FolderIconConstants.TmpDirectoryPath, folderName);
                }
                iconSetting.folder = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(childFolderPath) as DefaultAsset;
                iconSetting.overlayIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid));
                iconSetting.folderIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(FolderIconConstants.FolderTexturePath);
            }

            if (IconDict == null) IconDict = new Dictionary<string, FolderIcon>(icons.Length);
            else IconDict.Clear();
            foreach (var item in icons)
            {
                if (item.folder != null)
                    IconDict.Add(item.folder.name, item);
            }

            foreach (var item in defaultIconDict)
            {
                if (!IconDict.ContainsKey(item.Key))
                {
                    IconDict.Add(item.Key, item.Value);
                }
            }

            int i = 0;
            icons = new FolderIcon[IconDict.Count];
            foreach (var item in IconDict)
            {
                icons[i++] = item.Value;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


    }
}
