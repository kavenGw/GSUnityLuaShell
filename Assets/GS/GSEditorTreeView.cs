using UnityEditor.IMGUI.Controls;

namespace GS.Editor
{
    public class GSEditorTreeView : TreeView
    {
        private TreeViewItem mRoot = null;
        private int mNextId = 0;
        public GSEditorTreeView(TreeViewState treeViewState)
            : base(treeViewState)
        {
            Clear();
        }
        
        protected override TreeViewItem BuildRoot ()
        {
            return mRoot;
        }

        public void AddChild(string text,bool reload = false)
        {
            string[] strs = text.Split('\n');
            foreach (var tempstr in strs)
            {
                mNextId++;
                mRoot.AddChild(new TreeViewItem{id = mNextId,depth = -1,displayName = tempstr});
            }

            if (reload)
            {
                Reload();
            }
        }

        public void Clear(bool reload = true)
        {
            mRoot = new TreeViewItem {id = 0, depth = -1, displayName = "root"};
            AddChild("History");
            if (reload)
            {
                Reload();
            }
        }

        public TreeViewItem getRoot()
        {
            return mRoot;
        }
    }
}