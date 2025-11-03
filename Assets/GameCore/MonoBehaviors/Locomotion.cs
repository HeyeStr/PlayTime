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
            ActorBrain.ActorController.Move(Velocity * (MoveSpeed * G.DeltaTime));
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

            var newRotation = Quaternion.LookRotation(rotationDirection, PlayerTransform.up);

            PlayerTransform.rotation =
                Quaternion.Slerp(PlayerTransform.rotation, newRotation, RotationSpeed * G.DeltaTime);
        }

        private void _MockGravity()
        {
            Velocity.y += GravityForce * Time.deltaTime;

            if (IsGrounded)
            {
                Velocity.y = GravityForce * Time.deltaTime;
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
        // private readonly float _GroundCheckOffset = 0.5f;

        private Transform PlayerTransform => ActorBrain.ActorController.transform;

        private bool IsGrounded => ActorBrain.ActorController.isGrounded;

        // private bool IsGrounded => Physics.SphereCast(
        //     PlayerTransform.position + _GroundCheckOffset * PlayerTransform.up, ActorBrain.ActorController.radius,
        //     -PlayerTransform.up, out var hit,
        //     _GroundCheckOffset - ActorBrain.ActorController.radius + 2 * ActorBrain.ActorController.skinWidth);

        #endregion Fields
    }
}