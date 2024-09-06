using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Games.BallSort
{
    public class Tube : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private List<GameObject> ballPlaces = new List<GameObject>();
        [SerializeField] private List<GameObject> ballsInTube = new List<GameObject>();
        [SerializeField] private float cycleLength = 1f;

        #endregion

        #region Private Field

        private bool _isComplete;

        #endregion

        #region Public Field

        public UnityAction<GameObject> OnAssignBalls = delegate {  };

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            OnAssignBalls += AssignBalls;
        }

        #endregion
        

        #region Functions

        public void Init()
        {
            for (int i = 0; i < ballsInTube.Count; i++)
            {
                ballsInTube[i].transform.position = ballPlaces[i].transform.position;
            }
        }

        private void OnMouseDown()
        {
            SelectBall();
        }

        private void SelectBall()
        {
            
            if (ballsInTube.Count>0 && !BallSortSignals.Instance.OnGetIsSelect() && !BallSortSignals.Instance.OnGetIsLevelFinished() && !_isComplete)
            {
                Debug.Log("Select");
                //StartCoroutine(WaitForSelect());
                BallSortSignals.Instance.OnAssignSelectBall?.Invoke(ballsInTube[^1],ballsInTube, ballPlaces);
                BallSortSignals.Instance.OnSetIsSelectTrue?.Invoke();
                Debug.Log(BallSortSignals.Instance.OnGetIsSelect());
                ballsInTube[^1].transform.DOMove(ballPlaces[4].transform.position, cycleLength);
                ballsInTube.RemoveAt(ballsInTube.Count-1);
            }
            else if (BallSortSignals.Instance.OnGetIsSelect() && ballsInTube.Count<4 && !BallSortSignals.Instance.OnGetIsLevelFinished())
            {
                
                Debug.Log("Drop");
                //StartCoroutine(WaitForSelect2());
                if (ballsInTube.Count >0)
                {
                    if (BallSortSignals.Instance.OnGetBall?.Invoke().GetComponent<SortBall>().OnGetBallColor() == ballsInTube[ballsInTube.Count-1].GetComponent<SortBall>().OnGetBallColor())
                    {
                        BallSortSignals.Instance.OnSetIsSelectFalse?.Invoke();
                        ballsInTube.Add(BallSortSignals.Instance.OnGetBall?.Invoke());
                        Debug.Log("Aynı Renk");
                        BallSortSignals.Instance.OnGetBall?.Invoke().transform
                            .DOMove(ballPlaces[4].transform.position, cycleLength).OnComplete(() =>{
                                BallSortSignals.Instance.OnGetBall?.Invoke().transform.DOMove(ballPlaces[ballsInTube.Count-1].transform.position, cycleLength).OnComplete(
                                    () =>
                                    {
                                        if (ballsInTube.Count ==4)
                                        {
                                            CheckComplete();
                                        }
                               
                                    });
                       
                            });
                    }
                    else
                    {
                        Debug.Log("Farklı Renk");
                        BallSortSignals.Instance.OnSetIsSelectFalse?.Invoke();
                        BallSortSignals.Instance.OnGetPreviousTubeList?.Invoke().Add(BallSortSignals.Instance.OnGetBall?.Invoke());
                        int num = (BallSortSignals.Instance.OnGetPreviousTubeList().Count - 1);
                        BallSortSignals.Instance.OnGetBall?.Invoke().transform
                            .DOMove(BallSortSignals.Instance.OnGetPreviousBallPlaces()[num].transform.position, cycleLength);
                    }
                    
                }
                else
                {
                    Debug.Log("Boş Alan");
                    BallSortSignals.Instance.OnSetIsSelectFalse?.Invoke();
                    ballsInTube.Add(BallSortSignals.Instance.OnGetBall?.Invoke());
                    BallSortSignals.Instance.OnGetBall?.Invoke().transform
                        .DOMove(ballPlaces[4].transform.position, cycleLength).OnComplete(() =>{
                            BallSortSignals.Instance.OnGetBall?.Invoke().transform.DOMove(ballPlaces[ballsInTube.Count-1].transform.position, cycleLength).OnComplete(
                                () =>
                                {
                                    if (ballsInTube.Count ==4)
                                    {
                                        CheckComplete();
                                    }
                               
                                });
                       
                        });
                }
                
                
                
            }
        }

        private void CheckComplete()
        {
            var color = ballsInTube[0].GetComponent<SortBall>().OnGetBallColor();
            int i = 0;
            while (i<4)
            {
                if (color !=ballsInTube[i].GetComponent<SortBall>().OnGetBallColor())
                {
                    break;
                }
                else
                {
                    i++;
                }
            }

            if (i==4)
            {
                Complete();
            }
        }

        private void Complete()
        {
            _isComplete = true;
            GetComponent<SpriteRenderer>().color = Color.green;
            BallSortSignals.Instance.OnIncreaseCompletedTubes?.Invoke();
        }

        private void AssignBalls(GameObject ball1)
        {
            ballsInTube.Add(ball1);
        }

        public void OnClearBallsInTube()
        {
            ballsInTube.Clear();
            GetComponent<SpriteRenderer>().color = Color.white;
            _isComplete = false;
        }

        #endregion
    }
}
