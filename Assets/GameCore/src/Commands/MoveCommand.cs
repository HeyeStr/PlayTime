/* 移动指令 By ShuaiGuo
  2025年10月23日
*/

using GameCore.Actor;
using UnityEngine;

namespace GameCore.Commands
{
    public class MoveCommand : CommandBase
    {
        public MoveCommand()
        {
            _MoveDirection = Vector3.zero;
            _MoveAmount    = 0f;
        }

        public MoveCommand(Vector3 moveDirection, float moveAmount)
        {
            _MoveDirection = moveDirection;
            _MoveAmount    = moveAmount;
        }

        public override void Execute(Brain executor)
        {
            if (!executor.Locomotion) return;
            executor.Locomotion.Move(_MoveDirection, _MoveAmount);
            executor.Locomotion.Rotation(_MoveDirection);
        }

        public void Reset(Vector3 moveDirection, float moveAmount)
        {
            _MoveDirection = moveDirection;
            _MoveAmount    = moveAmount;
        }

        #region Field

        private Vector3 _MoveDirection;
        private float   _MoveAmount;

        #endregion Field
    }
}