/* 角色仓库 By ShuaiGuo
  2025年10月31日
*/

using System;
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

        public void PickUp(GameObject item)
        {
            // TODO: 拾取物品的逻辑
            SuperDebug.Log("Picked up " + item.name);
        }

        #endregion PublicMethods

        #region Fields

        #endregion Fields
    }
}