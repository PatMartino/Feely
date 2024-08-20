using System;
using UnityEngine;

namespace Games.BallSort
{
    public class SortBall : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private BallColors ballColor;

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            Init();
        }

        #endregion

        #region Functions

        private void Init()
        {
            GetComponent<SpriteRenderer>().color = ballColor switch
            {
                BallColors.Red => Color.red,
                BallColors.Blue => Color.blue,
                BallColors.Green => Color.green,
                BallColors.Yellow => Color.yellow,
                BallColors.Magenta => Color.magenta,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public BallColors OnGetBallColor()
        {
            return ballColor;
        }

        #endregion
    }
}
