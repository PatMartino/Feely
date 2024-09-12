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
        [SerializeField] private GameObject organicBin;
        [SerializeField] private GameObject glassBin;
        [SerializeField] private Transform bin1Transform;
        [SerializeField] private Transform bin2Transform;

        [Header("Score Text")] 
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("UI Panels")] 
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject nextLevelUI;
        
        [Header("Time Text")] 
        [SerializeField] private TextMeshProUGUI timeText;
        
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
            TrashSortSignals.Instance.OnUpdateTime += OnUpdateTime;
            TrashSortSignals.Instance.OnGameUI += OnGameUI;
            TrashSortSignals.Instance.OnEndGameUI += OnEndGameUI;
            TrashSortSignals.Instance.OnActivateBins += OnActivateBins;
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
        
        private void OnUpdateTime()
        {
            int minutes = Mathf.FloorToInt((int)TrashSortSignals.Instance.OnGetTimeRemaining?.Invoke() / 60); // Dakikayı hesaplar
            int seconds = Mathf.FloorToInt((int)TrashSortSignals.Instance.OnGetTimeRemaining?.Invoke() % 60); // Saniyeyi hesaplar
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Dakika:saniye formatında yazdırır
        }

        private void OnGameUI()
        {
            gameUI.SetActive(true);
            nextLevelUI.SetActive(false);
        }

        private void OnEndGameUI()
        {
            gameUI.SetActive(false);
            nextLevelUI.SetActive(true);
        }

        private void OnActivateBins()
        {
            organicBin.SetActive(true);
            glassBin.SetActive(true);
        }
        
        private void UnSubscribeEvents()
        {
            TrashSortSignals.Instance.OnSwapBinsUI -= OnSwapBinsUI;
            TrashSortSignals.Instance.OnUpdateScore -= OnUpdateScore;
            TrashSortSignals.Instance.OnUpdateTime -= OnUpdateTime;
            TrashSortSignals.Instance.OnGameUI -= OnGameUI;
            TrashSortSignals.Instance.OnEndGameUI -= OnEndGameUI;
            TrashSortSignals.Instance.OnActivateBins -= OnActivateBins;
        }

        #endregion
    }
}