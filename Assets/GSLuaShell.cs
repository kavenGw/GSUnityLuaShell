using UnityEditor;
using UnityShell;
using XLua;

namespace GSLuaShell
{
    public static class GSLuaShell
    {
        [MenuItem("Window/LuaShell")]
        private static void CreateWindow()
        {
            // GSLuaShellWindow.Create(()=>getLuaEnv());
            GSLuaShellWindow.Create();
        }

        public static LuaEnv GetLuaEnv()
        {
            return GSLuaState.getInstance().getLuaState();
        }
    }
}