using System;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Games.TrashSort
{
    public class TrashSortNextLevelUIController : MonoBehaviour
    {
        
        #region Serialize Field

        [Header("Level Status Text")]
        [SerializeField] private TextMeshProUGUI levelStatusText;
        
        [Header("Score Text")]
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Accuracy Texts")]
        [SerializeField] private TextMeshProUGUI accurateAmountText;
        [SerializeField] private TextMeshProUGUI wrongAmountText;
        [SerializeField] private TextMeshProUGUI accuracyAmountText;

        [Header("Difficulty")] 
        [SerializeField] private TextMeshProUGUI difficultyLevelText;
        [SerializeField] private TextMeshProUGUI difficultyText;

        [Header("Buttons")] 
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject playAgainButton;

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            Init();
        }

        #endregion

        #region Function

        private void Init()
        {
            switch (TrashSortSignals.Instance.OnGetLevelStatus())
            {
                case LevelStatus.Complete:
                    levelStatusText.text = "Level Complete!";
                    nextLevelButton.SetActive(true);
                    playAgainButton.SetActive(false);
                    break;
                case LevelStatus.Failed:
                    levelStatusText.text = "Level Failed!";
                    nextLevelButton.SetActive(false);
                    playAgainButton.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            scoreText.text = TrashSortSignals.Instance.OnGetScore().ToString();
            difficultyLevelText.text = TrashSortSignals.Instance.OnGetLevelID().ToString();
            difficultyText.text = "Collect more than 1000 points in 40 seconds to level up";
            accurateAmountText.text = TrashSortSignals.Instance.OnGetAccurateAmount().ToString();
            wrongAmountText.text = TrashSortSignals.Instance.OnGetWrongAmount().ToString();
            accuracyAmountText.text = TrashSortSignals.Instance.OnGetAccuracy().ToString();
        }

        #endregion
    }
}