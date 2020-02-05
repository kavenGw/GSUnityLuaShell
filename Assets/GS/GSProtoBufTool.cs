using System;
using System.IO;
using Google.Protobuf;

public class GSProtoBufTool
{
        public static bool load(string filePath,IMessage message)
        {
                try
                {
                        FileStream fs = new FileStream(filePath,FileMode.Open);
//                      CodedInputStream ctx = new CodedInputStream(fs);
                        message.MergeFrom(fs);
                }
                catch (Exception e)
                {
                        GSLogTool.exception("GSProtoBuffTool.init",e,string.Format("load file:{0} failed",filePath));
                        message = null;
                }
        
                if (message == null)
                {
                        GSLogTool.eFormat("GSProtoBuffTool.init","filePath:{0} failed",filePath);
                        return false;
                }
                else
                {
                        GSLogTool.dFormat("GSProtoBuffTool.init","filePath:{0} success",filePath);
                        return true;
                }
        }

        public static void save(string fileName, IMessage message)
        {
                if (GSFileTool.getFileExit(fileName))
                {
                        GSFileTool.deleteFile(fileName);
                }
                using (var output = File.Create(fileName))
                {
                        message.WriteTo(output);
                }

                GSLogTool.dFormat("GSProtoBuffTool.save","保存成功:{0}", fileName);
        }
}