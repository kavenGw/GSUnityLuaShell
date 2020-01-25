using System;
using XLua;

namespace GSLuaShell
{
    
    public partial class GSLuaShellWindow
    {
        public delegate LuaEnv GSLuaShellDelegate();
            
        // private GSLuaShellDelegate _luaEnvDelegate;
        
        // public void setLuaEnvDelegate(GSLuaShellDelegate luaEnvDelegate)
        // {
        //     _luaEnvDelegate = luaEnvDelegate;
        // }

        public object[] exec(string commond)
        {
            // if (_luaEnvDelegate == null)
            // {
            //     return new object[]{"_luaEnvDelegate is null"};
            // }
            LuaEnv luaEnv = null;
            try
            {
                // luaEnv = _luaEnvDelegate();
                luaEnv = GSLuaShell.GetLuaEnv();
            }
            catch (Exception e)
            {
                return new object[]{"getLuaEnvFailed",e};
            }

            if (luaEnv == null)
            {
                return new object[]{"luaEnv is null,please check your luaEnv avaliable"};
            }
            return luaEnv.DoString(commond);
        }
    }
}