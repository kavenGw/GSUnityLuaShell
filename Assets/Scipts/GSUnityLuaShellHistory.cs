using System.Collections;
using System.IO;
using System.Linq;
using Google.Protobuf;

namespace GSUnityLuaShell
{
    public class GSUnityLuaShellHistory
    {
        private GSUnityLuaShellCommands mCommands;

        private static GSUnityLuaShellHistory mInstance;
        public static GSUnityLuaShellHistory GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new GSUnityLuaShellHistory();
            }

            return mInstance;
        }

        private GSUnityLuaShellHistory()
        {
            mCommands = new GSUnityLuaShellCommands();
            CreateDirectoryIfNotExit();
            if (File.Exists(GSUnityLuaShellConst.HistoryFilePath))
            {
                GSProtoBufTool.load(GSUnityLuaShellConst.HistoryFilePath, mCommands);
            }
        }

        public void AddCommand(string command)
        {
            mCommands.Commands.Add(command);

            if (mCommands.Commands.Count > GSUnityLuaShellConst.HistoryCount)
            {
                mCommands.Commands.RemoveAt(0);
            }

            if (File.Exists(GSUnityLuaShellConst.HistoryFilePath))
            {
                File.Delete(GSUnityLuaShellConst.HistoryFilePath);
            }
            
            using (var output = File.Create(GSUnityLuaShellConst.HistoryFilePath))
            {
                IMessage message = mCommands;
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

        public string[] GetAllCommands()
        {
            return mCommands.Commands.ToArray();
        }
    }
}