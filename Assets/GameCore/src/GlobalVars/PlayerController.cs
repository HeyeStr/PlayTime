/* 玩家的控制器 By ShuaiGuo
  2025年10月23日
*/

using System.Collections.Generic;
using GameCore.Commands;
using UnityEngine;
using UnityEngine.InputSystem;

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
            InputController = new InputControl();
        }

        #region UnityBehavior

        public void Update()
        {
            if (!(_MoveAmount > 0)) return;

            _MoveCommand.Reset(new Vector3(HorizontalInput, 0, VerticalInput), _MoveAmount);
            CommandStream.Enqueue(_MoveCommand);
        }

        public void OnEnable()
        {
            InputController.Enable();
            InputController.GamePlay.Move.performed     += _OnMovePerformed;
            InputController.GamePlay.Move.canceled      += _OnMoveCanceled;
            InputController.GamePlay.Look.performed     += _OnLookPerformed;
            InputController.GamePlay.Interact.performed += _OnInteractPerformed;
            InputController.UI.Cancel.performed         += _OnCancelPerformed;
        }

        public void OnDisable()
        {
            InputController.GamePlay.Move.performed     -= _OnMovePerformed;
            InputController.GamePlay.Move.canceled      -= _OnMoveCanceled;
            InputController.GamePlay.Look.performed     -= _OnLookPerformed;
            InputController.GamePlay.Interact.performed -= _OnInteractPerformed;
            InputController.UI.Cancel.performed         -= _OnCancelPerformed;
            InputController.Disable();
        }

        #endregion UnityBehavior

        #region HandleMoveAndRoation

        private void _OnMovePerformed(InputAction.CallbackContext context)
        {
            _MovementInput = context.ReadValue<Vector2>();
            _MoveAmount    = Mathf.Clamp01(Mathf.Abs(VerticalInput) + Mathf.Abs(HorizontalInput));
            _MoveAmount = _MoveAmount switch
            {
                > 0 and <= 0.5f => 0.5f,
                > 0.5f and < 1  => 1f,
                _               => _MoveAmount
            };

            _CheckInputDeviceChange(context);
        }

        private void _OnMoveCanceled(InputAction.CallbackContext context)
        {
            _MovementInput = Vector2.zero;
            _MoveAmount    = 0;

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
            // if (PlayerBrain == null) return;
            // PlayerBrain.AttemptToPerformInteract();
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
            if (!G.GUIManager.UIControllers.ContainsKey("PausePage")) return;
            var pausePage = G.GUIManager.UIControllers["PausePage"].gameObject;
            pausePage.SetActive(!pausePage.activeSelf);
            _CheckInputDeviceChange(context);
        }

        private void _OnCancelPerformed(InputAction.CallbackContext context)
        {
            if (!G.GUIManager.UIControllers.ContainsKey("PausePage")) return;
            var pausePage = G.GUIManager.UIControllers["PausePage"].gameObject;
            pausePage.SetActive(false);
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

        // Move Input
        private float VerticalInput   => _MovementInput.y;
        private float HorizontalInput => _MovementInput.x;

        // CameraMove Input
        public float CameraVerticalInput   => _CameraInput.y;
        public float CameraHorizontalInput => _CameraInput.x;

        #endregion Property

        #region Field

        public readonly InputControl       InputController;
        public          Queue<CommandBase> CommandStream = new Queue<CommandBase>();

        private          float       _MoveAmount;
        private          Vector2     _MovementInput;
        private readonly MoveCommand _MoveCommand = new MoveCommand();
        private          Vector2     _CameraInput;

        #endregion Field
    }
}