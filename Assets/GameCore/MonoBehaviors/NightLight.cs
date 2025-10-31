/* 夜晚的灯光 By ShuaiGuo
  2025年10月31日
*/

using System;
using GameCore.GlobalVars;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.MonoBehaviors
{
    public class NightLight : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            _GameObject = gameObject;
            G.GameEventManager.AddEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
            _GameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            G.GameEventManager.RemoveEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _OnDayNightSwitch()
        {
            _GameObject.SetActive(!_GameObject.activeSelf);
        }

        #endregion PrivateMethods


        #region Fields

        private GameObject _GameObject;

        #endregion Fields
    }
}