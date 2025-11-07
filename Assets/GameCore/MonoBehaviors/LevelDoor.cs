/* 关卡选择门 By ShuaiGuo
  2025年11月5日
*/

using System;
using GameCore.Actor;
using GameCore.GlobalVars;
using GameCore.UI.UIControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.MonoBehaviors
{
    public class LevelDoor : MonoBehaviour
    {
        #region UnityBehaviour

        private void OnTriggerEnter(Collider other)
        {
            var actorBrain = other.transform.root.GetComponentInChildren<Brain>();
            if (actorBrain == null || LevelSceneName == "Default") return;

            if (LevelSceneName == "Original")
            {
                _BackToGameStart();
            }
            else
            {
                _EntryNewScene();
            }
        }

        #endregion UnityBehaviour

        #region PublicMethods

        public void Open()
        {
            Road.SetActive(true);
        }

        public void Close()
        {
            Road.SetActive(false);
        }

        #endregion PublicMethods

        #region PrivateMethods

        private void _EntryNewScene()
        {
            SceneManager.LoadScene(LevelSceneName);
        }

        private static void _BackToGameStart()
        {
            var uiManager = G.GUIManager;
            var page      = UIManager.GetUIController("GameStartPage", uiManager.Page);
            if (page is not GameStartPage) return;

            GameStartPage.IsFirstTime = true;
            uiManager.LoadPage("GameStartPage");
        }

        #endregion PrivateMethods

        #region Fields

        public bool IsEnable => Road.activeSelf;

        public GameObject Road;
        public string     LevelSceneName = "Default";

        #endregion Fields
    }
}