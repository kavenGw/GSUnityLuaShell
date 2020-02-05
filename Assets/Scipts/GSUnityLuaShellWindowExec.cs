using System;
using System.Collections.Generic;
using System.Linq;
using XLua;

namespace GSUnityLuaShell
{
    
    public partial class GSUnityLuaShellWindow
    {
        public delegate LuaEnv GSLuaShellDelegate();

        private LuaTable mEnvTable = null;
            
        // private GSLuaShellDelegate _luaEnvDelegate;
        
        // public void setLuaEnvDelegate(GSLuaShellDelegate luaEnvDelegate)
        // {
        //     _luaEnvDelegate = luaEnvDelegate;
        // }

        public object[] Exec(string command)
        {
            
            if (command == null)
            {
                return new object[]{"command is null"};
            }

            if (command.StartsWith("$"))
            {
                return ExecGM(command);
            }
            
            // if (_luaEnvDelegate == null)
            // {
            //     return new object[]{"_luaEnvDelegate is null"};
            // }
            LuaEnv luaEnv = null;
            try
            {
                // luaEnv = _luaEnvDelegate();
                luaEnv = GSUnityLuaShell.GetLuaEnv();
            }
            catch (Exception e)
            {
                return new object[]{"getLuaEnvFailed",e};
            }

            if (luaEnv == null)
            {
                return new object[]{"luaEnv is null,please check your luaEnv avaliable"};
            }
            
            if (mEnvTable == null)
            {
                try
                {
                    mEnvTable = luaEnv.DoString(@"
                    local env = {}
                    setmetatable(env, {__index = _G}) 
                    return env
                ") [0] as LuaTable;
                }
                catch (Exception e)
                {
                    return new object[]{"init lua env failed",e};
                }
            }

            object[] objects = null;
            bool success = false;
            try
            {
                objects = luaEnv.DoString($"return {command}",env:mEnvTable);
                success = true;
            }
            catch (Exception e)
            {
            }

            if (!success)
            {
                try
                {
                    objects = luaEnv.DoString(command,env:mEnvTable);
                }
                catch (Exception e)
                {
                    return new object[]{$"command:{command}",e};
                }
            }
            
            return objects;
        }
    }
}