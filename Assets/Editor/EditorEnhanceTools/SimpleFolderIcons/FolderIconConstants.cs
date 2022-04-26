using UnityEditor;
using UnityEngine;

namespace Cr7Sund.EditorEnhanceTools
{
    /// <summary>
    /// Contains all constant data, values and colours FolderIcon requires
    /// </summary>
    internal static class FolderIconConstants
    {


        // GUI
        public const float MAX_TREE_WIDTH = 118f;
        public const float MAX_PROJECT_WIDTH = 96f;

        public const float MAX_TREE_HEIGHT = 16f;
        public const float MAX_PROJECT_HEIGHT = 110f;

        // Colours
        public static readonly Color SelectedColor = new Color(0.235f, 0.360f, 0.580f);
        public static Color BackgroundColour = EditorGUIUtility.isProSkin
          ? new Color32(51, 51, 51, 255)
          : new Color32(190, 190, 190, 255);

        //Folder Icon Resources
        public const string FolderPath = "Assets/Editor Default Resources/Icons/SimpleFolder";
        public const string OverLayPath = "Assets/Editor Default Resources/Icons/SimpleFolder/Overlays"; // the overlay icon name
        public const string CustomIconPath = "Assets/Editor Default Resources/Icons/SimpleFolder/FolderIcons/Customs"; // the custom fodler icon icon name
        public const string DefaultIconPath = "Assets/Editor Default Resources/Icons/SimpleFolder/FolderIcons/DefaultFolders";// the default fodler icon icon name

        public const string FolderIconPath = "Assets/Editor Default Resources/Icons/FolderIcons"; //  the folder icon name
        public const string TmpDirectoryPath = "Assets/Editor Default Resources/Icons/TMPFolders";


    }
}
