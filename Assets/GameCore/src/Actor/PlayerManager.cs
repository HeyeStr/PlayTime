/* 管理 Player的 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Commands;
using GameCore.GlobalVars;
using GameCore.MonoBehaviors;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.Actor
{
    public class PlayerManager : ActorManagerBase
    {
        #region UnityBehaviour

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        private void Start()
        {
            _NormalModel    = NormalBrain.gameObject;
            _ShadowModel    = ShadowBrain.gameObject;
            _NormalCollider = NormalBrain.GetComponent<CapsuleCollider>();
            _ShadowCollider = ShadowBrain.GetComponent<SphereCollider>();
            SwitchToNormal();
        }

        private void Update()
        {
            _HandleMove();
        }

        private void OnEnable()
        {
            G.GameEventManager.AddEventListener(EventType.Interact, _OnInteract);
        }

        private void OnDisable()
        {
            G.GameEventManager.RemoveEventListener(EventType.Interact, _OnInteract);
        }

        #endregion UnityBehaviour

        #region PublicMethods

        public void SwitchToNormal()
        {
            _NormalModel.SetActive(true);
            _ShadowModel.SetActive(false);
        }

        public void SwitchToShadow()
        {
            _NormalModel.SetActive(false);
            _ShadowModel.SetActive(true);
        }

        #endregion PublicMethods

        #region PrivateMethods

        private void _HandleMove()
        {
            _GetMoveDirection();
            if (!CurrentBrain.Locomotion) return;

            // // 保留法线分量
            // var normalVelocity = Vector3.Dot(CurrentBrain.Locomotion.Velocity, transform.up) * transform.up;
            // CurrentBrain.Locomotion.Velocity =  _MoveDirection;
            // CurrentBrain.Locomotion.Velocity += normalVelocity;

            if (IsInShadow)
            {
                CurrentBrain.Locomotion.Velocity = _MoveDirection;
            }
            else
            {
                CurrentBrain.Locomotion.Velocity.x = _MoveDirection.x;
                CurrentBrain.Locomotion.Velocity.z = _MoveDirection.z;
            }
        }

        private void _GetMoveDirection()
        {
            _MoveDirection   =  PlayerController.VerticalInput   * CameraForward;
            _MoveDirection   += PlayerController.HorizontalInput * CameraRight;
            _MoveDirection.y =  0;

            _MoveDirection = Quaternion.FromToRotation(Vector3.up, transform.up) * _MoveDirection;
        }

        private void _OnInteract()
        {
            CurrentBrain.CommandStream.Enqueue(new InteractWithShadowCommand());
        }

        #endregion PrivateMethods

        #region Property

        public         bool             IsInShadow        => _ShadowModel.activeSelf;
        public         Brain            CurrentBrain      => IsInShadow ? ShadowBrain : NormalBrain;
        private static PlayerController PlayerController  => G.GPlayerController;
        private static Vector3          CameraForward     => G.Camera.transform.forward;
        private static Vector3          CameraRight       => G.Camera.transform.right;
        public         float            NormalModelHeight => _NormalCollider.height;
        public         float            ShadowModelHeight => _ShadowCollider.radius * 2;

        #endregion Property

        #region Fields

        public static PlayerManager Instance;
        public        Brain         NormalBrain;
        public        Brain         ShadowBrain;
        public        Inventory     PlayerInventory;

        private GameObject      _NormalModel;
        private GameObject      _ShadowModel;
        private CapsuleCollider _NormalCollider;
        private SphereCollider  _ShadowCollider;
        private Vector3         _MoveDirection;

        #endregion Fields
    }
}