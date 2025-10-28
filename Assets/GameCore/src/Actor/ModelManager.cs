/* 管理Actor的模型 By ShuaiGuo
  2025年10月28日
*/

using System;
using UnityEngine;

namespace GameCore.Actor
{
    public class ModelManager : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            NormalModel.SetActive(true);
            ShadowModel.SetActive(false);
        }

        #endregion UnityBehaviour

        #region PublicMethods

        public void SwitchModel()
        {
            NormalModel.SetActive(!NormalModel.activeSelf);
            ShadowModel.SetActive(!ShadowModel.activeSelf);
        }

        #endregion PublicMethods

        #region Fields

        public GameObject NormalModel;
        public GameObject ShadowModel;

        #endregion Fields
    }
}