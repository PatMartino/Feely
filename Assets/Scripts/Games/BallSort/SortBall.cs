using System;
using UnityEngine;

namespace Games.BallSort
{
    public class SortBall : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private BallColors ballColor;

        #endregion

        #region Functions

        public void Init()
        {
            GetComponent<SpriteRenderer>().color = ballColor switch
            {
                BallColors.Red => Color.red,
                BallColors.Blue => Color.blue,
                BallColors.Green => Color.green,
                BallColors.Yellow => Color.yellow,
                BallColors.Magenta => Color.magenta,
                BallColors.Black => Color.black,
                BallColors.Cyan => Color.cyan,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public BallColors OnGetBallColor()
        {
            return ballColor;
        }

        public void OnSetBallColor(BallColors color)
        {
            ballColor = color;
        }

        #endregion
    }
}
