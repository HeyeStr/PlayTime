/* 进入影子模式 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;

namespace GameCore.Commands
{
    public class EntryShadowCommand : CommandBase
    {
        public override void Execute(Brain executor)
        {
            if (!executor.InteractWithShadow) return;
            executor.InteractWithShadow.Interact();
        }
    }
}