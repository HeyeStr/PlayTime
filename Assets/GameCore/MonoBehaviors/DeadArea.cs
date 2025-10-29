/* 死亡区域 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class DeadArea : MonoBehaviour
    {
        #region UnityBehaviour

        private void Awake()
        {
            if (AreaCollider == null)
            {
                AreaCollider = GetComponent<BoxCollider>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var actorBrain = other.transform.root.GetComponent<Brain>();
            if (actorBrain == null) return;

            if (HarmlessForShadowPlayer && actorBrain.ModelManager.IsInShadow)
            {
                return;
            }

            BindArea.Reborn(actorBrain);
        }

        #endregion UnityBehaviour

        #region Fields

        public Collider   AreaCollider;
        public RebornArea BindArea;
        public bool       HarmlessForShadowPlayer = true;

        #endregion Fields
    }
}