/* 移动物体 By ShuaiGuo
  2025年11月8日
*/

using System;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public enum LoopBehavior
    {
        Loop,
        RoundTrip
    }

    public class AutoItem : MonoBehaviour
    {
        #region UnityBehaviour

        private void Update()
        {
            _HandleMove();
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _GetMoveDirection()
        {
            _MoveDirection = EndPosition.position - transform.position;
            _MoveDirection.Normalize();
        }

        private void _HandleMove()
        {
            _GetMoveDirection();
            transform.position += _MoveDirection * (MoveSpeed * Time.deltaTime);

            var distanceToTarget = Vector3.Distance(transform.position, EndPosition.position);

            if (distanceToTarget < 0.1f)
            {
                _OnReachEndPosition();
            }
        }

        private void _OnReachEndPosition()
        {
            switch (LoopBehavior)
            {
                case LoopBehavior.Loop:
                    transform.position = StartPosition.position;
                    break;
                case LoopBehavior.RoundTrip:
                    (StartPosition, EndPosition) = (EndPosition, StartPosition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion PrivateMethods

        #region Fields

        public Transform    StartPosition;
        public Transform    EndPosition;
        public float        MoveSpeed = 8f;
        public LoopBehavior LoopBehavior;

        private Vector3 _MoveDirection;

        #endregion Fields
    }
}