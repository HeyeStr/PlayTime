/* 全局变量表 By ShuaiGuo
  2025年10月22日
*/

using GameCore.Utilities;

namespace GameCore.GlobalVars
{
    public static class G
    {
        public static PlayerController GPlayerController => PlayerController.Instance;
        public static MainCamera       Camera            => MainCamera.Instance;
        public static LevelManager     GLevelManager     => LevelManager.Instance;
        public static UIManager        GUIManager        => UIManager.Instance;
        public static GlobalAdmin      Admin             => GlobalAdmin.Instance;

        public static bool UseUnscaledDeltaTime = false;

        public static readonly EventManager GameEventManager = new EventManager();
    }
}