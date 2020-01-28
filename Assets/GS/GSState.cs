using System;
using System.Collections;
using System.Collections.Generic;
using XLua;

public class GSState
{
    private String mName;
    
    public LuaTable LuaTable
    {
        get;
        private set;
    }
    private Dictionary<string, LuaFunction> mFuncs = new Dictionary<string, LuaFunction>();

    public GSState(string name,LuaTable luaTable = null)
    {
        mName = name;
        LuaTable = luaTable;
    }

    ~GSState()
    {
        this.destroy();
    }

    public void destroy()
    {
        foreach (var func in mFuncs.Values)
        {
            func.Dispose();
        }
        mFuncs.Clear();
		if (this.LuaTable != null) {
			this.LuaTable.Dispose ();
			this.LuaTable = null;
		}
    }

    public virtual void onEnter(params object[] parameters)
    {
        if (LuaTable != null)
        {
            this.call("onEnter",parameters);
        }
    }

    public virtual void onExit(params object[] parameters)
    {
        if (LuaTable != null)
        {
            this.call("onExit",parameters);
        }
    }

    public virtual void update(params object[] parameters)
    {
        if (LuaTable != null)
        {
            this.call("update",parameters);
        }
    }

    public void call(string funcName, params object[] parameters)
    {
        if (!mFuncs.ContainsKey(funcName))
        {
            if (LuaTable.ContainsKey(funcName) == false)
            {
                GSLogTool.eFormat("GSState.call","{0} funcName:{1} not eixt",this,funcName);
                return;
            }
            
            mFuncs[funcName] = LuaTable[funcName] as LuaFunction;;
        }
        
        ArrayList realParames = new ArrayList(parameters);
        realParames.Insert(0,this.LuaTable);
        mFuncs[funcName].Call (realParames.ToArray());
    }

    public override string ToString()
    {
        return string.Format("{0} {1}", mName, base.ToString());
    }

    public string Name
    {
        get { return mName; }
    }
}