/* 跨平面移动能力 By ShuaiGuo
  2025年11月2日
*/

using System;
using GameCore.GlobalVars;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class CrossPlane : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            _IgnoreLayerMask =  ~(1 << LayerMask.NameToLayer("Player"));
            _IgnoreLayerMask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
        }

        private void Update()
        {
            if (!GlobalAdmin.Player.IsInShadow || !_WallDetect()) return;

            var preForward = Vector3.Cross(_HitPos.normal, GlobalAdmin.Player.transform.right);
            GlobalAdmin.Player.transform.rotation = Quaternion.LookRotation(preForward, _HitPos.normal);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, DetectDistance * transform.forward);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private bool _WallDetect()
        {
            var hasHitForward =
                Physics.Raycast(transform.position, transform.forward, out var hitWall,
                    DetectDistance, _IgnoreLayerMask);

            if (!hasHitForward) return false;

            var actorForwardPos = hitWall.point;
            _HitPos = hitWall;

            Debug.DrawLine(transform.position, actorForwardPos, Color.blue, 2f);

            foreach (var shadowLight in G.Admin.ShadowLights)
            {
                if (!shadowLight.gameObject.activeSelf) continue;
                var lightPos                = shadowLight.transform.position;
                var lightToForwardDirection = actorForwardPos - lightPos;

                var lightHasHitForward = Physics.Raycast(lightPos, lightToForwardDirection, out hitWall,
                    shadowLight.Range, _IgnoreLayerMask);


                if (!lightHasHitForward) continue;

                // 表明至少有一个光源照到了物体
                if (actorForwardPos == hitWall.point)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion PrivateMethods

        #region Fields

        public float DetectDistance = 1f;

        private RaycastHit _HitPos;
        private LayerMask  _IgnoreLayerMask;

        #endregion Fields
    }
}