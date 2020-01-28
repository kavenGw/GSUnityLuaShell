using System.Collections;
using PlayFab.PfEditor;
using UnityEditor;
using XLua;

namespace GSUnityLuaShell
{
    public static class GSUnityLuaShell
    {
        [MenuItem("Window/GSUnityLuaShell")]
        private static void CreateWindow()
        {
            // GSLuaShellWindow.Create(()=>getLuaEnv());
            GSUnityLuaShellWindow.Create();
        }
        
        [MenuItem("Window/GSUnityLuaShellTest")]
        private static void Test()
        {
            EditorCoroutine.Start(_Test());
        }

        private static IEnumerator _Test()
        {
            yield return null;
            GSUnityLuaShellWindow unityLuaShellWindow = GSUnityLuaShellWindow.Create();
            for (int i = 0; i <= 100000; i++)
            {
                unityLuaShellWindow.Test(string.Format("return {0}+{1}",i,i+1));
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