/* 跳跃命令 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;

namespace GameCore.Commands
{
    public class JumpCommand : CommandBase
    {
        public JumpCommand(float jumpSpeed)
        {
            _JumpSpeed = jumpSpeed;
        }

        public override void Execute(Brain executor)
        {
            if (!executor.Locomotion) return;
            executor.Locomotion.Jump(_JumpSpeed);
        }

        #region Field

        private readonly float _JumpSpeed;

        #endregion Field
    }
}