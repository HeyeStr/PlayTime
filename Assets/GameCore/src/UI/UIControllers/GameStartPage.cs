/* 游戏开始界面 By ShuaiGuo
  2025年11月3日
*/

using GameCore.GlobalVars;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.UIControllers
{
    public class GameStartPage : UIControllerBase
    {
        #region UnityBehaviour

        private void OnEnable()
        {
            if (IsFirstTime)
            {
                G.GPlayerController.OnDisable();
                UICamera.SetActive(true);
                StartGame.onClick.AddListener(_OnStartGame);
                QuitGame.onClick.AddListener(_OnQuitGame);
                IsFirstTime = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            StartGame.onClick.RemoveListener(_OnStartGame);
            QuitGame.onClick.RemoveListener(_OnQuitGame);
            UICamera.SetActive(false);
            G.GPlayerController.OnEnable();
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _OnStartGame()
        {
            gameObject.SetActive(false);
        }

        private static void _OnQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion PrivateMethods

        #region Fields

        public static bool IsFirstTime = true;

        public GameObject UICamera;
        public Button     StartGame;
        public Button     QuitGame;

        #endregion Fields
    }
}