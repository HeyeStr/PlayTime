/* 动画管理器 By ShuaiGuo
  2025年10月24日
*/

using UnityEngine;

namespace GameCore.Actor
{
    public class AnimatorManager : MonoBehaviour
    {
        #region UnityBehavior

        private void OnAnimatorMove()
        {
            if (!ApplyRootMotion) return;
            var velocity = ActorBrain.ActorAnimator.deltaPosition;
            ActorBrain.ActorController.Move(velocity);
            ActorBrain.transform.rotation *= ActorBrain.ActorAnimator.deltaRotation;
        }

        #endregion UnityBehavior

        public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
        {
            // ActorBrain.ActorAnimator.SetFloat(Horizontal, horizontalMovement, 0.1f, Time.deltaTime);
            ActorBrain.ActorAnimator.SetFloat(Vertical, verticalMovement, 0.1f, Time.deltaTime);
        }

        #region Field

        public Brain ActorBrain;
        public bool  ApplyRootMotion = false;

        // private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        #endregion Field
    }
}