/* 主相机视角 By ShuaiGuo
  2025年10月23日
*/

using System;
using UnityEngine;

namespace GameCore.GlobalVars
{
    public class MainCamera : MonoBehaviour
    {
        #region Singleton

        public static MainCamera Instance;

        protected void Awake()
        {
            //这样写单例确保进入新场景时被新场景的 MainCamera 接管
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        #endregion Singleton
    }
}