/* 控制太阳角度变化 By ShuaiGuo
  2025年10月29日
*/

using GameCore.GlobalVars;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.MonoBehaviors
{
    public class SunLightController : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            _Transform      = Pivot.transform;
            _CurrentAngle   = _Transform.eulerAngles;
            _CurrentAngle.x = _Transform.eulerAngles.x > MinAngle ? _Transform.eulerAngles.x : MinAngle;
            G.GameEventManager.AddEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
        }

        private void FixedUpdate()
        {
            _RotationAmount = RotationSpeed * G.DeltaTime;
            if (G.GPlayerController.IsTimeForwardPerformed)
            {
                _TimeForward();
            }

            if (G.GPlayerController.IsTimeBackwardPerformed)
            {
                _TimeBackward();
            }
        }

        private void OnDestroy()
        {
            G.GameEventManager.RemoveEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _TimeForward()
        {
            _CurrentAngle.x += _RotationAmount;
            if (_CurrentAngle.x >= MaxAngle)
            {
                _CurrentAngle.x = MaxAngle;
            }

            _Transform.rotation = Quaternion.Euler(_CurrentAngle);
        }

        private void _TimeBackward()
        {
            _CurrentAngle.x -= _RotationAmount;
            if (_CurrentAngle.x < MinAngle)
            {
                _CurrentAngle.x = MinAngle;
            }

            _Transform.rotation = Quaternion.Euler(_CurrentAngle);
        }

        private void _OnDayNightSwitch()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        #endregion PrivateMethods

        #region Fields

        public Transform Pivot;

        public float RotationSpeed = 30f;
        public float MinAngle      = 0f;
        public float MaxAngle      = 180f;

        private Transform _Transform;
        private Vector3   _CurrentAngle;
        private float     _RotationAmount;

        #endregion Fields
    }
}