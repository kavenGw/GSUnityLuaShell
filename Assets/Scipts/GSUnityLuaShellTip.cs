using System;

namespace GSUnityLuaShell
{
    public class GSUnityLuaShellTip
    {
        private string[] Macros = new string[]
        {
            "and",
            "or",
            "not",
            "true",
            "false",
            "nil"
        };
        
        private static GSUnityLuaShellTip mInstance;
        public static GSUnityLuaShellTip GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new GSUnityLuaShellTip();
            }

            return mInstance;
        }

        public string[] getTips(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            if (text.StartsWith("$"))
            {
                return null;
            }
            return null;
        }
    }
}