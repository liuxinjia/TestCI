using System.Collections;
using System.Collections.Generic;
using Cr7Sund.EditorEnhanceTools;
using UnityEditor;
using UnityEngine;
namespace Cr7Sund.AssetPipeline
{
    public class IconImporter : AssetPostprocessor
    {
        bool isFolderIcon => assetPath.StartsWith(FolderIconConstants.FolderIconPath);

        void OnPreprocessTexture()
        {
            if (!assetPath.StartsWith(FolderIconConstants.FolderPath)) return;
            // if(assetPath.IndexOf(folderName) != -1) return;

            TextureImporter importer = assetImporter as TextureImporter;

            // Since we require the icons located in the Editor Default Resources
            // Object asset = EditorGUIUtility.Load(importer.assetPath);

            // BUG: Unity can not create texture if using SetTextureSettings
            // var textureSetting = new TextureImporterSettings()
            // {
            //     textureType = TextureImporterType.Sprite,
            //     alphaIsTransparency = true,
            //     mipmapEnabled = false,
            //     readable = true,
            // };
            // importer.SetTextureSettings(textureSetting);

            importer.textureType = TextureImporterType.GUI;
            importer.alphaIsTransparency = true;
            importer.mipmapEnabled = false;
            importer.isReadable = true;

            SetPlatformSettings(importer);
        }

        private void SetPlatformSettings(TextureImporter importer)
        {
            var texturePlatformSettings = new List<TextureImporterPlatformSettings>(ConstDefines.BuildPlatforms.Length + 1);
            texturePlatformSettings.Add(importer.GetDefaultPlatformTextureSettings());
            foreach (var platform in ConstDefines.BuildPlatforms)
            {
                texturePlatformSettings.Add(importer.GetPlatformTextureSettings(platform));
            }

            foreach (var texPlatformSetting in texturePlatformSettings)
            {
                texPlatformSetting.format = TextureImporterFormat.RGBA32;
                texPlatformSetting.crunchedCompression = false;
                texPlatformSetting.overridden = true;
                texPlatformSetting.maxTextureSize = isFolderIcon ? 256 : 128;
            }


            foreach (var texPlatformSetting in texturePlatformSettings)
            {
                importer.SetPlatformTextureSettings(texPlatformSetting);
            }
        }
    }
}