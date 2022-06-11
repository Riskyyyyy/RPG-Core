﻿using RPG.Editor;
using UnityEngine;

namespace Station
{
    public class InteractionTab
    {
        #region [[FIELDS]]
        private static int _toolBarIndex;
        private string _categoryName;
        private object _selecteditem;
        #endregion
    
        public static void DrawTab()
        {
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            {
                DrawLeftBar();
                DrawProperties();
            }
            GUILayout.EndHorizontal();
        }


        #region [[ DRAW SECTIONS ]]
        private static void DrawLeftBar()
        {
            GUILayout.BeginVertical("box",GUILayout.Width(EditorStatic.LIST_VIEW_WIDTH),GUILayout.ExpandHeight(true));
            {
                var  toolbarOptions = new GUIContent[1];
                toolbarOptions[0] = new GUIContent(RpgEditorStatic.INTERACTION_TAB_CONFIGURATION,null, "");
      
                var previousIndex = _toolBarIndex;
                var height = 40 * toolbarOptions.Length;
                _toolBarIndex = GUILayout.SelectionGrid(_toolBarIndex, toolbarOptions,1,EditorStatic.ToolBarStyle,GUILayout.Height(height));
                if(_toolBarIndex != previousIndex)EditorStatic.ResetFocus();
            }
            GUILayout.EndVertical();
        }
    
        private static void DrawProperties()
        {
            switch (_toolBarIndex)
            {
                case 0:
                    InteractionConfigEditor.DrawContent();
                    break;
       
                
            }
        }
        #endregion
    }
}
