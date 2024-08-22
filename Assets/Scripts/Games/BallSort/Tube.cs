using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Signals;
using UnityEngine;

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

        #region OnEnable

        private void OnEnable()
        {
            Init();
        }

        #endregion
        

        #region Functions

        private void Init()
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
            
            if (ballsInTube.Count>0 && !GameSignals.Instance.OnGetIsSelect() && !GameSignals.Instance.OnGetIsLevelFinished() && !_isComplete)
            {
                Debug.Log("Select");
                //StartCoroutine(WaitForSelect());
                GameSignals.Instance.OnAssignSelectBall?.Invoke(ballsInTube[^1],ballsInTube, ballPlaces);
                GameSignals.Instance.OnSetIsSelectTrue?.Invoke();
                Debug.Log(GameSignals.Instance.OnGetIsSelect());
                ballsInTube[^1].transform.DOMove(ballPlaces[4].transform.position, cycleLength);
                ballsInTube.RemoveAt(ballsInTube.Count-1);
            }
            else if (GameSignals.Instance.OnGetIsSelect() && ballsInTube.Count<4 && !GameSignals.Instance.OnGetIsLevelFinished())
            {
                
                Debug.Log("Drop");
                //StartCoroutine(WaitForSelect2());
                if (ballsInTube.Count >0)
                {
                    if (GameSignals.Instance.OnGetBall?.Invoke().GetComponent<SortBall>().OnGetBallColor() == ballsInTube[ballsInTube.Count-1].GetComponent<SortBall>().OnGetBallColor())
                    {
                        GameSignals.Instance.OnSetIsSelectFalse?.Invoke();
                        ballsInTube.Add(GameSignals.Instance.OnGetBall?.Invoke());
                        Debug.Log("Aynı Renk");
                        GameSignals.Instance.OnGetBall?.Invoke().transform
                            .DOMove(ballPlaces[4].transform.position, cycleLength).OnComplete(() =>{
                                GameSignals.Instance.OnGetBall?.Invoke().transform.DOMove(ballPlaces[ballsInTube.Count-1].transform.position, cycleLength).OnComplete(
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
                        GameSignals.Instance.OnSetIsSelectFalse?.Invoke();
                        GameSignals.Instance.OnGetPreviousTubeList?.Invoke().Add(GameSignals.Instance.OnGetBall?.Invoke());
                        int num = (GameSignals.Instance.OnGetPreviousTubeList().Count - 1);
                        GameSignals.Instance.OnGetBall?.Invoke().transform
                            .DOMove(GameSignals.Instance.OnGetPreviousBallPlaces()[num].transform.position, cycleLength);
                    }
                    
                }
                else
                {
                    Debug.Log("Boş Alan");
                    GameSignals.Instance.OnSetIsSelectFalse?.Invoke();
                    ballsInTube.Add(GameSignals.Instance.OnGetBall?.Invoke());
                    GameSignals.Instance.OnGetBall?.Invoke().transform
                        .DOMove(ballPlaces[4].transform.position, cycleLength).OnComplete(() =>{
                            GameSignals.Instance.OnGetBall?.Invoke().transform.DOMove(ballPlaces[ballsInTube.Count-1].transform.position, cycleLength).OnComplete(
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
            GameSignals.Instance.OnIncreaseCompletedTubes?.Invoke();
        }

        #endregion
    }
}
