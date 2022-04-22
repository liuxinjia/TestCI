using System.IO;
namespace Cr7Sund.EditorUtils
{
    using UnityEngine;
    using UnityEditor;
    using UnityEngine.UIElements;
    using System.Reflection;
    using System.Collections.Generic;
    using instance.id.EATK;
    using instance.id.EATK.Extensions;
    using System;

    using UnityEngine;
    using UnityEditor;

    public class PeferenceSettingWindow : EditorWindow
    {

        [MenuItem("TestSLG/PeferenceSetting #t")]
        private static void ShowWindow()
        {
            var window = GetWindow<PeferenceSettingWindow>();
            window.titleContent = new GUIContent("PeferenceSetting");
            window.DrawWindow();
            window.Show();
        }

        private void DrawWindow()
        {
            if (rootVisualElement.childCount < 1)
                rootVisualElement.Add(PeferenceSettingWindow.DrawElements());
        }
        private void OnFocus() => DrawWindow();

        #region  DrawTools
        public static VisualElement DrawElements()
        {
            VisualElement foldOut = null;
            var assembly = typeof(PeferenceSettingWindow).Assembly;
            var types = assembly.GetTypes();
            var elements = new List<VisualElement>();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                var props = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                elements.Clear();
                foreach (var propInfo in props)
                {
                    if (!Attribute.IsDefined(propInfo, typeof(PerferenceSettingMethodAttribute))) continue;//break; don't do that
                    var attr = System.Attribute.GetCustomAttribute(propInfo, typeof(PerferenceSettingMethodAttribute));
                    if (attr != null)
                    {
                        if (attr is PerferenceSettingMethodAttribute perferSettingAttr)
                        {
                            var value = EditorPrefs.GetBool(propInfo.Name, false);
                            VisualElement propertyElement = FieldFactory.CreateField(propInfo.PropertyType, value, (newValue) =>
                                                            {
                                                                // fieldInfo.SetValue(null, newValue);
                                                                // EditorPrefs.GetBool();
                                                                string name = propInfo.Name;
                                                                EditorPrefs.SetBool(name, (bool)newValue);
                                                                var refreshMethodInfo = type.GetMethod(perferSettingAttr.refreshMethod);
                                                                if (refreshMethodInfo != null) refreshMethodInfo.Invoke(null, null);
                                                            }, propInfo.Name);
                            elements.Add(propertyElement);
                        }
                    }
                }
                if (elements.Count > 0)
                {
                    foldOut = CreateAnimFoldOut(type.Name);
                    var foldOutContainer = CreateAnimFoldoutContainer();

                    foreach (var element in elements)
                    {
                        foldOutContainer.Add(element);
                    }
                    foldOut.Add(foldOutContainer);

                }
            }

            return foldOut;
        }

        public static AnimatedFoldout CreateAnimFoldOut(string title)
        {
            // -- @AnimatedFoldout ---------------------------------------
            // -- The AnimatedFoldout is a custom element type I created
            // -- that works just like a normal foldout, but the action
            // -- of showing/hiding the contents is animated, of course!
            new AnimatedFoldout { text = title, value = false }.Create(out var imageAnimationAnimatedFoldout).ToUSS(nameof(imageAnimationAnimatedFoldout));
            imageAnimationAnimatedFoldout.JumpToCode("AnimatedFoldout", true, jumpUSS: true,
                additionalMenus: new Dictionary<string, Action> { { "Replay Animation", FoldoutAnimation } });
            imageAnimationAnimatedFoldout.RegisterValueChangedCallback(evt =>
            {
                imageAnimationAnimatedFoldout.text = evt.newValue
                    ? "Animated Foldout"
                    : title;
            });
            return imageAnimationAnimatedFoldout;

            // --- End Animated Foldout ---------
        }

        private static void FoldoutAnimation()
        {
            throw new NotImplementedException();
        }

        private static VisualElement CreateAnimFoldoutContainer()
        {
            new VisualElement().Create(out var foldoutLabelContainer).ToUSS(nameof(foldoutLabelContainer));

            return foldoutLabelContainer;
        }

        #endregion

        #region  Test
        public static int TestFullReflection()
        {
            int count = 0;
            var assembly = typeof(PeferenceSettingWindow).Assembly;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                var props = type.GetProperties();
                foreach (var propInfo in props)
                {
                    var attr = System.Attribute.GetCustomAttribute(propInfo, typeof(PerferenceSettingMethodAttribute));
                    if (attr != null)
                    {
                        var value = propInfo.GetValue(null);
                        count++;
                    }
                }
            }

            return count;
        }

        public static int TestReflection_Optimized()
        {
            int count = 0;
            var assembly = typeof(PeferenceSettingWindow).Assembly;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                var props = type.GetProperties();
                foreach (var propInfo in props)
                {
                    if (Attribute.IsDefined(propInfo, typeof(PerferenceSettingMethodAttribute))) //Decrease reflection a lot
                    {
                        var attr = System.Attribute.GetCustomAttribute(propInfo, typeof(PerferenceSettingMethodAttribute), false);
                        if (attr != null)
                        {
                            var value = propInfo.GetValue(null);
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public static int TestReflection_Optimized2()
        {
            int count = 0;
            var assembly = typeof(PeferenceSettingWindow).Assembly;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                var props = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var propInfo in props)
                {
                    if (Attribute.IsDefined(propInfo, typeof(PerferenceSettingMethodAttribute)))
                    {
                        var attr = System.Attribute.GetCustomAttribute(propInfo, typeof(PerferenceSettingMethodAttribute), false);
                        if (attr != null)
                        {

                            var value = propInfo.GetValue(null);
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public static int TestReflection_Optimized3()
        {
            int count = 0;
            var assembly = typeof(PeferenceSettingWindow).Assembly;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                if (Attribute.IsDefined(type, typeof(PerferenceSettingMethodAttribute))) //Decrease reflection a lot
                {
                    var props = type.GetProperties();
                    foreach (var propInfo in props)
                    {
                        if (Attribute.IsDefined(propInfo, typeof(PerferenceSettingMethodAttribute)))
                        {
                            var attr = System.Attribute.GetCustomAttribute(propInfo, typeof(PerferenceSettingMethodAttribute), false);
                            if (attr != null)
                            {

                                var value = propInfo.GetValue(null);
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }


        public static VisualElement TestReflection_WithouAttribute()
        {
            VisualElement foldOut = null;
            var assembly = typeof(PeferenceSettingWindow).Assembly;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                var props = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var propInfo in props)
                {

                }
            }

            return foldOut;
        }

        public static VisualElement TestReflection_WithouProperteis()
        {
            VisualElement foldOut = null;
            var assembly = typeof(PeferenceSettingWindow).Assembly;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;

            }

            return foldOut;
        }
        #endregion
    }






    internal class AttributsChecker
    {
        /// <summary>
        /// PerferenceSettingMethodAttribute  only decorate static variable
        /// </summary>
        /// <param name="root"></param>
        public static void CheckPerferenceSettingAttributeVail(VisualElement root)
        {
            // var assembly = typeof(PerferenceSettingNode).Assembly;
            // var types = assembly.GetTypes();
            // foreach (var type in types)
            // {
            //     var fields = type.GetFields(BindingFlags.Static);
            //     foreach (var field in fields)
            //     {
            //         var attr = System.Attribute.GetCustomAttribute(field, typeof(PerferenceSettingMethodAttribute));
            //         if (attr != null)
            //         {
            //             if (attr != null)
            //             {

            //             }
            //         }
            //     }
            // }

        }
    }

}