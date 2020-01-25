using System.Collections;
using PlayFab.PfEditor;
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
        
        [MenuItem("Window/LuaShellTest")]
        private static void Test()
        {
            EditorCoroutine.Start(_Test());
        }

        private static IEnumerator _Test()
        {
            yield return null;
            GSLuaShellWindow luaShellWindow = GSLuaShellWindow.Create();
            for (int i = 0; i <= 100000; i++)
            {
                luaShellWindow.Test(string.Format("return {0}+{1}",i,i+1));
                yield return null;
            }
            yield return null;
        }

        public static LuaEnv GetLuaEnv()
        {
            return GSLuaState.getInstance().getLuaState();
        }
    }
}