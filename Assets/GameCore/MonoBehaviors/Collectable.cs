/* 可被主角收集物品 By ShuaiGuo
  2025年10月31日
*/

using GameCore.Actor;
using GameCore.GlobalVars;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.MonoBehaviors
{
    public class Collectable : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            _GameObject = gameObject;
            GlobalAdmin.GLevelManager.NeededItems.Add(this);
            G.GameEventManager.AddEventListener(EventType.LevelRestart, _OnLevelRestart);
        }

        private void OnTriggerEnter(Collider other)
        {
            var playerInventory = other.transform.root.GetComponent<Inventory>();
            if (playerInventory == null) return;

            playerInventory.PickUp(this);
            _GameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            G.GameEventManager.RemoveEventListener(EventType.LevelRestart, _OnLevelRestart);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _OnLevelRestart()
        {
            _GameObject.SetActive(true);
        }

        #endregion PrivateMethods

        #region Fields

        private GameObject _GameObject;

        #endregion Fields
    }
}