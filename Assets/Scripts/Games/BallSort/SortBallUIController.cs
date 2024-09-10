using System;
using Signals;
using TMPro;
using UnityEngine;

namespace Games.BallSort
{
    public class SortBallUIController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject pauseMenu;

        #endregion

        #region Onenbale, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Function

        private void SubscribeEvents()
        {
            UISignals.Instance.OnUpdateBallSortLevelIDText += OnUpdateLevelText;
            UISignals.Instance.OnCompleteBallSortLevel += OnCompleteLevel;
            UISignals.Instance.OnStartBallSortLevel += OnStartLevel;
            BallSortSignals.Instance.OnOpenPauseMenu += OnOpenPauseMenu;
            BallSortSignals.Instance.OnClosePauseMenu += OnClosePauseMenu;
        }

        private void OnUpdateLevelText()
        {
            levelText.text = "Level " + BallSortSignals.Instance.OnGetLevelID();
        }

        private void OnCompleteLevel()
        {
            nextLevelButton.SetActive(true);
            restartButton.SetActive(false);
        }
        
        private void OnStartLevel()
        {
            nextLevelButton.SetActive(false);
            restartButton.SetActive(true);
        }

        private void OnOpenPauseMenu()
        {
            pauseMenu.SetActive(true);
        }

        private void OnClosePauseMenu()
        {
            pauseMenu.SetActive(false);
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnUpdateBallSortLevelIDText -= OnUpdateLevelText;
            UISignals.Instance.OnCompleteBallSortLevel -= OnCompleteLevel;
            UISignals.Instance.OnStartBallSortLevel -= OnStartLevel;
            BallSortSignals.Instance.OnOpenPauseMenu -= OnOpenPauseMenu;
            BallSortSignals.Instance.OnClosePauseMenu -= OnClosePauseMenu;
        }

        #endregion

    }
}
