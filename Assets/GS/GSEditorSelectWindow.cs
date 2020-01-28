using System;
using Boo.Lang;
using GSUnityLuaShell;
using PlayFab.PfEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GS.Editor
{
    public class GSEditorSelectWindow
    {
        private GSEditorTreeView mTreeView;
        private TreeViewState mTreeViewState = new TreeViewState ();
        private int mNowSelectIndex = 0;

        public GSEditorSelectWindow()
        {
            mTreeView = new GSEditorTreeView(mTreeViewState);
        }
        
        public void OnGUI(float x, float y,float width,float height,Texture2D bgTexture)
        {
            GUI.DrawTexture(new Rect(x, y, width, height), bgTexture, ScaleMode.StretchToFill);
            mTreeView.OnGUI(new Rect(x,y, width, height));
        }

        public void SetText(string[] result)
        {
            mTreeView.Clear(false);
            foreach (var text in result)
            {
                mTreeView.AddChild(text);
            } 
            mTreeView.Reload();
            mNowSelectIndex = result.Length ;
            RefreshSelect();
        }

        public void SelectItemByDiff(int diff)
        {
            mNowSelectIndex += diff;
            mNowSelectIndex = Math.Max(1, mNowSelectIndex);
            var root = mTreeView.getRoot();
            mNowSelectIndex = Math.Min(root.children.Count,mNowSelectIndex);
            RefreshSelect();
        }

        public TreeViewItem GetSelectItem()
        {
            var root = mTreeView.getRoot();
            if (mNowSelectIndex < 0 || mNowSelectIndex >= root.children.Count)
            {
                return null;
            }
            return root.children[mNowSelectIndex];
        }

        private void RefreshSelect()
        {
            var root = mTreeView.getRoot();
            if (mNowSelectIndex < 0 || mNowSelectIndex >= root.children.Count)
            {
                return;
            }
            mTreeView.SetSelection(new List<int>(){root.children[mNowSelectIndex].id},TreeViewSelectionOptions.RevealAndFrame);
        }
    }
}