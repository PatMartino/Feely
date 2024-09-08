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
            GetComponentInChildren<SpriteRenderer>().color = ballColor switch
            {
                BallColors.Cyan => new Color(0.3f, 0.8f, 0.6f),
                BallColors.Pink => new Color(0.75f, 0.3f, 0.4f),
                BallColors.Yellow => new Color(0.9f, 0.65f, 0.3f),
                BallColors.Blue => new Color(0.4f, 0.5f, 0.8f),
                BallColors.Green => new Color(0.6f, 0.6f, 0.3f),
                BallColors.Purple => new Color(0.6f, 0.3f, 0.8f),
                BallColors.Orange => new Color(0.7f, 0.4f, 0.2f),
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
