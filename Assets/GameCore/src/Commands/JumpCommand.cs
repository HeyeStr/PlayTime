/* 跳跃命令 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;
using UnityEngine;

namespace GameCore.Commands
{
    public class JumpCommand : CommandBase
    {
        public JumpCommand(float jumpHeight, Vector3 jumpDirection)
        {
            _JumpHeight    = jumpHeight;
            _JumpDirection = jumpDirection;
        }

        public override void Execute(Brain executor)
        {
            if (!executor.Locomotion) return;
            executor.SurfaceJump.Jump(_JumpHeight, _JumpDirection);
        }

        #region Fields

        private readonly float   _JumpHeight;
        private readonly Vector3 _JumpDirection;

        #endregion Fields
    }
}