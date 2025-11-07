/* UI管理 By ShuaiGuo
  2025年10月23日
*/


using System.Collections.Generic;
using GameCore.UI.UIControllers;
using GameCore.Utilities.Log;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.GlobalVars
{
    public class UIManager : MonoBehaviour
    {
        #region UnityBehavior

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
            G.GameEventManager.AddEventListener(EventType.GameOver, _OnGameOver);
        }

        private void OnDisable()
        {
            G.GameEventManager.RemoveEventListener(EventType.GameOver, _OnGameOver);
        }

        #endregion UnityBehavior

        #region PublicMethod

        public void LoadPage(string pageName) => _Load(pageName, Page);

        public void LoadAbovePage(string pageName) => _Load(pageName, AbovePage);

        public void LoadDialog(string pageName) => _Load(pageName, Dialog);

        public void ClearAll()
        {
            ClearBelowPage();
            ClearPage();
            ClearAbovePage();
            ClearDialog();
            ClearAboveDialog();
        }

        public void ClearBelowPage() => _ClearChild(BelowPage);

        public void ClearPage() => _ClearChild(Page);

        public void ClearAbovePage() => _ClearChild(AbovePage);

        public void ClearDialog() => _ClearChild(Dialog);

        public void ClearAboveDialog() => _ClearChild(AboveDialog);

        public UIControllerBase GetUIController(string pageName, List<UIControllerBase> children)
        {
            foreach (var child in children)
            {
                if (child.ControllerName == pageName)
                {
                    return child;
                }
            }

            return null;
        }

        #endregion PublicMethod

        #region PrivateMethod

        private static void _ClearChild(List<UIControllerBase> children)
        {
            foreach (var child in children)
            {
                child.gameObject.SetActive(false);
            }
        }

        private static void _Load(string pageName, List<UIControllerBase> children)
        {
            foreach (var child in children)
            {
                if (child.ControllerName == pageName)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        private void _OnGameOver()
        {
        }

        #endregion PrivateMethod

        #region Fields

        public static UIManager Instance;

        public List<UIControllerBase> BelowPage;
        public List<UIControllerBase> Page;
        public List<UIControllerBase> AbovePage;
        public List<UIControllerBase> Dialog;
        public List<UIControllerBase> AboveDialog;

        #endregion Fields
    }
}