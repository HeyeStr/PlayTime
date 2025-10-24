/* 日志系统接口 By ShuaiGuo
  2025年10月22日
*/

namespace GameCore.Utilities.Log
{
    public interface ILogSystem
    {
        void Log(string        message);
        void LogWarning(string message);
        void LogError(string   message);
    }
}