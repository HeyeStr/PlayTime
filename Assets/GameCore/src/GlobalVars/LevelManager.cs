/* 关卡管理 By ShuaiGuo
  2025年10月31日
*/

namespace GameCore.GlobalVars
{
    public class LevelManager
    {
        #region Singleton

        private static LevelManager _Instance;

        public static LevelManager Instance
        {
            get { return _Instance ??= new LevelManager(); }
        }

        #endregion Singleton

        #region Fields

        public int CurrentLevel;
        public int MaxLevel;

        #endregion Fields
    }
}