using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang;
using UnityEngine;
using XLua;

namespace GSUnityLuaShell
{

    public sealed class GSUnityLuaShellGMItem : Attribute
    {
        public string mMacros;
        
        public GSUnityLuaShellGMItem(string macros)
        {
            this.mMacros = macros;
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
                    string macros = gmItem.mMacros;
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
        
        [GSUnityLuaShellGMItem(("pl"))]
        public object[] Loaded()
        {
            return getValue("package.loaded");
        }
        
        [GSUnityLuaShellGMItem(("r"))]
        public object[] Reload()
        {
            string command = @"
            local all_loaded_packages = {}
            local except_packages = {
                ['package']=true,['coroutine']=true,
                ['_G']=true,['string']=true,
                ['table']=true,['math']=true,
                ['debug']=true,['utf8']=true,
                ['os']=true,['io']=true}
            for packageName,_ in pairs(package.loaded) do
                if not except_packages[packageName] then
                    table.insert( all_loaded_packages, packageName )
                end
            end
            for index,loaded_package in ipairs(all_loaded_packages) do
                print(loaded_package)
                package.loaded[loaded_package] = nil
                require(loaded_package)
            end
            return all_loaded_packages
            ";
            return getValue(command);
        }
        
        [GSUnityLuaShellGMItem(("g"))]
        public object[] G()
        {
            return getValue("_G");
        }

        private object[] getValue(string value)
        {
            object[] objects = Exec(value);
            if (objects.Length <= 0)
            {
                return new object[]{$"value:{value} not exit"};
            }

            System.Collections.Generic.List<object> results = new System.Collections.Generic.List<object>();
            printLuaValue(results,objects[0]);
            return results.ToArray();
        }

        private void printLuaValue(System.Collections.Generic.List<object> result,object obj)
        {
            if (obj is LuaTable)
            {
                LuaTable table = obj as LuaTable;
                foreach (var key in table.GetKeys())
                {
                    result.Add($"{key}    {table[key].ToString()}");
                }

                for (int i = 1; i <= int.MaxValue; i++)
                {
                    object value = table[i];
                    if (value == null)
                    {
                        break;
                    }
                    result.Add($"{table[i].ToString()}");
                }
            }
            else
            {
                result.Add(obj.ToString());
            }
        }
    }

}