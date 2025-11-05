/* 游戏状态AI By ShuaiGuo
  2025年11月3日
*/

using GameCore.GlobalVars;
using GameCore.MonoBehaviors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EventType = GameCore.Enum.EventType;

namespace GameCore.UI.UIControllers
{
    public class GameState : UIControllerBase
    {
        #region UnityBehaviour

        private void Start()
        {
            _SunLightController     = SunPivot.GetComponentInChildren<SunLightController>();
            _RectTransform          = SunImage.rectTransform;
            _CurrentAngle.z         = SunPivot.transform.eulerAngles.x - 90;
            _RectTransform.rotation = Quaternion.Euler(_CurrentAngle);
        }

        private void FixedUpdate()
        {
            _UpdateCollectable();

            _TimeChange();
        }


        private void OnEnable()
        {
            G.GameEventManager.AddEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
        }

        private void OnDisable()
        {
            G.GameEventManager.RemoveEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _UpdateCollectable()
        {
            CollectedNumber.text = LevelManager.CollectedNumber.ToString();
            NeededNumber.text    = LevelManager.NeededNumber.ToString();
        }

        private void _TimeChange()
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

        private void _TimeForward()
        {
            _CurrentAngle.z += _RotationAmount;
            if (_CurrentAngle.z >= MaxAngle)
            {
                _CurrentAngle.z = MaxAngle;
            }

            _RectTransform.rotation = Quaternion.Euler(_CurrentAngle);
        }

        private void _TimeBackward()
        {
            _CurrentAngle.z -= _RotationAmount;
            if (_CurrentAngle.z < MinAngle)
            {
                _CurrentAngle.z = MinAngle;
            }

            _RectTransform.rotation = Quaternion.Euler(_CurrentAngle);
        }

        private void _OnDayNightSwitch()
        {
            SunImage.enabled = !SunImage.enabled;
        }

        #endregion PrivateMethods

        #region Fields

        public Transform SunPivot;
        public Image     SunImage;


        public TextMeshProUGUI CollectedNumber;
        public TextMeshProUGUI NeededNumber;

        private RectTransform      _RectTransform;
        private SunLightController _SunLightController;
        private Vector3            _CurrentAngle;
        private float              _RotationAmount;

        private static LevelManager LevelManager  => GlobalAdmin.GLevelManager;
        private        float        MinAngle      => _SunLightController.MinAngle - 90;
        private        float        MaxAngle      => _SunLightController.MaxAngle - 90;
        private        float        RotationSpeed => _SunLightController.RotationSpeed;

        #endregion Fields
    }
}