/* 日志系统 By ShuaiGuo 
  2025年10月22日
*/

namespace GameCore.Utilities.Log
{
    public static class SuperDebug
    {
        private static readonly ILogSystem LogSystem = new UnityDebug();

        public static void Log(string message)
        {
            LogSystem.Log(message);
        }

        public static void LogWarning(string message)
        {
            LogSystem.LogWarning(message);
        }

        public static void LogError(string message)
        {
            LogSystem.LogError(message);
        }
    }
}
