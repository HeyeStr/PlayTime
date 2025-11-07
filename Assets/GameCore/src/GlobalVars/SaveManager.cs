/* 存档管理 By ShuaiGuo
  2025年11月6日
*/

using System.Collections.Generic;
using GameCore.MonoBehaviors;
using UnityEngine;

namespace GameCore.GlobalVars
{
    public class SaveManager : MonoBehaviour
    {
        #region UnityBehaviour

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

        #endregion UnityBehaviour

        #region PublicMethods

        public void SaveGame()
        {
            foreach (var levelDoor in LevelDoorToSave)
            {
                if (levelDoor == null) continue;

                var key         = SavePrefix + levelDoor.name;
                var activeState = levelDoor.IsEnable ? 1 : 0;
                PlayerPrefs.SetInt(key, activeState);
            }

            PlayerPrefs.Save();
        }

        public void LoadGame()
        {
            foreach (var levelDoor in LevelDoorToSave)
            {
                if (levelDoor == null) continue;

                var key = SavePrefix + levelDoor.name;

                if (!PlayerPrefs.HasKey(key)) continue;

                var activeState = PlayerPrefs.GetInt(key);
                if (activeState == 1)
                {
                    levelDoor.Open();
                }
                else if (activeState == 0)
                {
                    levelDoor.Close();
                }
            }
        }

        public void DeleteSave()
        {
            G.GLevelSelect.ResetLevelDoors();
            foreach (var levelDoor in LevelDoorToSave)
            {
                if (levelDoor == null) continue;

                var key = SavePrefix + levelDoor.name;
                PlayerPrefs.DeleteKey(key);
            }

            PlayerPrefs.Save();
        }

        #endregion PublicMethods

        #region Fields

        public        List<LevelDoor> LevelDoorToSave = new List<LevelDoor>();
        public static SaveManager     Instance;

        private const string SavePrefix = "ObjectState_";

        #endregion Fields
    }
}