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

        private void Update()
        {
            if (ShouldExecute)
            {
                _ExecuteCommands();
            }
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _ExecuteCommands()
        {
            while (CommandStream.Count > 0)
            {
                ExecuteCommand();
            }
        }

        public void ExecuteCommand()
        {
            var command = CommandStream.Dequeue();
            command.Execute(this);
        }

        #endregion PrivateMethods

        #region Capabilities

        public Locomotion         Locomotion;
        public Inventory          Inventory;
        public InteractWithShadow InteractWithShadow;
        public AnimatorManager    AnimatorManager;
        public PlayerManager      PlayerManager;

        #endregion Capabilities

        #region Fields

        public Queue<CommandBase>  CommandStream = new Queue<CommandBase>();
        public bool                ShouldExecute = true;
        public Animator            ActorAnimator;
        public CharacterController ActorController;
        public Rigidbody           Rigidbody;

        #endregion Fields
    }
}