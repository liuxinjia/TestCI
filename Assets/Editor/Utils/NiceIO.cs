namespace Cr7Sund.EditorEnhanceTools
{
    using UnityEditor;
    using UnityEngine;

    public static class NiceIO
    {
        /// <summary>
        /// This function will look in Assets/Editor Default Resources/ + path for the resource. 
        /// If not there, it will try the built-in editor resources by name.
        /// </summary>
        /// <param name="path">Assets/Editor Default Resources/ + pat</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadEditorAsset<T>(string path) where T : UnityEngine.Object
        {
            return EditorGUIUtility.Load(path) as T;
        }

        /// <summary>
        /// Try Get Asset Name Via GUID
        /// skip folder
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="assetName"></param>
        /// <returns>Is vaild asset, excluded guid</returns>
        public static bool TryGetAsseetNameViaGUID(string guid, out string assetName)
        {
            assetName = string.Empty;
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (AssetDatabase.IsValidFolder(path)) return false;

            int startIndex = path.LastIndexOf('/');
            int endIndex = path.IndexOf('.');
            assetName = path.Substring(startIndex + 1, endIndex - startIndex - 1);
            return true;
        }

        public static T LoadAssetViaGUID<T>(string guid) where T : UnityEngine.Object =>
              AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
        public static string GetAsseetNameViaRelativePath(string path)
        {
            int startIndex = path.LastIndexOf('/');
            int endIndex = path.IndexOf('.');
            string name = path.Substring(startIndex + 1, endIndex - startIndex);
            return name;
        }

        public static bool TryGetFolderNameViaGUID(string guid, out string folderName)
        {
            folderName = string.Empty;
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (!AssetDatabase.IsValidFolder(path)) return false;
            int startIndex = path.LastIndexOf('/');
            folderName = path.Substring(startIndex + 1);
            return true;
        }

        private static void Recursive(string folder)
        {
            Debug.Log(folder);
            var folders = AssetDatabase.GetSubFolders(folder);
            foreach (var fld in folders)
            {
                Recursive(fld);
            }
        }

        public static string GetRelativePathViaAbsolutePath(string absolutePath)
        {
            int localPathIndex = absolutePath.IndexOf("Assets");
            var path = absolutePath.Substring(localPathIndex, absolutePath.Length - localPathIndex);
            return path;
        }
    }
}