using System;
using System.Collections;
using GS.Editor;
using UnityEngine;

namespace GSUnityLuaShell
{
    public class GSUnityLuaShellState : GSState
    {
        public GSUnityLuaShellState(string name):base(name)
        {
            
        }

        public virtual void onKeyEvent(GSUnityLuaShellWindow window,KeyCode keyCode)
        {
            
        }
    }
    
    //"输入"
    public class GSUnityLuaShellStatesIdle : GSUnityLuaShellState
    {
        public GSUnityLuaShellStatesIdle(string name):base(name)
        {
            
        }

        public override void update(params object[] parameters)
        {
            base.update(parameters);
            GSUnityLuaShellWindow window = parameters[0] as GSUnityLuaShellWindow;
            GUI.FocusControl(GSUnityLuaShellConst.InputTextAreaControlName);
            // if (!string.IsNullOrEmpty(window.text))
            // {
            //     window.ChangeToState(GSUnityLuaShellWindow.StateHint);
            // }
        }

        public override void onKeyEvent(GSUnityLuaShellWindow window,KeyCode keyCode)
        {
            base.onKeyEvent(window,keyCode);
            if(keyCode == KeyCode.Return)
            {
                window.ParseResult();
                window.Repaint();   
            }else if (keyCode == KeyCode.UpArrow)
            {
                window.ChangeToState(GSUnityLuaShellWindow.StateHistory);
            }
        }
    }
    
    //历史
    public class GSUnityLuaShellStatesHistory : GSUnityLuaShellState
    {
        public GSUnityLuaShellStatesHistory(string name):base(name)
        {
            
        }

        public override void update(params object[] parameters)
        {
            base.update(parameters);
            GSUnityLuaShellWindow window = parameters[0] as GSUnityLuaShellWindow;
            window.SelectWindow.OnGUI(0,window.position.height/2 - GSUnityLuaShellConst.InputFiledHeight,
                window.position.width / 2,window.position.height/2,
                GSUnityLuaShellStyle.backgroundTexture);
        }

        public override void onEnter(params object[] parameters)
        {
            base.onEnter(parameters);
            GSUnityLuaShellWindow window = parameters[0] as GSUnityLuaShellWindow;
            window.SelectWindow.SetText(GSUnityLuaShellHistory.GetInstance().GetAllCommands());
        }
        
        public override void onKeyEvent(GSUnityLuaShellWindow window,KeyCode keyCode)
        {
            base.onKeyEvent(window,keyCode);
            if(keyCode == KeyCode.Return)
            {
                var selectItem = window.SelectWindow.GetSelectItem();
                if (selectItem == null)
                {
                    return;
                }
                window.Text = selectItem.displayName;
                window.Repaint();   
                window.ChangeToState(GSUnityLuaShellWindow.StateIdle);
            }else if (keyCode == KeyCode.UpArrow)
            {
                window.SelectWindow.SelectItemByDiff(-1);
            }else if(keyCode == KeyCode.DownArrow)
            {
                window.SelectWindow.SelectItemByDiff(1);
            }
        }
    }
    
    //提示
    public class GSUnityLuaShellStatesHint : GSUnityLuaShellState
    {
        public GSUnityLuaShellStatesHint(string name):base(name)
        {
            
        }
    }
    
    public partial class GSUnityLuaShellWindow
    {
        private GSStateM stateM;

        public const string StateIdle = "StateIdle";
        public const string StateHistory = "StateHistory";
        public const string StateHint = "StateHint";

        public GSEditorSelectWindow SelectWindow;
        
        public void InitStateM()
        {
            SelectWindow = new GSEditorSelectWindow();
            stateM = new GSStateM();
            stateM.add(new GSUnityLuaShellStatesIdle(StateIdle));
            stateM.add(new GSUnityLuaShellStatesHistory(StateHistory));
            stateM.add(new GSUnityLuaShellStatesHint(StateHint));
            ChangeToState(StateIdle);
        }

        public void ChangeToState(string state)
        {
            Debug.LogError($"GSUnityLuaShellWindow.ChangeToState {state}");
            stateM.changeTo(state,this);
        }

        public void UpdateStateM()
        {
            stateM.update(this);
        }

        public void OnKeycodePress(KeyCode keyCode)
        {
            var state = stateM.getCurState() as GSUnityLuaShellState;
            state.onKeyEvent(this,keyCode);
        }
        
        public void HandleKeyboard()
        {
            if (!focusedWindow == this)
            {
                return;
            }
            var curEvent = Event.current;
            if (curEvent == null)
            {
                return;
            }
            // Debug.LogError($"GSUnityLuaShell.Update cur {curEvent.type}:{curEvent.keyCode}");

            if (curEvent.type == EventType.KeyDown)
            {
                if (curEvent.keyCode == KeyCode.Return && !curEvent.shift)
                {
                    OnKeycodePress(curEvent.keyCode);
                }else if (curEvent.keyCode == KeyCode.UpArrow || curEvent.keyCode == KeyCode.DownArrow)
                {
                    OnKeycodePress(curEvent.keyCode);
                }
                else
                {
                    return;
                }
                curEvent.Use();
            }
        }
    }
}