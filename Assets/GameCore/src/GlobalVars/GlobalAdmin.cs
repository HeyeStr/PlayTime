/* 全局管理 By ShuaiGuo
  2025年10月22日
*/

using System.Collections.Generic;
using GameCore.Actor;
using GameCore.MonoBehaviors;
using GameCore.Systems;
using UnityEngine;

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
        }

        private void OnDisable()
        {
            G.GPlayerController.OnDisable();
        }

        private void Update()
        {
            foreach (var system in _Systems)
            {
                system.Update();
            }
        }

        #endregion UnityBehaviour

        #region Fields

        public static PlayerManager Player        => PlayerManager.Instance;
        public static LevelManager  GLevelManager => LevelManager.Instance;

        public List<ShadowLight> ShadowLights = new List<ShadowLight>();

        private List<SystemBase> _Systems = new List<SystemBase>();

        #endregion Fields
    }
}