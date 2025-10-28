/* Brain只负责作为各个组件的中枢并执行指令 By ShuaiGuo
  2025年10月23日
*/

using System.Collections.Generic;
using GameCore.Commands;
using GameCore.MonoBehaviors;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameCore.Actor
{
    public class Brain : MonoBehaviour
    {
        #region UnityBehaviour

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
                ExecuteCommand();
            }
        }

        public void ExecuteCommand()
        {
            var command = CommandQueue.Dequeue();
            command.Execute(this);
        }

        #endregion PrivateMethods

        #region Capabilities

        public Locomotion         Locomotion;
        public InteractWithShadow InteractWithShadow;
        public AnimatorManager    AnimatorManager;
        public ModelManager       ModelManager;

        #endregion Capabilities

        #region Field

        public Queue<CommandBase>  CommandQueue  = new Queue<CommandBase>();
        public bool                ShouldExecute = true;
        public Animator            ActorAnimator;
        public CharacterController ActorController;

        #endregion Field
    }
}