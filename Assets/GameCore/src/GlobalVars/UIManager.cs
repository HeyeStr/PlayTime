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

            if (BelowPage == null)
            {
                BelowPage = transform.Find("BelowPage").gameObject;
            }

            if (Page == null)
            {
                Page = transform.Find("BelowPage").gameObject;
            }

            if (AbovePage == null)
            {
                AbovePage = transform.Find("AbovePage").gameObject;
            }

            if (Dialog == null)
            {
                Dialog = transform.Find("Dialog").gameObject;
            }

            if (AboveDialog == null)
            {
                AboveDialog = transform.Find("AboveDialog").gameObject;
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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        #endregion UnityBehavior
        
        #region PublicMethod

        public void LoadPage(string prefabPath) => _LoadPrefab(prefabPath, Page);

        public void LoadAbovePage(string prefabPath) => _LoadPrefab(prefabPath, AbovePage);

        public void LoadDialog(string prefabPath) => _LoadPrefab(prefabPath, Dialog);

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
        

        #endregion PublicMethod

        #region PrivateMethod

        private void _ClearChild(GameObject go)
        {
            var allChildren = go.GetComponentsInChildren<Transform>(true);
            foreach (var child in allChildren)
            {
                if (child != go.transform)
                {
                    Destroy(child.gameObject);
                }
                var uiController = child.GetComponent<UIControllerBase>();
                if (_UIControllers.ContainsValue(uiController))
                {
                    _UIControllers.Remove(uiController.ControllerName);
                }
            }
        }

        private void _LoadPrefab(string prefabPath, GameObject pageLayer)
        {
            var go = Resources.Load<GameObject>(prefabPath);

            if (go != null)
            {
                var instance = Instantiate(go, transform.position, Quaternion.identity);
                instance.transform.SetParent(pageLayer.transform);
                var uiController = instance.GetComponentInChildren<UIControllerBase>();
                if (uiController != null)
                {
                    _UIControllers.Add(uiController.ControllerName, uiController);
                }
            }
            else
            {
                SuperDebug.LogError($"Prefab not found in {prefabPath}");
            }
        }

        private void _OnGameOver()
        {
            _UIControllers["GameOverPage"].gameObject.SetActive(true);
        }

        #endregion PrivateMethod

        #region Field

        public static UIManager Instance;

        public GameObject BelowPage;
        public GameObject Page;
        public GameObject AbovePage;
        public GameObject Dialog;
        public GameObject AboveDialog;
        public Dictionary<string, UIControllerBase> UIControllers => _UIControllers;

        private readonly Dictionary<string, UIControllerBase> _UIControllers =
            new Dictionary<string, UIControllerBase>();

        #endregion Field
    }
}