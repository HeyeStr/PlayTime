/* 传送指令 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;
using UnityEngine;

namespace GameCore.Commands
{
    public class TeleProtCommand : CommandBase
    {
        public TeleProtCommand(Vector3 teleProtPosition)
        {
            _TeleProtPosition = teleProtPosition;
        }

        public override void Execute(Brain executor)
        {
            if (executor.ActorController)
            {
                executor.ActorController.enabled = false;
                executor.transform.position      = _TeleProtPosition;
                executor.ActorController.enabled = true;
            }
            else
            {
                executor.transform.position = _TeleProtPosition;
            }
        }

        #region Fields

        private readonly Vector3 _TeleProtPosition;

        #endregion Fields
    }
}