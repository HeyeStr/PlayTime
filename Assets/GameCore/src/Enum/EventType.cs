/* 事件类型 By ShuaiGuo
  2025年10月22日
*/

namespace GameCore.Enum
{
    public enum EventType
    {
        GameStart,
        GameOver,
        LevelPass,
        LevelRestart,

        ItemCollect,
        DayNightSwitch,

        #region Player

        Interact,

        #endregion Player
    }
}