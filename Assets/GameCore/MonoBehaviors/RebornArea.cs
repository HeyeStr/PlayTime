/* 重生区域 By ShuaiGuo
  2025年10月28日
*/

using GameCore.Actor;
using GameCore.Commands;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class RebornArea : MonoBehaviour
    {
        #region PublicMethods

        public void Reborn(GameObject rebornGameObject)
        {
            var actorBrain = rebornGameObject.transform.root.GetComponent<Brain>();
            if (actorBrain == null) return;

            actorBrain.CommandQueue.Enqueue(new TeleProtCommand(transform.position));
        }

        #endregion PublicMethods
    }
}