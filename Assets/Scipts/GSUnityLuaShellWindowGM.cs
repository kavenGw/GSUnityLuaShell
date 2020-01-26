using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GSUnityLuaShell
{

    public sealed class GSUnityLuaShellGMItem : Attribute
    {
        public string macros;
        
        public GSUnityLuaShellGMItem(string _macros)
        {
            this.macros = _macros;
        }
    }
    
    public partial class GSUnityLuaShellWindow
    {
        Dictionary<string,MethodInfo> allGM = new Dictionary<string, MethodInfo>();
        public void InitGM()
        {
            allGM.Clear();
            MethodInfo[] allMethodInfo = this.GetType().GetMethods();
            foreach (var methodInfo in allMethodInfo)
            {
                GSUnityLuaShellGMItem gmItem = methodInfo.GetCustomAttribute<GSUnityLuaShellGMItem>();
                if (gmItem != null)
                {
                    string macros = gmItem.macros;
                    if (allGM.ContainsKey(macros))
                    {
                        Debug.LogError($"GSUnityLuaShellWindow.InitGM repeat macros:{macros}");
                        continue;
                    }
                    allGM.Add(macros,methodInfo);
                }
            }
        }

        public object[] ExecGM(string command)
        {
            if (!command.StartsWith("$"))
            {
                Debug.LogError($"GSUnityLuaShellWindow.ExecGM error command:{command}");
                return null;
            }

            command = command.Substring(1, command.Length - 1);
            MethodInfo methodInfo = null;
            allGM.TryGetValue(command, out methodInfo);
            if (methodInfo == null)
            {
                return Exec($"{command}()");
            }
            
            return methodInfo.Invoke(this,null) as object[];
        }
        
        [GSUnityLuaShellGMItem(("r"))]
        public object[] Reload()
        {
            return null;
        }
        
        [GSUnityLuaShellGMItem(("g"))]
        public object[] G()
        {
            string command = @"
                temp_value = _G
                local result = {}  
                for a,b in pairs(_G) do
                    table.insert(result,string.format('%s: %s',a,b))
                end
                return table.concat(result,'\n')
            ";
            return Exec(command);
        }
    }

}