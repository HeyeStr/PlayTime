/* 暂停界面控制 By ShuaiGuo
  2025年11月7日
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCore.UI.UIControllers
{
    public class PausePage : UIControllerBase
    {
        #region UnityBehaviour

        private void OnEnable()
        {
            ContinueGame.onClick.AddListener(_OnContinueGame);
            ReturnToLevelSelection.onClick.AddListener(_OnReturnToLevelSelection);
            QuitGame.onClick.AddListener(_OnQuitGame);
        }

        private void OnDisable()
        {
            ContinueGame.onClick.RemoveListener(_OnContinueGame);
            ReturnToLevelSelection.onClick.RemoveListener(_OnReturnToLevelSelection);
            QuitGame.onClick.RemoveListener(_OnQuitGame);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _OnContinueGame()
        {
            _RestoreGame();
        }


        private void _OnReturnToLevelSelection()
        {
            _RestoreGame();
            SceneManager.LoadScene("LvSelection1");
        }

        private static void _OnQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void _RestoreGame()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        #endregion PrivateMethods

        #region Fields

        public Button ContinueGame;
        public Button ReturnToLevelSelection;
        public Button QuitGame;

        #endregion Fields
    }
}