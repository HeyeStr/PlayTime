/* 死亡区域 By ShuaiGuo
  2025年10月28日
*/

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
            BindArea.Reborn(other.gameObject);
        }

        #endregion UnityBehaviour

        #region Fields

        public Collider   AreaCollider;
        public RebornArea BindArea;

        #endregion Fields
    }
}