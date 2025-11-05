/* 关卡管理 By ShuaiGuo
  2025年10月31日
*/

using System.Collections.Generic;
using GameCore.MonoBehaviors;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.GlobalVars
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton

        public static LevelManager Instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(Instance.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            G.GameEventManager.AddEventListener(EventType.ItemCollect, _OnItemCollected);
        }

        private void OnDisable()
        {
            G.GameEventManager.RemoveEventListener(EventType.ItemCollect, _OnItemCollected);
        }

        #endregion Singleton


        #region PrivateMethods

        private void _OnItemCollected()
        {
            if (CollectedNumber == NeededNumber)
            {
                G.GameEventManager.TriggerEvent(EventType.LevelPass);
            }
        }

        #endregion PrivateMethods

        #region Fields

        public static int               CollectedNumber => GlobalAdmin.Player.PlayerInventory.CollectedItems.Count;
        public        int               NeededNumber    => NeededItems.Count;
        public        List<Collectable> NeededItems = new List<Collectable>();

        #endregion Fields
    }
}