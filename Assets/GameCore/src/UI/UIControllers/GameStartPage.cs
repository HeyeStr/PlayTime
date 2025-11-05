/* 游戏开始界面 By ShuaiGuo
  2025年11月3日
*/

using GameCore.GlobalVars;
using UnityEngine.UI;

namespace GameCore.UI.UIControllers
{
    public class GameStartPage : UIControllerBase
    {
        #region UnityBehaviour

        private void OnEnable()
        {
            G.GPlayerController.OnDisable();
            StartGame.onClick.AddListener(_OnStartGame);
            ContinueGame.onClick.AddListener(_OnContinueGame);
            QuitGame.onClick.AddListener(_OnQuitGame);
        }

        private void OnDisable()
        {
            StartGame.onClick.RemoveListener(_OnStartGame);
            ContinueGame.onClick.RemoveListener(_OnContinueGame);
            QuitGame.onClick.RemoveListener(_OnQuitGame);
            G.GPlayerController.OnEnable();
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _OnStartGame()
        {
            gameObject.SetActive(false);
        }

        private void _OnContinueGame()
        {
            // TODO: 存档逻辑
        }

        private void _OnQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion PrivateMethods

        #region Fields

        public Button StartGame;
        public Button ContinueGame;
        public Button QuitGame;

        #endregion Fields
    }
}