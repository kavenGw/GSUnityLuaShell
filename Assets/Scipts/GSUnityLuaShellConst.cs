using System;
using System.IO;
using UnityEngine;

namespace GSUnityLuaShell
{
    public static class GSUnityLuaShellConst
    {
        public const string CommandName = "> ";
        public const string InputTextAreaControlName = "GSUnityLuaShellInputTextAread";
        
        //历史记录保存文件
        public static string HistoryFilePath = Path.Combine(Application.dataPath,"Editor/GSUnityLuaShellCommands/GSUnityLuaShellCommands");
        //保存历史记录数量
        public const int HistoryCount = 100;
        
        // 输入框高度
        public const float InputFiledHeight = 100;
        
        // Lua Depth
        public const int LuaTableDepth = 3;
    }
}