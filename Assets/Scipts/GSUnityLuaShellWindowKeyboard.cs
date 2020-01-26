using UnityEngine;

namespace GSUnityLuaShell
{
    public partial class GSUnityLuaShellWindow
    {
        public void HandleKeyboard()
        {
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
                    ParseResult();
                    Repaint();
                }else if (curEvent.keyCode == KeyCode.UpArrow)
                {
                    
                }else if (curEvent.keyCode == KeyCode.DownArrow)
                {
                    
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