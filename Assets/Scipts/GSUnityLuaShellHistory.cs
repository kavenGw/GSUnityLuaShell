using System.IO;
using Google.Protobuf;

namespace GSUnityLuaShell
{
    public class GSUnityLuaShellHistory
    {
        private GSUnityLuaShellCommands _commands;

        private static GSUnityLuaShellHistory _instance;
        public static GSUnityLuaShellHistory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GSUnityLuaShellHistory();
            }

            return _instance;
        }

        private GSUnityLuaShellHistory()
        {
            _commands = new GSUnityLuaShellCommands();
            CreateDirectoryIfNotExit();
            if (File.Exists(GSUnityLuaShellConst.HistoryFilePath))
            {
                GSProtoBuffTool.load(GSUnityLuaShellConst.HistoryFilePath, _commands);
            }
        }

        public void AddCommand(string command)
        {
            _commands.Commands.Add(command);

            if (_commands.Commands.Count > GSUnityLuaShellConst.HistoryCount)
            {
                _commands.Commands.RemoveAt(0);
            }

            if (File.Exists(GSUnityLuaShellConst.HistoryFilePath))
            {
                File.Delete(GSUnityLuaShellConst.HistoryFilePath);
            }
            
            using (var output = File.Create(GSUnityLuaShellConst.HistoryFilePath))
            {
                IMessage message = _commands;
                message.WriteTo(output);
            }
        }

        public void CreateDirectoryIfNotExit()
        {
            string path = GSUnityLuaShellConst.HistoryFilePath;
            int lastIndex = path.LastIndexOf (Path.AltDirectorySeparatorChar);
            string directory = path.Substring(0, lastIndex);
            Directory.CreateDirectory(directory);
        }
    }
}