using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// In MenuItem method I cannot open popupwindow as I don't know mouse pointer location (as Event.current is null)
/// so I open menu in the next event, however how to detect next event? I could create EditorWindow, but it would require
/// to have that window opened all the time. So here is the workaround: using reflection we replace processEvent delegate in GUIUtility
/// to show the menu when needed
/// </summary>
namespace Cr7Sund.CreateWindow
{
    internal class CreateNewContextMenu
    {

        private static bool showContextMenuInNextEvent;

        private static CreatePopupWindowContent popupWindowContent;


        public static CreatePopupWindowContent CreateCreateWindowContent(bool isRecreated = false)
        {
            if (popupWindowContent == null || isRecreated) popupWindowContent = new CreatePopupWindowContent();


            return popupWindowContent;
        }

        public static void RegisterProcessEvent()
        {
            if (showContextMenuInNextEvent)
            {
                showContextMenuInNextEvent = false;

                PopupWindow.Show(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 0, 0),
                CreateCreateWindowContent());
            }
        }


        [MenuItem("Assets/Create New", false, -1)]
        private static void DoSomething()
        {
            showContextMenuInNextEvent = true;
        }
    }
}