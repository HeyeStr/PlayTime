/* 玩家的控制器 By ShuaiGuo
  2025年10月23日
*/

using UnityEngine;
using UnityEngine.InputSystem;
using EventType = GameCore.Enum.EventType;

namespace GameCore.GlobalVars
{
    public class PlayerController
    {
        #region Singleton

        private static PlayerController _Instance;

        public static PlayerController Instance
        {
            get { return _Instance ??= new PlayerController(); }
        }

        #endregion Singleton

        private PlayerController()
        {
            _InputController = new InputControl();
        }

        #region UnityBehavior

        public void OnEnable()
        {
            _InputController.Enable();
            _InputController.GamePlay.Move.performed           += _OnMovePerformed;
            _InputController.GamePlay.Move.canceled            += _OnMoveCanceled;
            _InputController.GamePlay.Look.performed           += _OnLookPerformed;
            _InputController.GamePlay.TimeForward.started      += _OnTimeForwardStarted;
            _InputController.GamePlay.TimeForward.canceled     += _OnTimeForwardCanceled;
            _InputController.GamePlay.TimeBackward.started     += _OnTimeBackwardStarted;
            _InputController.GamePlay.TimeBackward.canceled    += _OnTimeBackwardCanceled;
            _InputController.GamePlay.Interact.performed       += _OnInteractPerformed;
            _InputController.GamePlay.DayNightSwitch.performed += _OnDayNightSwitch;
            _InputController.UI.Cancel.performed               += _OnCancelPerformed;
            IsEnable                                           =  true;
        }

        public void OnDisable()
        {
            _InputController.GamePlay.Move.performed           -= _OnMovePerformed;
            _InputController.GamePlay.Move.canceled            -= _OnMoveCanceled;
            _InputController.GamePlay.Look.performed           -= _OnLookPerformed;
            _InputController.GamePlay.TimeForward.started      -= _OnTimeForwardStarted;
            _InputController.GamePlay.TimeForward.canceled     -= _OnTimeForwardCanceled;
            _InputController.GamePlay.TimeBackward.started     -= _OnTimeBackwardStarted;
            _InputController.GamePlay.TimeBackward.canceled    -= _OnTimeBackwardCanceled;
            _InputController.GamePlay.Interact.performed       -= _OnInteractPerformed;
            _InputController.GamePlay.DayNightSwitch.performed -= _OnDayNightSwitch;
            _InputController.UI.Cancel.performed               -= _OnCancelPerformed;
            _InputController.Disable();
            IsEnable = false;
        }

        #endregion UnityBehavior

        #region HandleMoveAndRoation

        private void _OnMovePerformed(InputAction.CallbackContext context)
        {
            _MovementInput = context.ReadValue<Vector2>();
            MoveAmount     = Mathf.Clamp01(Mathf.Abs(VerticalInput) + Mathf.Abs(HorizontalInput));
            MoveAmount = MoveAmount switch
            {
                > 0 and <= 0.5f => 0.5f,
                > 0.5f and < 1  => 1f,
                _               => MoveAmount
            };

            _CheckInputDeviceChange(context);
        }

        private void _OnMoveCanceled(InputAction.CallbackContext context)
        {
            _MovementInput = Vector2.zero;
            MoveAmount     = 0;

            _CheckInputDeviceChange(context);
        }

        #endregion HandleMoveAndRoation

        #region HandleLookAround

        private void _OnLookPerformed(InputAction.CallbackContext context)
        {
            _CameraInput = context.ReadValue<Vector2>();
            _CheckInputDeviceChange(context);
        }

        #endregion HandleLookAround

        #region HandleCapability

        private void _OnInteractPerformed(InputAction.CallbackContext context)
        {
            G.GameEventManager.TriggerEvent(EventType.Interact);
            _CheckInputDeviceChange(context);
        }

        private void _OnDayNightSwitch(InputAction.CallbackContext context)
        {
            G.GameEventManager.TriggerEvent(EventType.DayNightSwitch);
            _CheckInputDeviceChange(context);
        }

        private void _OnTimeForwardStarted(InputAction.CallbackContext context)
        {
            IsTimeForwardPerformed = true;
            _CheckInputDeviceChange(context);
        }

        private void _OnTimeForwardCanceled(InputAction.CallbackContext context)
        {
            IsTimeForwardPerformed = false;
            _CheckInputDeviceChange(context);
        }

        private void _OnTimeBackwardStarted(InputAction.CallbackContext context)
        {
            IsTimeBackwardPerformed = true;
            _CheckInputDeviceChange(context);
        }

        private void _OnTimeBackwardCanceled(InputAction.CallbackContext context)
        {
            IsTimeBackwardPerformed = false;
            _CheckInputDeviceChange(context);
        }

        #endregion HandleCapability

        #region HandleAbility

        private void _OnActiveAbilityPerformed(InputAction.CallbackContext context)
        {
            // if (PlayerBrain == null) return;
            // PlayerBrain.ActorAbilityManager.Execute();
            _CheckInputDeviceChange(context);
        }

        #endregion HandleAbility

        #region HanldeUI

        public void OnPausePerformed(InputAction.CallbackContext context)
        {
            // TODO: 暂停逻辑
            _CheckInputDeviceChange(context);
        }

        private void _OnCancelPerformed(InputAction.CallbackContext context)
        {
            // TODO: 暂停逻辑
            _CheckInputDeviceChange(context);
        }

        #endregion HanldeUI

        private void _CheckInputDeviceChange(InputAction.CallbackContext context)
        {
            if (CurrentControllerDevice == context.control.device) return;

            CurrentControllerDevice = context.control.device;
        }

        #region Property

        private InputDevice CurrentControllerDevice { get; set; }

        public float VerticalInput   => _MovementInput.y;
        public float HorizontalInput => _MovementInput.x;

        // CameraMove Input
        public float CameraVerticalInput   => _CameraInput.y;
        public float CameraHorizontalInput => _CameraInput.x;

        #endregion Property

        #region Fields

        private readonly InputControl _InputController;

        public float MoveAmount;
        public bool  IsEnable                = false;
        public bool  IsTimeForwardPerformed  = false;
        public bool  IsTimeBackwardPerformed = false;

        private Vector2 _MovementInput;
        private Vector2 _CameraInput;

        #endregion Fields
    }
}