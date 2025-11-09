/* 播放音效 By ShuaiGuo
  2025年11月9日
*/

using System;
using GameCore.GlobalVars;
using UnityEngine;
using EventType = GameCore.Enum.EventType;

namespace GameCore.MonoBehaviors
{
    public class AudioManager : MonoBehaviour
    {
        #region UnityBehaviour

        private void OnEnable()
        {
            G.GameEventManager.AddEventListener(EventType.EntryShadow, _OnEntryShadow);
            G.GameEventManager.AddEventListener(EventType.ExitShadow,  _OnExitShadow);
            G.GameEventManager.AddEventListener(EventType.ItemCollect, _OnItemCollect);
        }

        private void OnDisable()
        {
            G.GameEventManager.RemoveEventListener(EventType.EntryShadow, _OnEntryShadow);
            G.GameEventManager.RemoveEventListener(EventType.ExitShadow,  _OnExitShadow);
            G.GameEventManager.RemoveEventListener(EventType.ItemCollect, _OnItemCollect);
        }

        #endregion UnityBehaviour

        #region PrivateMethods

        private void _OnEntryShadow()
        {
            AudioSource.clip = EntryShadow;
            AudioSource.Play();
        }

        private void _OnExitShadow()
        {
            AudioSource.clip = ExitShadow;
            AudioSource.Play();
        }

        private void _OnItemCollect()
        {
            AudioSource.clip = ItemCollect;
            AudioSource.Play();
        }

        #endregion PrivateMethods

        #region Fields

        public AudioSource BGMSource;
        public AudioSource AudioSource;
        public AudioClip   EntryShadow;
        public AudioClip   ExitShadow;
        public AudioClip   ItemCollect;

        #endregion Fields
    }
}