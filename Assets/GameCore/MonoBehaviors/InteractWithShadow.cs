/* 赋予可以进入影子的能力 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;
using GameCore.Commands;
using GameCore.GlobalVars;
using GameCore.Utilities.Log;
using UnityEngine;
using UnityEngine.Serialization;
using EventType = GameCore.Enum.EventType;

namespace GameCore.MonoBehaviors
{
    public class InteractWithShadow : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            _IgnoreLayerMask =  ~(1 << LayerMask.NameToLayer("Player"));
            _IgnoreLayerMask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
        }

        private void Update()
        {
            // 进入影子模式后要时刻检测是否还处在影子里
            if (PlayerManager.IsInShadow && !_FloorDetect())
            {
                _TryToExitShadow();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, -transform.up     * FloorMaxDetectDistance);
            Gizmos.DrawRay(transform.position, transform.forward * ForwardMaxDetectDistance);
        }

        #endregion UnityBehaviour

        #region PublicMethods

        public void Interact()
        {
            if (PlayerManager.IsInShadow)
            {
                _TryToExitShadow();
            }
            else
            {
                _TryToEntryShadow();
            }
        }

        #endregion PublicMethods

        #region PrivateMethods

        private void _TryToExitShadow()
        {
            if (_CanExitShadow())
            {
                _ExitShadow();
            }
        }

        private void _ExitShadow()
        {
            PlayerManager.SwitchToNormal();
            NormalBrain.CommandStream.Enqueue(new JumpCommand(ExitSpeed));
            ActorController.height       = PlayerManager.NormalModelHeight;
            ActorController.transform.up = Vector3.up;
        }

        private void _TryToEntryShadow()
        {
            if (_CanEntryShadow())
            {
                _EntryShadow();
            }
            else
            {
                SuperDebug.Log("Can Not EntryShadow");
            }
        }

        private void _EntryShadow()
        {
            PlayerManager.SwitchToShadow();
            ActorController.height = PlayerManager.ShadowModelHeight;
            var preForward = Vector3.Cross(_HitPos.normal, ActorController.transform.right);
            ActorController.transform.rotation = Quaternion.LookRotation(preForward, _HitPos.normal);
        }

        private bool _CanEntryShadow()
        {
            return _FloorDetect() || _WallDetect();
        }

        private bool _CanExitShadow()
        {
            // TODO : 离开影子模式的判断条件
            return true;
        }

        private bool _FloorDetect()
        {
            var hasHitFloor = Physics.Raycast(transform.position, -transform.up, out var hitFloor,
                FloorMaxDetectDistance,
                _IgnoreLayerMask);

            if (!hasHitFloor) return false;

            var actorFeetPos = hitFloor.point;
            _HitPos = hitFloor;

            Debug.DrawLine(transform.position, actorFeetPos, Color.red, 2f);
            Debug.DrawRay(actorFeetPos + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.blue, 2f);
            Debug.DrawRay(actorFeetPos + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.blue, 2f);
            Debug.DrawRay(actorFeetPos + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.blue, 2f);

            foreach (var shadowLight in G.Admin.ShadowLights)
            {
                if (!shadowLight.gameObject.activeSelf) continue;
                var lightPos              = shadowLight.transform.position;
                var lightToFloorDirection = actorFeetPos - lightPos;
                var lightHasHitFloor = Physics.Raycast(lightPos, lightToFloorDirection, out hitFloor,
                    shadowLight.Range, _IgnoreLayerMask);

                Debug.DrawRay(lightPos, lightToFloorDirection, Color.red, 2f);

                if (!lightHasHitFloor) continue;
                Debug.DrawRay(hitFloor.point + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.blue, 2f);
                Debug.DrawRay(hitFloor.point + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.blue, 2f);
                Debug.DrawRay(hitFloor.point + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.blue, 2f);

                // 表明至少有一个光源照到了物体
                if (actorFeetPos == hitFloor.point)
                {
                    return false;
                }
            }

            return true;
        }

        private bool _WallDetect()
        {
            var hasHitForward =
                Physics.Raycast(transform.position, transform.forward, out var hitWall,
                    ForwardMaxDetectDistance, _IgnoreLayerMask);

            if (!hasHitForward) return false;

            var actorForwardPos = hitWall.point;
            _HitPos = hitWall;

            Debug.DrawLine(transform.position, actorForwardPos, Color.yellow, 2f);
            Debug.DrawRay(actorForwardPos + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.green, 2f);
            Debug.DrawRay(actorForwardPos + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.green, 2f);
            Debug.DrawRay(actorForwardPos + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.green, 2f);

            foreach (var shadowLight in G.Admin.ShadowLights)
            {
                if (!shadowLight.gameObject.activeSelf) continue;
                var lightPos                = shadowLight.transform.position;
                var lightToForwardDirection = actorForwardPos - lightPos;

                var lightHasHitForward = Physics.Raycast(lightPos, lightToForwardDirection, out hitWall,
                    shadowLight.Range, _IgnoreLayerMask);

                Debug.DrawRay(lightPos, lightToForwardDirection, Color.yellow, 2f);

                if (!lightHasHitForward) continue;

                Debug.DrawRay(hitWall.point + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.green, 2f);
                Debug.DrawRay(hitWall.point + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.green, 2f);
                Debug.DrawRay(hitWall.point + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.green, 2f);

                // 表明至少有一个光源照到了物体
                if (actorForwardPos == hitWall.point)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion PrivateMethods

        #region Field

        public  PlayerManager       PlayerManager;
        public  CharacterController ActorController;
        public  float               ForwardMaxDetectDistance = 2f;
        public  float               FloorMaxDetectDistance   = 2f;
        public  int                 ExitSpeed                = 10;
        private RaycastHit          _HitPos;
        private LayerMask           _IgnoreLayerMask;
        private Brain               NormalBrain => PlayerManager.NormalBrain;
        private Brain               ShadowBrain => PlayerManager.ShadowBrain;

        #endregion Field
    }
}