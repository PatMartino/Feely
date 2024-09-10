using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Games.BallSort
{
    public class BallSortLevelManager : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private List<GameObject> tubes = new List<GameObject>();
        [SerializeField] private Transform ballHolder;

        #endregion

        #region Private Field

        private List<BallColors> _ballColors = new List<BallColors>() { BallColors.Cyan, BallColors.Pink, BallColors.Yellow, BallColors.Blue, BallColors.Green, BallColors.Purple, BallColors.Orange };
        private List<BallColors> _selectedColors = new List<BallColors>();
        private List<BallColors> _levelBalls = new List<BallColors>();

        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            BallSortSignals.Instance.OnGenerateLevel += OnGenerateLevel;
            BallSortSignals.Instance.OnAssignBallsToTubes += OnAssignBallsToTubes;
            BallSortSignals.Instance.OnClearLevel += OnClearLevel;
        }

        private void OnGenerateLevel()
        {
            var ballAmount = BallSortSignals.Instance.OnGetLevelType.Invoke() - 2;
            _selectedColors = _ballColors.OrderBy(x => Random.Range(0, _ballColors.Count))
                .Take(ballAmount).ToList();
            foreach (var ballColor in _selectedColors)
            {
                _levelBalls.Add(ballColor);
                _levelBalls.Add(ballColor);
                _levelBalls.Add(ballColor);
                _levelBalls.Add(ballColor);
            }

            _levelBalls = _levelBalls.OrderBy(x => Random.value).ToList();
            OnAssignBallsToTubes();
        }

        private void OnAssignBallsToTubes()
        {
            int ballnum = 0;
            for (int i = 0; i < BallSortSignals.Instance.OnGetLevelType.Invoke() - 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var ball = Object.Instantiate(Resources.Load<GameObject>("Games/BallSort/Objects/Ball"),ballHolder);
                    ball.GetComponent<SortBall>().OnSetBallColor(_levelBalls[ballnum]);
                    ball.GetComponent<SortBall>().Init();
                    tubes[i].GetComponent<Tube>().OnAssignBalls.Invoke(ball);
                    tubes[i].GetComponent<Tube>().Init();
                    ballnum++;
                }
            }
        }

        private void OnClearLevel()
        {
            for (int i = 0; i < BallSortSignals.Instance.OnGetLevelType.Invoke(); i++)
            {
                tubes[i].GetComponent<Tube>().OnClearBallsInTube();
            }
            for (int i = 0; i < ballHolder.childCount; i++)
            {
                Destroy(ballHolder.GetChild(i).gameObject);
            }
        }
        
        
        
        private void UnSubscribeEvents()
        {
            BallSortSignals.Instance.OnGenerateLevel -= OnGenerateLevel;
            BallSortSignals.Instance.OnAssignBallsToTubes -= OnAssignBallsToTubes;
            BallSortSignals.Instance.OnClearLevel -= OnClearLevel;
        }

        #endregion

    }
}