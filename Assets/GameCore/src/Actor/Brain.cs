/* Brain只负责作为各个组件的中枢 By ShuaiGuo
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
        #region Capabilities

        public Locomotion      Locomotion;
        public AnimatorManager AnimatorManager;

        #endregion Capabilities

        #region Field

        public Animator            ActorAnimator;
        public CharacterController ActorController;

        #endregion Field
    }
}