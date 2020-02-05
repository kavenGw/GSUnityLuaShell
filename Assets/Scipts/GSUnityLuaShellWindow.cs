using System.Collections.Generic;
using System.Runtime.InteropServices;
using GS.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Serialization;
using XLua;
using XLua.LuaDLL;

namespace GSUnityLuaShell
{
    public partial class GSUnityLuaShellWindow : EditorWindow
    {
        public static GSUnityLuaShellWindow Create()
        {
            return GetWindow<GSUnityLuaShellWindow>("GSLuaShell");
            // window.setLuaEnvDelegate(luaEnvDelegate);
        }

        private TextEditor mTextEditor;

        private string mText = ""; 
        public string Text
        {
            get
            {
                return mText;
            }
            set { mText = value; }
        }

        [SerializeField] 
        TreeViewState treeViewState;
        private GSEditorTreeView treeView;

        private void OnEnable()
        {
            if (treeViewState == null)
                treeViewState = new TreeViewState ();

            treeView = new GSEditorTreeView(treeViewState);
            InitGM();
            InitStateM();
        }

        private Vector2 mScrollPos = Vector2.zero;
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
                    treeView.Clear();
                }

                // if (GUILayout.Button("Run", EditorStyles.toolbarButton))
                // {
                //     ParseResult();
                // }
            }
            EditorGUILayout.EndHorizontal();
        
            EditorGUILayout.BeginScrollView(mScrollPos);
            treeView.OnGUI(new Rect(0,0, position.width, position.height-GSUnityLuaShellConst.InputFiledHeight - 50));
            EditorGUILayout.EndScrollView();
            
            //TODO 修复回车导致对于\n
            if (Text.StartsWith("\n"))
            {
                Text = Text.Substring(1, Text.Length - 1);
            }
            mTextEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
            mTextEditor.text = Text;
            GUI.SetNextControlName(GSUnityLuaShellConst.InputTextAreaControlName);
            Text = GUILayout.TextArea(Text,GUILayout.MinHeight(GSUnityLuaShellConst.InputFiledHeight),GUILayout.MaxHeight(GSUnityLuaShellConst.InputFiledHeight));
            UpdateStateM();
        }

        public void ParseResult()
        {
            Debug.LogError($"ParseResult text:{Text}");
            GSUnityLuaShellHistory.GetInstance().AddCommand(Text);
            treeView.AddChild($"{GSUnityLuaShellConst.CommandName}{Text}");
            object[] objects = Exec(Text);
            if (objects == null)
            {
            
            }
            else
            {
                if (objects.Length == 1)
                {
                    object result = objects[0];
                    if (result is LuaTable)
                    {
                        treeView.AddChild(result.ToString());
                        ParseResultLuaTable(result as LuaTable,1);
                    }
                    else
                    {
                        treeView.AddChild( result == null ? "nil" : result.ToString());
                    }
                }
                else
                {
                    foreach (var obj in objects)
                    {
                        treeView.AddChild( obj == null ? "nil" : obj.ToString());
                    }
                }
            }
            
            treeView.Reload();
            treeView.SetSelection(new List<int>(){treeView.getRoot().children[treeView.getRoot().children.Count-1].id},TreeViewSelectionOptions.RevealAndFrame);
            Text = "";
        }

        public void Test(string command)
        {
            Text = command;
            ParseResult();
        }

        public void ParseResultLuaTable(LuaTable luaTable,int depth)
        {
            if (depth >= GSUnityLuaShellConst.LuaTableDepth)
            {
                return;
            }
            
            foreach (var key in luaTable.GetKeys())
            {
                var value = luaTable[key];
                if (value == null)
                {
                    treeView.AddChild($"{key}    nil",false,depth);
                    continue;
                }
                treeView.AddChild($"{key}    {value.ToString()}",false,depth);
                if (value is LuaTable)
                {
                    ParseResultLuaTable(value as LuaTable, depth+1);
                }
            }

            for (int i = 1; i <= luaTable.Length; i++)
            {
                object value = luaTable[i];
                if (value == null)
                {
                    break;
                }
                treeView.AddChild($"{value.ToString()}",false,depth);
                if (value is LuaTable)
                {
                    ParseResultLuaTable(value as LuaTable, depth+1);
                }
            }
        }
    }
}