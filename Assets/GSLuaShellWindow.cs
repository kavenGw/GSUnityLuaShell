using System;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using XLua;
using Debug = System.Diagnostics.Debug;

namespace GSLuaShell
{
    public partial class GSLuaShellWindow : EditorWindow
    {
        public static void Create()
        {
            GSLuaShellWindow window = GetWindow<GSLuaShellWindow>("GSLuaShell");
            // window.setLuaEnvDelegate(luaEnvDelegate);
        }

        bool requestFocusOnTextArea = false;
        public GSLuaShellWindow()
        {
            autoCompleteBox = new GSLuaShellAutoCompleteBox();
        }

        #region Text
        [SerializeField]
        private TextEditor textEditor;
        private string text
        {
            get
            {
                return textEditor.text;
            }
            set
            {
                if (textEditor != null)
                {
                    textEditor.text = value;
                }
            }
        }
        #endregion

        #region AutoCompleteBox
        private GSLuaShellAutoCompleteBox autoCompleteBox;
        #endregion

        private void Awake()
        {
            requestFocusOnTextArea = true;
        }

        [SerializeField]
        private Vector2 scrollPos = Vector2.zero;
        private void OnGUI()
        {
            
            GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), GSLuaShellStyle.backgroundTexture, ScaleMode.StretchToFill);
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Clear", EditorStyles.toolbarButton))
                {
                    text = "";
                }

                if (GUILayout.Button("Run", EditorStyles.toolbarButton))
                {
                    ParseResult(exec(text));
                }
            }
            EditorGUILayout.EndHorizontal();
            textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
            
            GUI.SetNextControlName(GSLuaShellConst.ConsoleTextAreaControlName);
            GUILayout.TextArea(text, GSLuaShellStyle.textAreaStyle, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            
        }

        private void ParseResult(object[] objects)
        {
            text = text + "\n";
            if (objects == null)
            {
                Console.Write(objects);
            }
            else
            {
                foreach (var obj in objects)
                {
                    text = text + obj.ToString();
                }
            }
        }
    }
}