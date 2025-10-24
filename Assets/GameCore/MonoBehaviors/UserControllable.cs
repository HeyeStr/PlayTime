/* 表明收到玩家控制 By ShuaiGuo
  2025年10月24日
*/

using GameCore.Actor;
using GameCore.GlobalVars;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class UserControllable : MonoBehaviour
    {
        #region UnityBehavior

        private void Awake()
        {
            if (!ActorBrain)
            {
                ActorBrain = GetComponent<Brain>();
            }
        }

        private void Update()
        {
            while (G.GPlayerController.CommandStream.Count > 0)
            {
                var command = G.GPlayerController.CommandStream.Dequeue();
                command.Execute(ActorBrain);
            }
        }

        #endregion UnityBehavior

        #region Field

        public Brain ActorBrain;

        #endregion Field
    }
}