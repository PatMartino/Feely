using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        
        [Header("Buttons")]
        [SerializeField] private Transform leftButtonTransform;
        [SerializeField] private Transform rightButtonTransform;
        
        [Header("Tween Settings")]
        [SerializeField] private float tweenTime;
        [SerializeField] private Ease tweenType;

        #endregion

        #region Private Field

        private List<TrashType> _leftSideBin = new List<TrashType>();
        private List<TrashType> _rightSideBin = new List<TrashType>();
        private int _trashCount = 0;

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
        }

        private void OnTrashGeneration()
        {
            var trash = Object.Instantiate(Resources.Load<GameObject>("Games/TrashSort/TrashObject/Trash"));
            int randomNumber = Random.Range(0, 100);
            if (TrashSortSignals.Instance.OnGetLevelID() < 4)
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
            
            trashes.Add(trash);
            trash.transform.position = trashTransforms[trashes.Count - 1].position;
        }

        private void OnRemoveTrash()
        {
            Destroy(trashes[0].gameObject,1f);
            trashes.RemoveAt(0);
            for (int i = 0; i < trashes.Count; i++)
            {
                trashes[i].transform.DOMove(trashTransforms[i].position,tweenTime).SetEase(tweenType);
            }

            _trashCount++;
            if (_trashCount > 25)
            {
                OnSwitchBins();
                _trashCount = 0;
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
            }
            else
            {
                TrashSortSignals.Instance.OnDecreaseScore?.Invoke();
            }
            
            OnRemoveTrash();
        }

        private void OnAssignBins()
        {
            _leftSideBin.Clear();
            _rightSideBin.Clear();
            _leftSideBin.Add(TrashType.Plastic);
            _leftSideBin.Add(TrashType.Organic);
            _rightSideBin.Add(TrashType.Paper);
            _rightSideBin.Add(TrashType.Glass);
        }

        private void OnSwitchBins()
        {
            (_leftSideBin[0], _rightSideBin[0]) = (_rightSideBin[0], _leftSideBin[0]);
            TrashSortSignals.Instance.OnPauseGame?.Invoke();
            TrashSortSignals.Instance.OnSwapBinsUI?.Invoke();
            StartCoroutine(WaitForSwapBins());
        }

        private IEnumerator WaitForSwapBins()
        {
            yield return new WaitForSeconds(0.7f);
            TrashSortSignals.Instance.OnContinueGame?.Invoke();
        }
        
        private void UnSubscribeEvents()
        {
            TrashSortSignals.Instance.OnTrashGeneration -= OnTrashGeneration;
            TrashSortSignals.Instance.OnRemoveTrash -= OnRemoveTrash;
            TrashSortSignals.Instance.OnThrowTrashToBin -= OnThrowTrashToBin;
            TrashSortSignals.Instance.OnAssignBins -= OnAssignBins;
        }

        #endregion
    }
}