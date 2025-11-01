/* 赋予运动能力 By ShuaiGuo
  2025年10月24日
*/

using GameCore.Actor;
using GameCore.GlobalVars;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class Locomotion : MonoBehaviour
    {
        #region UnityBehavior

        private void FixedUpdate()
        {
            if (Velocity.magnitude > 0.1f)
            {
                _HandleMove();
                _HandleRotation();
            }

            if (ShouldMockGravity)
            {
                _MockGravity();
            }
        }

        #endregion UnityBehavior

        #region PrivateMethods

        private void _HandleMove()
        {
            ActorBrain.ActorController.Move(Velocity * (MoveSpeed * DeltaTime));
            ActorBrain.AnimatorManager?.UpdateAnimatorMovementParameters(0, _MoveAmount);
        }

        private void _HandleRotation()
        {
            var rotationDirection = transform.InverseTransformDirection(Velocity);
            rotationDirection.y = 0;
            rotationDirection   = transform.TransformDirection(rotationDirection);

            if (rotationDirection == Vector3.zero)
            {
                rotationDirection = transform.forward;
            }

            var actorTransform = ActorBrain.ActorController.transform;
            var newRotation    = Quaternion.LookRotation(rotationDirection, actorTransform.up);

            ActorBrain.ActorController.transform.rotation = Quaternion.Slerp(
                ActorBrain.ActorController.transform.rotation,
                newRotation,
                RotationSpeed * DeltaTime);
        }

        private void _MockGravity()
        {
            Velocity.y += GravityForce * Time.deltaTime;

            if (IsGrounded)
            {
                Velocity.y = 0;
            }
        }

        #endregion PrivateMethods

        #region Fields

        public Brain   ActorBrain;
        public Vector3 Velocity;
        public float   MoveSpeed         = 4;
        public float   RotationSpeed     = 15;
        public float   GravityForce      = -10;
        public bool    ShouldMockGravity = true;

        private float _MoveAmount;

        private static float DeltaTime =>
            G.UseUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;

        private bool IsGrounded => ActorBrain.ActorController.isGrounded;

        #endregion Fields
    }
}