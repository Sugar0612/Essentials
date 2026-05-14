using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SUG_UnityCore
{
    public interface ILogger
    {
        void LogInfo(string mess);
        void LogWarning(string mess);
        void LogError(string mess);

        public void Log(string mess, [CallerMemberName] string member = "", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            Debug.Log($"[{System.IO.Path.GetFileName(file)}:{line} - {member}] {mess}");
        }

        public void LogWarning(string mess, [CallerMemberName] string member = "", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            Debug.LogWarning($"[{System.IO.Path.GetFileName(file)}:{line} - {member}] {mess}");
        }

        public void LogError(string mess, [CallerMemberName] string member = "", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            Debug.LogError($"[{System.IO.Path.GetFileName(file)}:{line} - {member}] {mess}");
        }
    }

    public class UnityLogger : ILogger
    {
        public void LogInfo(string mess) => Debug.Log($"{mess}");
        public void LogWarning(string mess) => Debug.LogWarning($"{mess}");
        public void LogError(string mess) => Debug.LogError($"{mess}");
    }

    public class FileLogger : ILogger
    {
        public void LogInfo(string mess) => WriteToFile($"[INFO] {mess}");
        public void LogWarning(string mess) => WriteToFile($"[WARNING] {mess}");
        public void LogError(string mess) => WriteToFile($"[ERROR] {mess}");

        private void WriteToFile(string mess) { }
    }
}