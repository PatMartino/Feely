using System;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;

namespace Games.TrashSort
{
    public class TrashSortUIController : MonoBehaviour
    {
        #region Serialize Field

        [Header("Bins")]
        [SerializeField] private GameObject plasticBin;
        [SerializeField] private GameObject paperBin;
        [SerializeField] private Transform bin1Transform;
        [SerializeField] private Transform bin2Transform;

        [Header("Score Panel")] 
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [Header("Tween Settings")]
        [SerializeField] private float tweenTime;
        [SerializeField] private Ease tweenType;

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
            TrashSortSignals.Instance.OnSwapBinsUI += OnSwapBinsUI;
            TrashSortSignals.Instance.OnUpdateScore += OnUpdateScore;
        }

        private void OnSwapBinsUI()
        {
            plasticBin.transform.DOMove(bin2Transform.position, tweenTime).SetEase(tweenType);
            paperBin.transform.DOMove(bin1Transform.position, tweenTime).SetEase(tweenType);
        }

        private void OnUpdateScore()
        {
            scoreText.text = "Score: " + TrashSortSignals.Instance.OnGetScore();
        }
        
        private void UnSubscribeEvents()
        {
            TrashSortSignals.Instance.OnSwapBinsUI -= OnSwapBinsUI;
            TrashSortSignals.Instance.OnUpdateScore -= OnUpdateScore;
        }

        #endregion
    }
}