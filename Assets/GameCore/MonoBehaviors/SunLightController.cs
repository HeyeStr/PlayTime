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
            _Transform = Pivot.transform;
            G.GameEventManager.AddEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
        }

        private void Update()
        {
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
            _RotationAmount =  RotationSpeed * DeltaTime;
            _CurrentAngle   += _RotationAmount;
            if (_CurrentAngle >= MaxAngle)
            {
                _CurrentAngle = MaxAngle;
            }

            _Transform.rotation = Quaternion.Euler(_CurrentAngle, 0, 0);
        }

        private void _TimeBackward()
        {
            _RotationAmount =  RotationSpeed * DeltaTime;
            _CurrentAngle   -= _RotationAmount;
            if (_CurrentAngle < MinAngle)
            {
                _CurrentAngle = MinAngle;
            }

            _Transform.rotation = Quaternion.Euler(_CurrentAngle, 0, 0);
        }

        private void _OnDayNightSwitch()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            _CurrentAngle       = MinAngle;
            _Transform.rotation = Quaternion.Euler(MinAngle, 0, 0);
        }

        #endregion PrivateMethods

        #region Fields

        public Transform Pivot;
        public float     RotationSpeed = 30f;
        public float     MinAngle      = 0f;
        public float     MaxAngle      = 180f;

        private Transform _Transform;
        private float     _CurrentAngle;
        private float     _RotationAmount;

        private static float DeltaTime =>
            G.UseUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;

        #endregion Fields
    }
}