/* 只允许在初始平面移动的物体 By ShuaiGuo
  2025年11月1日
*/

using UnityEngine;

namespace GameCore.MonoBehaviors
{
    public class Moveable : MonoBehaviour
    {
        #region UnityBehaviour

        private void Start()
        {
            _StartPosition = transform.position;
        }

        private void Update()
        {
            if (Mathf.Abs(transform.position.y - _StartPosition.y) > PlaneLimitHeight)
            {
                transform.position = _StartPosition;
            }
        }

        #endregion UnityBehaviour

        #region Fields

        public  float   PlaneLimitHeight = 2f;
        private Vector3 _StartPosition;

        #endregion Fields
    }
}