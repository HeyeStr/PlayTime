/* 全局管理 By ShuaiGuo
  2025年10月22日
*/

using System.Collections.Generic;
using GameCore.Actor;
using GameCore.MonoBehaviors;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.GlobalVars
{
    public class GlobalAdmin : MonoBehaviour
    {
        #region Singleton

        public static GlobalAdmin Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion Singleton

        #region UnityBehaviour

        private void OnEnable()
        {
            G.GPlayerController.OnEnable();
            G.GameEventManager.AddEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
            G.GameEventManager.AddEventListener(EventType.LevelPass,      _OnLevelPass);
        }

        private void OnDisable()
        {
            G.GPlayerController.OnDisable();
            G.GameEventManager.RemoveEventListener(EventType.DayNightSwitch, _OnDayNightSwitch);
            G.GameEventManager.RemoveEventListener(EventType.LevelPass,      _OnLevelPass);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _OnDayNightSwitch()
        {
            RenderSettings.skybox = RenderSettings.skybox == DaySkybox ? NightSkybox : DaySkybox;
        }

        private void _OnLevelPass()
        {
        }

        #endregion PrivateMethods

        #region Fields

        public static PlayerManager Player        => PlayerManager.Instance;
        public static LevelManager  GLevelManager => LevelManager.Instance;

        public List<ShadowLight> ShadowLights = new List<ShadowLight>();

        public Material DaySkybox;
        public Material NightSkybox;

        #endregion Fields
    }
}