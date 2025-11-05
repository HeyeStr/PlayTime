/* 角色仓库 By ShuaiGuo
  2025年10月31日
*/

using System.Collections.Generic;
using GameCore.GlobalVars;
using GameCore.Utilities.Log;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.MonoBehaviors
{
    public class Inventory : MonoBehaviour
    {
        #region UnityBehaviour

        #endregion UnityBehaviour

        #region PublicMethods

        public void PickUp(Collectable item)
        {
            CollectedItems.Add(item);
            G.GameEventManager.TriggerEvent(EventType.ItemCollect);
        }

        #endregion PublicMethods

        #region Fields

        public List<Collectable> CollectedItems = new List<Collectable>();

        #endregion Fields
    }
}