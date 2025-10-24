/* 命令基类 By ShuaiGuo
  2025年10月23日
*/

using GameCore.Actor;

namespace GameCore.Commands
{
    public abstract class CommandBase
    {
        public abstract void Execute(Brain executor);
    }
}