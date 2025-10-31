/* 夜晚的灯光 By ShuaiGuo
  2025年10月31日
*/

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