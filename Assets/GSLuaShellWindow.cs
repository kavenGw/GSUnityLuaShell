using System;
using System.Text;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using XLua;
using Debug = System.Diagnostics.Debug;

namespace GSLuaShell
{
    public partial class GSLuaShellWindow : EditorWindow
    {
        public static GSLuaShellWindow Create()
        {
            return GetWindow<GSLuaShellWindow>("GSLuaShell");
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
                if (textEditor != null)
                {
                    return textEditor.text;
                }

                return "";
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
        
        [SerializeField] TreeViewState treeViewState;
        private GSLuaShellTreeView treeView;

        private void Awake()
        {
            requestFocusOnTextArea = true;
        }

        private void OnEnable()
        {
            if (treeViewState == null)
                treeViewState = new TreeViewState ();

            treeView = new GSLuaShellTreeView(treeViewState);
        }

        [SerializeField]
        private Vector2 scrollPos = Vector2.zero;
        private void OnGUI()
        {
            Console.Write(position);
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
                    ParseResult();
                }
            }
            EditorGUILayout.EndHorizontal();
        
            Console.Write(position);
            EditorGUILayout.BeginScrollView(scrollPos);
            treeView.OnGUI(new Rect(0,0, position.width, position.height-100));
            EditorGUILayout.EndScrollView();
            
            Console.Write(position);
            textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
            GUILayout.TextArea(text,GUILayout.MinHeight(100));
            Console.Write(position);
        }

        private void ParseResult()
        {
            treeView.addChild(string.Format("{0}{1}\n", GSLuaShellConst.CommandName, text));
            object[] objects = exec(text);
            if (objects == null)
            {
            
            }
            else
            {
                foreach (var obj in objects)
                {
                    treeView.addChild(string.Format("{0}\n", obj.ToString()));
                }
            }
            
            treeView.Reload();
            text = "";
            Repaint();
        }

        public void Test(string command)
        {
            text = command;
            ParseResult();
        }
    }
}