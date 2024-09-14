using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Games.TrashSort
{
    public class TrashSortLevelManager : MonoBehaviour
    {
        #region Serialize Field

        [Header("Trashes")]
        [SerializeField] private List<GameObject> trashes = new List<GameObject>();
        [SerializeField] private List<Transform> trashTransforms = new List<Transform>();
        [SerializeField] private Transform noTrashPoint;
        
        [Header("Buttons")]
        [SerializeField] private Transform leftButtonTransform;
        [SerializeField] private Transform rightButtonTransform;
        
        [Header("Tween Settings")]
        [SerializeField] private float tweenTime;
        [SerializeField] private Ease tweenType;

        [Header("TrashHolder")] 
        [SerializeField] private Transform trashHolder;

        #endregion

        #region Private Field

        private List<TrashType> _leftSideBin = new List<TrashType>();
        private List<TrashType> _rightSideBin = new List<TrashType>();
        private int _trashCount = 0;
        private bool _notrash;
        private LevelStatus _levelStatus;
        private int _necessaryScore = 1000;
        private int _accurate;
        private int _wrong;
        private int _accuracy;
        private bool _isSwitch = false;

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
            TrashSortSignals.Instance.OnTrashGeneration += OnTrashGeneration;
            TrashSortSignals.Instance.OnRemoveTrash += OnRemoveTrash;
            TrashSortSignals.Instance.OnThrowTrashToBin += OnThrowTrashToBin;
            TrashSortSignals.Instance.OnAssignBins += OnAssignBins;
            TrashSortSignals.Instance.OnLevelFinish += OnLevelFinish;
            TrashSortSignals.Instance.OnGetLevelStatus += OnGetLevelStatus;
            TrashSortSignals.Instance.OnGetAccurateAmount += OnGetAccurateAmount;
            TrashSortSignals.Instance.OnGetWrongAmount += OnGetWrongAmount;
            TrashSortSignals.Instance.OnGetAccuracy += OnGetAccuracy;
            TrashSortSignals.Instance.OnStartLevel += OnStartLevel;
        }

        private void OnStartLevel()
        {
            if (TrashSortSignals.Instance.OnGetLevelID() > 4)
            {
                TrashSortSignals.Instance.OnActivateBins?.Invoke();
            }
            var trash = Object.Instantiate(Resources.Load<GameObject>("Games/TrashSort/TrashObject/Trash"),trashHolder);
            trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Plastic);
            trashes.Add(trash);
            trash.transform.position = trashTransforms[trashes.Count-1].position;
            OnTrashGeneration();
            OnTrashGeneration();
            OnTrashGeneration();
            OnTrashGeneration();
        }

        private void OnTrashGeneration()
        {
            var trash = Object.Instantiate(Resources.Load<GameObject>("Games/TrashSort/TrashObject/Trash"),trashHolder);
            int randomNumber = Random.Range(0, 100);
            if (TrashSortSignals.Instance.OnGetLevelID() < 3)
            {
                if (randomNumber<50)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Plastic);
                }
                else 
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Paper);
                }
            }

            else if(TrashSortSignals.Instance.OnGetLevelID() < 5)
            {
                if (randomNumber<45)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Plastic);
                }
                else if(randomNumber <90)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Paper);
                }
                else
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.NoTrash);
                }
            }
            else if(TrashSortSignals.Instance.OnGetLevelID() < 6)
            {
                if (randomNumber<35)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Plastic);
                }
                else if(randomNumber <70)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Paper);
                }
                else if(randomNumber <90)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Organic);
                }
                else
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.NoTrash);
                }
            }
            else
            {
                if (randomNumber<22)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Plastic);
                }
                else if(randomNumber <44)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Paper);
                }
                else if(randomNumber <66)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Organic);
                }
                else if(randomNumber <88)
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.Glass);
                }
                else
                {
                    trash.GetComponent<TrashObject>().OnSetType?.Invoke(TrashType.NoTrash);
                }
            }
            
            trashes.Add(trash);
            trash.transform.position = trashTransforms[trashes.Count-1].position;
        }

        private void OnRemoveTrash()
        {
            Destroy(trashes[0].gameObject,1f);
            trashes.RemoveAt(0);
            for (int i = 0; i < trashes.Count; i++)
            {
                trashes[i].transform.DOMove(trashTransforms[i].position,tweenTime).SetEase(tweenType);
            }

            CheckItemIsNotTrash();

            if (TrashSortSignals.Instance.OnGetLevelID() > 1)
            {
                _trashCount++;
            }
            
            if (_trashCount > 40 && !_isSwitch)
            {
                OnSwitchBins();
                _trashCount = 0;
                _isSwitch = true;
            }
            OnTrashGeneration();
        }

        private void OnThrowTrashToBin(ButtonType type)
        {
            if (TrashSortSignals.Instance.OnGetGameState() != TrashSortGameStates.Play) return;
            switch (type)
            {
                case ButtonType.Left:
                    trashes[0].transform.DOMove(leftButtonTransform.position, tweenTime).SetEase(tweenType);
                    trashes[0].transform.DOScale(Vector3.one*0, tweenTime).SetEase(tweenType);
                    break;
                case ButtonType.Right:
                    trashes[0].transform.DOMove(rightButtonTransform.position, tweenTime).SetEase(tweenType);
                    trashes[0].transform.DOScale(Vector3.one*0, tweenTime).SetEase(tweenType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            CheckTrash(type);
        }

        private void CheckTrash(ButtonType type)
        {
            var isMatch = false;
            switch (type)
            {
                case ButtonType.Left:
                    for (int i = 0; i < _leftSideBin.Count; i++)
                    {
                        if (trashes[0].GetComponent<TrashObject>().OnGetTrashType() == _leftSideBin[i])
                        {
                            isMatch = true;
                        }
                    }
                    break;
                case ButtonType.Right:
                    for (int i = 0; i < _rightSideBin.Count; i++)
                    {
                        if (trashes[0].GetComponent<TrashObject>().OnGetTrashType() == _rightSideBin[i])
                        {
                            isMatch = true;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            if (isMatch)
            {
                TrashSortSignals.Instance.OnIncreaseScore?.Invoke();
                _accurate++;
            }
            else
            {
                TrashSortSignals.Instance.OnDecreaseScore?.Invoke();
                if (trashes[0].GetComponent<TrashObject>().OnGetTrashType() == TrashType.NoTrash)
                {
                    _notrash = true;
                }

                _wrong++;
            }
            
            OnRemoveTrash();
        }

        private void OnAssignBins()
        {
            _trashCount = 0;
            trashes.Clear();
            _leftSideBin.Clear();
            _rightSideBin.Clear();
            _leftSideBin.Add(TrashType.Plastic);
            _leftSideBin.Add(TrashType.Organic);
            _rightSideBin.Add(TrashType.Paper);
            _rightSideBin.Add(TrashType.Glass);
            _isSwitch = false;
        }

        private void OnSwitchBins()
        {
            (_leftSideBin[0], _rightSideBin[0]) = (_rightSideBin[0], _leftSideBin[0]);
            TrashSortSignals.Instance.OnPauseGame?.Invoke();
            TrashSortSignals.Instance.OnSwapBinsUI?.Invoke();
            StartCoroutine(WaitForSwapBins());
        }

        private void OnLevelFinish()
        {
            CalculateAccuracy();
            _levelStatus = TrashSortSignals.Instance.OnGetScore() >= _necessaryScore ? LevelStatus.Complete : LevelStatus.Failed;
            DestroyAllTrash();
            TrashSortSignals.Instance.OnEndGameUI?.Invoke();
            TrashSortSignals.Instance.OnContinueGame?.Invoke();
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(1.5f);
            TrashSortSignals.Instance.OnPauseGame?.Invoke();
        }

        private void CheckItemIsNotTrash()
        {
            if (trashes[0].GetComponent<TrashObject>().OnGetTrashType() == TrashType.NoTrash)
            {
                StartCoroutine(NoTrashCoroutine());
            }
        }

        private IEnumerator WaitForSwapBins()
        {
            yield return new WaitForSeconds(0.7f);
            TrashSortSignals.Instance.OnContinueGame?.Invoke();
        }

        private void DestroyAllTrash()
        {
            for (int i = trashHolder.childCount - 1; i >= 0; i--)
            {
                GameObject child = trashHolder.GetChild(i).gameObject;
                Destroy(child);
            }
        }

        private IEnumerator NoTrashCoroutine()
        {
            yield return new WaitForSeconds(0.4f);
            if (!_notrash)
            {
                TrashSortSignals.Instance.OnIncreaseScore?.Invoke();
                trashes[0].transform.DOMove(noTrashPoint.position, tweenTime).SetEase(tweenType);
                OnRemoveTrash();
            }

            _notrash = false;
        }

        private void CalculateAccuracy()
        {
            var sum = _accurate + _wrong;
            float temp = (float)_accurate / sum * 100;
            _accuracy = (int)temp;
        }

        private LevelStatus OnGetLevelStatus()
        {
            return _levelStatus;
        }

        private int OnGetAccurateAmount()
        {
            return _accurate;
        }

        private int OnGetWrongAmount()
        {
            return _wrong;
        }

        private int OnGetAccuracy()
        {
            return _accuracy;
        }
        
        private void UnSubscribeEvents()
        {
            TrashSortSignals.Instance.OnTrashGeneration -= OnTrashGeneration;
            TrashSortSignals.Instance.OnRemoveTrash -= OnRemoveTrash;
            TrashSortSignals.Instance.OnThrowTrashToBin -= OnThrowTrashToBin;
            TrashSortSignals.Instance.OnAssignBins -= OnAssignBins;
            TrashSortSignals.Instance.OnLevelFinish -= OnLevelFinish;
            TrashSortSignals.Instance.OnGetLevelStatus -= OnGetLevelStatus;
            
            TrashSortSignals.Instance.OnGetAccurateAmount -= OnGetAccurateAmount;
            TrashSortSignals.Instance.OnGetWrongAmount -= OnGetWrongAmount;
            TrashSortSignals.Instance.OnGetAccuracy -= OnGetAccuracy;
            TrashSortSignals.Instance.OnStartLevel -= OnStartLevel;
        }

        #endregion
    }
}