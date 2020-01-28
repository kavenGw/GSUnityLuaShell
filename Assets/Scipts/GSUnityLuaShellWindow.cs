using System.Runtime.InteropServices;
using GS.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Serialization;

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
            treeView.OnGUI(new Rect(0,0, position.width, position.height-GSUnityLuaShellConst.InputFiledHeight));
            EditorGUILayout.EndScrollView();
            
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
                foreach (var obj in objects)
                {
                    if (obj == null)
                    {
                        treeView.AddChild("nil");
                        continue;
                    }

                    treeView.AddChild(obj.ToString());
                }
            }
            
            treeView.Reload();
            Text = "";
        }

        public void Test(string command)
        {
            Text = command;
            ParseResult();
        }
    }
}