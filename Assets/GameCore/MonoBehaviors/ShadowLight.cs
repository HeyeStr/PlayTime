/* 造影灯 By ShuaiGuo
  2025年10月28日
*/

using GameCore.GlobalVars;
using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class ShadowLight : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            G.Admin.ShadowLights.Add(this);
        }

        #endregion UnityBehaviour
    }
}