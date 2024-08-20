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
        }

        private void OnUpdateLevelText()
        {
            levelText.text = "Level " + GameSignals.Instance.OnGetLevelID();
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
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnUpdateBallSortLevelIDText -= OnUpdateLevelText;
            UISignals.Instance.OnCompleteBallSortLevel -= OnCompleteLevel;
            UISignals.Instance.OnStartBallSortLevel -= OnStartLevel;
        }

        #endregion

    }
}
