using Boo.Lang;
using UnityEditor.IMGUI.Controls;

namespace GSUnityLuaShell
{
    public class GSUnityLuaShellTreeView : TreeView
    {
        private TreeViewItem root = null;
        private int id = 0;
        public GSUnityLuaShellTreeView(TreeViewState treeViewState)
            : base(treeViewState)
        {
            clear();
        }
        
        protected override TreeViewItem BuildRoot ()
        {
            
            // Return root of the tree
            return root;
        }

        public void addChild(string text)
        {
            string[] strs = text.Split('\n');
            foreach (var tempstr in strs)
            {
                id++;
                root.AddChild(new TreeViewItem{id = id,depth = -1,displayName = tempstr});
            }
            
        }

        public void clear()
        {
            root = new TreeViewItem {id = 0, depth = -1, displayName = "root"};
            this.addChild("History");
            Reload();
        }
    }
}