using Boo.Lang;
using UnityEditor.IMGUI.Controls;

namespace GSLuaShell
{
    public class GSLuaShellTreeView : TreeView
    {
        TreeViewItem root = new TreeViewItem {id = 0, depth = -1, displayName = "root"};
        private int id = 0;
        public GSLuaShellTreeView(TreeViewState treeViewState)
            : base(treeViewState)
        {
        }
        
        protected override TreeViewItem BuildRoot ()
        {
            
            // Return root of the tree
            return root;
        }

        public void addChild(string text)
        {
            id++;
            root.AddChild(new TreeViewItem{id = id,depth = -1,displayName = text});
        }
        
        
    }
}