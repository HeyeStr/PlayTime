/* 管理 Player的 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Commands;
using GameCore.GlobalVars;
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
            _NormalModel.SetActive(true);
            _ShadowModel.SetActive(false);
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
            CurrentBrain.Locomotion.Velocity.x = _MoveDirection.x;
            CurrentBrain.Locomotion.Velocity.z = _MoveDirection.z;
        }

        private void _GetMoveDirection()
        {
            _MoveDirection =  PlayerController.VerticalInput   * Forward;
            _MoveDirection += PlayerController.HorizontalInput * Right;
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
        private static Vector3          Forward           => G.Camera.transform.forward;
        private static Vector3          Right             => G.Camera.transform.right;
        public         float            NormalModelHeight => _NormalCollider.height;
        public         float            ShadowModelHeight => _ShadowCollider.radius * 2;

        #endregion Property

        #region Fields

        public static PlayerManager Instance;
        public        Brain         NormalBrain;
        public        Brain         ShadowBrain;

        private GameObject      _NormalModel;
        private GameObject      _ShadowModel;
        private CapsuleCollider _NormalCollider;
        private SphereCollider  _ShadowCollider;
        private Vector3         _MoveDirection;

        #endregion Fields
    }
}