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

        private void Awake()
        {
            if (ActorBrain == null)
            {
                ActorBrain = GetComponent<Brain>();
            }
        }

        private void Update()
        {
            _MockGravity();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position, GroundCHeckSphereRadius);
        }

        #endregion UnityBehavior

        public void Move(Vector3 moveDirection, float moveAmount)
        {
            MoveDirection = moveDirection;
            MoveDirection.Normalize();

            ActorBrain.ActorController.Move(MoveDirection * (MoveSpeed * DeltaTime));
            ActorBrain.AnimatorManager?.UpdateAnimatorMovementParameters(0, moveAmount);
        }

        public void Rotation(Vector3 rotationDirection)
        {
            rotationDirection.Normalize();

            if (rotationDirection == Vector3.zero)
            {
                rotationDirection = transform.forward;
            }

            var newRotation = Quaternion.LookRotation(rotationDirection);

            ActorBrain.ActorController.transform.rotation = Quaternion.Slerp(
                ActorBrain.ActorController.transform.rotation,
                newRotation,
                RotationSpeed * DeltaTime);
        }

        private void _MockGravity()
        {
            _IsGrounded = Physics.CheckSphere(transform.position, GroundCHeckSphereRadius, GroundLayer);
            if (_IsGrounded)
            {
                _YVelocity.y = 0;
                return;
            }

            _YVelocity.y += GravityForce * Time.deltaTime;

            ActorBrain.ActorController.Move(_YVelocity * DeltaTime);
        }

        #region Field

        public Brain ActorBrain;

        public float MoveSpeed               = 4;
        public float RotationSpeed           = 15;
        public float GravityForce            = -10;
        public float GroundCHeckSphereRadius = 0.1f;

        public                   LayerMask GroundLayer;
        [SerializeField] private Vector3   MoveDirection;

        private static float DeltaTime =>
            G.UseUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;

        private Vector3 _YVelocity;
        private bool    _IsGrounded;

        #endregion Field
    }
}