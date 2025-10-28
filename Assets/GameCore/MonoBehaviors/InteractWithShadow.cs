/* 赋予可以进入影子的能力 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;
using GameCore.Commands;
using GameCore.GlobalVars;
using GameCore.Utilities.Log;
using UnityEngine;
using UnityEngine.Serialization;

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
            if (IsInShadow && !_FloorDetect())
            {
                IsInShadow = false;
                _TryToExitShadow();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.down * 10);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * MaxDetectDistance);
        }

        #endregion UnityBehaviour

        #region PublicMethods

        public void Interact()
        {
            if (IsInShadow)
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
            // TODO : 改成获得垂直于平面的速度
            ActorBrain.CommandQueue.Enqueue(new JumpCommand(ExitSpeed));
            ActorBrain.ModelManager.SwitchModel();
            IsInShadow = false;
        }

        private void _TryToEntryShadow()
        {
            if (_CanEntryShadow())
            {
                IsInShadow = true;
                _EntryShadow();
            }
            else
            {
                SuperDebug.Log("Can Not EntryShadow");
            }
        }

        private bool _CanEntryShadow()
        {
            return _FloorDetect() || _WallDetect();
        }

        private void _EntryShadow()
        {
            // TODO : 切换模型等工作
            ActorBrain.ModelManager.SwitchModel();
            ActorBrain.ActorController.Move(_HitPos - ActorBrain.transform.position);
        }

        private bool _FloorDetect()
        {
            var hasHitFloor = Physics.Raycast(transform.position, -transform.up, out var hitFloor,
                float.PositiveInfinity,
                _IgnoreLayerMask);

            if (!hasHitFloor) return false;


            var actorFeetPos = hitFloor.point;

            Debug.DrawLine(transform.position, actorFeetPos, Color.red, 2f);
            Debug.DrawRay(actorFeetPos + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.red, 2f);
            Debug.DrawRay(actorFeetPos + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.red, 2f);
            Debug.DrawRay(actorFeetPos + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.red, 2f);

            foreach (var shadowLight in G.Admin.ShadowLights)
            {
                var lightPos              = shadowLight.transform.position;
                var lightToFloorDirection = actorFeetPos - lightPos;
                var lightHasHitFloor = Physics.Raycast(lightPos, lightToFloorDirection, out hitFloor,
                    float.PositiveInfinity,
                    _IgnoreLayerMask);
                Debug.DrawRay(lightPos, lightToFloorDirection, Color.red, 2f);
                if (!lightHasHitFloor) continue;
                Debug.DrawRay(hitFloor.point + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.red, 2f);
                Debug.DrawRay(hitFloor.point + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.red, 2f);
                Debug.DrawRay(hitFloor.point + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.red, 2f);

                // 表明被物体挡住了，说明 hitPos 处有影子
                if (actorFeetPos != hitFloor.point)
                {
                    _HitPos = actorFeetPos;
                    return true;
                }
            }

            return false;
        }

        private bool _WallDetect()
        {
            var hasHitForward =
                Physics.Raycast(transform.position, transform.forward, out var hitWall,
                    MaxDetectDistance, _IgnoreLayerMask);

            if (!hasHitForward) return false;
            var temp = hitWall.collider.gameObject.layer & _IgnoreLayerMask;


            var actorForwardPos = hitWall.point;
            Debug.DrawLine(transform.position, actorForwardPos, Color.yellow, 2f);
            Debug.DrawRay(actorForwardPos + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.yellow, 2f);
            Debug.DrawRay(actorForwardPos + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.yellow, 2f);
            Debug.DrawRay(actorForwardPos + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.yellow, 2f);
            foreach (var shadowLight in G.Admin.ShadowLights)
            {
                var lightPos                = shadowLight.transform.position;
                var lightToForwardDirection = actorForwardPos - lightPos;

                var lightHasHitForward = Physics.Raycast(lightPos, lightToForwardDirection, out hitWall,
                    float.PositiveInfinity,
                    _IgnoreLayerMask);

                Debug.DrawRay(lightPos, lightToForwardDirection, Color.yellow, 2f);
                if (!lightHasHitForward) continue;
                Debug.DrawRay(hitWall.point + Vector3.up      * 0.1f, Vector3.down * 0.2f, Color.yellow, 2f);
                Debug.DrawRay(hitWall.point + Vector3.right   * 0.1f, Vector3.left * 0.2f, Color.yellow, 2f);
                Debug.DrawRay(hitWall.point + Vector3.forward * 0.1f, Vector3.back * 0.2f, Color.yellow, 2f);
                // 表明被物体挡住了，说明 hitPos 处有影子
                if (actorForwardPos != hitWall.point)
                {
                    _HitPos = actorForwardPos;
                    return true;
                }
            }

            return false;
        }

        #endregion PrivateMethods

        #region Field

        public  Brain     ActorBrain;
        public  float     MaxDetectDistance = 2f;
        public  float     ExitSpeed         = 10f;
        public  bool      IsInShadow        = false;
        private Vector3   _HitPos;
        private LayerMask _IgnoreLayerMask;

        #endregion Field
    }
}