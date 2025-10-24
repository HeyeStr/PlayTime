/* Unity日志系统 By ShuaiGuo 
  2025年10月22日
*/

using UnityEngine;

namespace GameCore.Utilities.Log
{
    public class UnityDebug : ILogSystem
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}