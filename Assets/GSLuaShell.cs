using UnityEditor;
using UnityShell;

namespace GSLuaShell
{
    public class GSLuaShell : EditorWindow
    {
        [MenuItem("Window/LuaShell")]
        private static void CreateWindow()
        {
            GetWindow<GSLuaShell>("GSLuaShell");
        }
    }
}