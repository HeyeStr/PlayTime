/* 可在任意平面起跳 By ShuaiGuo
  2025年11月3日
*/

using GameCore.Actor;
using GameCore.GlobalVars;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class SurfaceJump : MonoBehaviour
    {
        #region UnityBehaviour

        private void FixedUpdate()
        {
            if (_JumpTimer <= 0f)
            {
                return;
            }

            _JumpVelocity += Drag * G.DeltaTime * _JumpDirection;
            _JumpTimer    -= G.DeltaTime;
            ActorBrain.ActorController.Move(_JumpVelocity * (ActorBrain.Locomotion.MoveSpeed * G.DeltaTime));
        }

        #endregion UnityBehaviour

        #region PublicMethods

        public void Jump(float jumpHeight, Vector3 jumpDirection)
        {
            _JumpDirection                   = jumpDirection.normalized;
            _JumpVelocity                    = Mathf.Sqrt(Mathf.Abs(2 * jumpHeight * Drag)) * _JumpDirection;
            ActorBrain.Locomotion.Velocity.y = _JumpVelocity.y;
            _JumpVelocity.y                  = 0f;
            _JumpDirection.y                 = 0;
            _JumpDirection.Normalize();
            _JumpTimer = Mathf.Sqrt(Mathf.Abs(2 * jumpHeight / Drag));
        }

        #endregion PublicMethods

        #region PrivateMethods

        private bool _CheckGround()
        {
            var checkDistance = ActorBrain.ActorController.height / 2 + 5 * ActorBrain.ActorController.skinWidth;
            return Physics.Raycast(ActorBrain.ActorController.transform.position, Vector3.down, checkDistance);
        }

        #endregion PrivateMethods

        #region Fields

        public Brain ActorBrain;

        public  float   Drag = -10f;
        private Vector3 _JumpVelocity;
        private Vector3 _JumpDirection;
        private float   _JumpTimer;

        #endregion Fields
    }
}