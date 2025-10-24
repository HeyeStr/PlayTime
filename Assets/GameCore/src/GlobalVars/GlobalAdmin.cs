/* 全局管理 By ShuaiGuo
  2025年10月22日
*/

using System.Collections.Generic;
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

        private void OnEnable()
        {
            G.GPlayerController.OnEnable();
        }

        private void OnDisable()
        {
            G.GPlayerController.OnDisable();
        }

        #endregion Singleton

        private void Update()
        {
            G.GPlayerController.Update();
            foreach (var system in _Systems)
            {
                system.Update();
            }
        }

        #region Field

        private List<SystemBase> _Systems = new List<SystemBase>();

        #endregion Field
    }
}