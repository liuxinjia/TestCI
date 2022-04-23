

namespace Cr7Sund.FolderIcons
{
    using Cr7Sund.EditorUtils;
    using System.IO;
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using static Cr7Sund.FolderIcons.FolderIconSettings;
    
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



        public FolderIcon[] icons;


        public Dictionary<string, FolderIcon> IconDict;
        public static Texture2D[] DefaultFolders;

        public void GetConfigs()
        {

            if (DefaultFolders == null)
            {
                var guids = AssetDatabase.FindAssets("t:texture", new[] { FolderIconConstants.DefaultIconPath });
                DefaultFolders = new Texture2D[guids.Length];

                for (int textureAssetCount = 0; textureAssetCount < guids.Length; textureAssetCount++)
                {
                    var folderTexture = NiceIO.LoadAssetViaGUID<Texture2D>(guids[textureAssetCount]);
                    DefaultFolders[textureAssetCount] = folderTexture;
                }
            }


            if (IconDict == null ) IconDict = new Dictionary<string, FolderIcon>();
            else IconDict.Clear();

            if (icons != null)
            {
                foreach (var item in icons)
                {
                    if (item.folder != null)
                    {
                        if (IconDict.ContainsKey(item.folder.name))
                        {
                            icons = null;
                            GetConfigs();
                            return;
                        }
                        else
                        {
                            IconDict.Add(item.folder.name, item);
                        }
                    }
                }
            }
            IconConfigger.Create<FolderIconConfigger>().InitIconSettings(IconDict);
            IconConfigger.Create<OverLayIconConfigger>().InitIconSettings(IconDict);

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


    public abstract class IconConfigger
    {
        public virtual string[] Paths { get; }
        public void InitIconSettings(Dictionary<string, FolderIcon> IconDict)
        {
            if (!Directory.Exists(FolderIconConstants.TmpDirectoryPath)) Directory.CreateDirectory(FolderIconConstants.TmpDirectoryPath);

            var iconGuids = AssetDatabase.FindAssets("t:texture", Paths);

            var defaultIconDict = new Dictionary<string, FolderIcon>();

            foreach (var guid in iconGuids)
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
                SetFolderIconInfo(guid, childFolderPath, iconSetting);
            }


            foreach (var item in defaultIconDict)
            {
                if (!IconDict.ContainsKey(item.Key))
                {
                    IconDict.Add(item.Key, item.Value);
                }
            }
        }

        protected virtual void SetFolderIconInfo(string guid, string childFolderPath, FolderIcon iconSetting)
        {
            iconSetting.folder = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(childFolderPath) as DefaultAsset;
        }

        public static IconConfigger Create<T>() where T : IconConfigger, new()
        {
            return new T();
        }
    }

    public class FolderIconConfigger : IconConfigger
    {
        public override string[] Paths { get => new string[] { FolderIconConstants.CustomIconPath }; }

        protected override void SetFolderIconInfo(string guid, string childFolderPath, FolderIcon iconSetting)
        {
            base.SetFolderIconInfo(guid, childFolderPath, iconSetting);
            iconSetting.folderIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid));
        }

    }

    public class OverLayIconConfigger : IconConfigger
    {
        public override string[] Paths { get => new string[] { FolderIconConstants.OverLayPath }; }

        protected override void SetFolderIconInfo(string guid, string childFolderPath, FolderIcon iconSetting)
        {
            base.SetFolderIconInfo(guid, childFolderPath, iconSetting);
            iconSetting.overlayIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid));
            string name = iconSetting.overlayIcon.name.ToLower();
            iconSetting.folderIcon = DefaultFolders[Mathf.Min(name[0] - 'a', DefaultFolders.Length - 1)];
        }
    }
}
