/* Brain只负责作为各个组件的中枢并执行指令 By ShuaiGuo
  2025年10月23日
*/

using System.Collections.Generic;
using GameCore.Commands;
using GameCore.MonoBehaviors;
using UnityEngine;

namespace GameCore.Actor
{
    public class Brain : MonoBehaviour
    {
        #region UnityBehaviour

        private void Awake()
        {
        }

        private void Update()
        {
            if (ShouldExecute)
            {
                ExecuteCommands();
            }
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void ExecuteCommands()
        {
            while (CommandQueue.Count > 0)
            {
                var command = CommandQueue.Dequeue();
                command.Execute(this);
            }
        }

        #endregion PrivateMethods


        #region Capabilities

        public Locomotion      Locomotion;
        public AnimatorManager AnimatorManager;

        #endregion Capabilities

        #region Field

        public Queue<CommandBase>  CommandQueue  = new Queue<CommandBase>();
        public bool                ShouldExecute = true;
        public Animator            ActorAnimator;
        public CharacterController ActorController;

        #endregion Field
    }
}