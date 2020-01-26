using System;
using System.Text;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using XLua;
using Debug = System.Diagnostics.Debug;

namespace GSUnityLuaShell
{
    public partial class GSUnityLuaShellWindow : EditorWindow
    {
        public static GSUnityLuaShellWindow Create()
        {
            return GetWindow<GSUnityLuaShellWindow>("GSLuaShell");
            // window.setLuaEnvDelegate(luaEnvDelegate);
        }

        public GSUnityLuaShellWindow()
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
        private GSUnityLuaShellTreeView treeView;

        private void Awake()
        {
        }

        private void OnEnable()
        {
            if (treeViewState == null)
                treeViewState = new TreeViewState ();

            treeView = new GSUnityLuaShellTreeView(treeViewState);
            InitGM();
        }

        [SerializeField]
        private Vector2 scrollPos = Vector2.zero;
        private void OnGUI()
        {
            HandleKeyboard();
            GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), GSUnityLuaShellStyle.backgroundTexture, ScaleMode.StretchToFill);
           
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Clear", EditorStyles.toolbarButton))
                {
                    // text = "";
                    treeView.clear();
                }

                // if (GUILayout.Button("Run", EditorStyles.toolbarButton))
                // {
                //     ParseResult();
                // }
            }
            EditorGUILayout.EndHorizontal();
        
            EditorGUILayout.BeginScrollView(scrollPos);
            treeView.OnGUI(new Rect(0,0, position.width, position.height-100));
            EditorGUILayout.EndScrollView();
            
            textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
            GUI.SetNextControlName(GSUnityLuaShellConst.InputTextAreaControlName);
            GUILayout.TextArea(text,GUILayout.MinHeight(100),GUILayout.MaxHeight(100));
            GUI.FocusControl(GSUnityLuaShellConst.InputTextAreaControlName);
        }

        private void ParseResult()
        {
            GSUnityLuaShellHistory.GetInstance().AddCommand(text);
            treeView.addChild($"{GSUnityLuaShellConst.CommandName}{text}");
            object[] objects = Exec(text);
            if (objects == null)
            {
            
            }
            else
            {
                foreach (var obj in objects)
                {
                    if (obj == null)
                    {
                        treeView.addChild("nil");
                        continue;
                    }

                    treeView.addChild(obj.ToString());
                }
            }
            
            treeView.Reload();
            text = "";
        }

        public void Test(string command)
        {
            text = command;
            ParseResult();
        }
    }
}