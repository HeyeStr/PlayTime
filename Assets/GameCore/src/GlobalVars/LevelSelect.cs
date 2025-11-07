/* 选关场景管理 By ShuaiGuo
  2025年11月5日
*/

using System;
using System.Collections.Generic;
using GameCore.MonoBehaviors;
using GameCore.Utilities.Log;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.GlobalVars
{
    public class LevelSelect : MonoBehaviour
    {
        #region UintyBehaviour

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            G.GSaveManager.LoadGame();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += _OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= _OnSceneLoaded;
        }

        #endregion UintyBehaviour

        #region PublicMethods

        public void ResetLevelDoors()
        {
            foreach (var door in LevelDoors)
            {
                door.Close();
            }

            var firstDoor = LevelDoors[0];
            firstDoor.Open();
            LevelDoorsSituation[firstDoor] = firstDoor.IsEnable;
        }

        public void UpdateLevelDoors()
        {
            foreach (var pair in LevelDoorsSituation)
            {
                var levelDoor = pair.Key;
                if (levelDoor.IsEnable) continue;

                levelDoor.Open();
                LevelDoorsSituation[levelDoor] = levelDoor.IsEnable;
                break;
            }
        }

        #endregion PublicMethods

        #region PrivateMethods

        private void _UpdateDoorSituation()
        {
            foreach (var levelDoor in LevelDoors)
            {
                LevelDoorsSituation[levelDoor] = levelDoor.IsEnable;
            }
        }

        private void _OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "LvSelection1")
            {
                SuperDebug.Log("LoadScene: LvSelection1");

                UpdateLevelDoors();
                G.GSaveManager.SaveGame();
            }
        }

        #endregion PrivateMethods

        #region Fields

        public static LevelSelect                 Instance;
        public        List<LevelDoor>             LevelDoors          = new List<LevelDoor>();
        public        Dictionary<LevelDoor, bool> LevelDoorsSituation = new Dictionary<LevelDoor, bool>();

        #endregion Fields
    }
}