using System;
using System.IO;
using UnityEngine;

namespace GSUnityLuaShell
{
    public static class GSUnityLuaShellConst
    {
        public const string CommandName = "> ";
        public const string InputTextAreaControlName = "GSUnityLuaShellInputTextAread";
        public static string HistoryFilePath = Path.Combine(Application.dataPath,"Editor/GSUnityLuaShellCommands/GSUnityLuaShellCommands");
        public const int HistoryCount = 100;
    }
}