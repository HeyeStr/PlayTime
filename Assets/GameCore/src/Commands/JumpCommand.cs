/* 跳跃命令 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;
using UnityEngine;

namespace GameCore.Commands
{
    public class JumpCommand : CommandBase
    {
        public JumpCommand(int jumpSpeed)
        {
            _JumpSpeed = jumpSpeed;
        }

        public override void Execute(Brain executor)
        {
            if (!executor.Locomotion) return;
            executor.Locomotion.Velocity += _JumpSpeed * executor.transform.up;
        }

        #region Fields

        private readonly int _JumpSpeed;

        #endregion Fields
    }
}