using System.Collections.Generic;
using XLua;

public class GSStateM
{
        private Dictionary<string, GSState> mStates = new Dictionary<string, GSState>();
        private GSState mCurState = null;

        public GSStateM()
        {
                
        }

        ~GSStateM()
        {
                this.destroy();
        }
        
        public void destroy()
        {
                mCurState = null;
                foreach (var state in mStates.Values)
                {
                        state.destroy();
                }
                mStates.Clear();
        }

        public void update(params object[] parameters)
        {
                
                if (mCurState != null)
                {
                        mCurState.update(parameters);
                }
        }

        public override string ToString()
        {
                return string.Format("{0} {1}", mCurState, base.ToString());
        }

        public void add(GSState state)
        {
                GSLogTool.dFormat("GSStateM.add","state:{0}",state);
                string name = state.Name;
                if (mStates.ContainsKey(name))
                {
                        GSLogTool.eFormat("GSStateM.add","name:{} already exit",name);
                        return;
                }
                
                mStates.Add(name,state);
        }

        public void remove(string name)
        {
                GSLogTool.dFormat("GSStateM.remove","name:{0}",name);
                if (!mStates.ContainsKey(name))
                {
                        GSLogTool.eFormat("GSStateM.remove","name:{} not exit",name);
                        return;
                }
                
                mStates.Remove(name);
        }

        public void changeTo(string name,params object[] parameters)
        {
                if (GSTest.GSDEBUG)
                {
                        GSLogTool.wFormat("GSStateM.changeTo","name:{0} parameters:{1}",name,parameters);
                }
                else
                {
                        GSLogTool.dFormat("GSStateM.changeTo","name:{0} parameters:{1}",name,parameters);
                }
                if (!mStates.ContainsKey(name))
                {
                        GSLogTool.eFormat("GSStateM.changeTo","name:{} not exit",name);
                        return;
                }

                if (mCurState != null)
                {
                        mCurState.onExit();
                }

                mCurState = mStates[name];
                
                mCurState.onEnter(parameters);
        }

        public void callCurState(string funcName,params object[] parameters)
        {
                if (mCurState == null)
                {
                        GSLogTool.eFormat("GSStateM.changeTo","mCurState == null");
                        return;
                }

                mCurState.call(funcName, parameters);
        }

        public void checkAndCallCurStae(string stateName, string funcName, params object[] parameters)
        {
                this.changeTo(stateName,parameters);
                this.callCurState(funcName,parameters);
        }

        public GSState getCurState()
        {
                return mCurState;
        }

        public string getCurStateName()
        {
                if (mCurState == null)
                {
                        return "";
                }
                return mCurState.Name;
        }

        public LuaTable getCurStateLua()
        {
                return mCurState.LuaTable;
        }
}