using System;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Games.CalculationResult
{
    public class CalculationResultNextLevelUIController : MonoBehaviour
    {
        #region Serialize Field

        [Header("Level Status Text")]
        [SerializeField] private TextMeshProUGUI levelStatusText;

        [Header("Images")]
        [SerializeField] private GameObject levelCompleteImage;
        [SerializeField] private GameObject levelFailedImage;

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
            switch (CalculationResultSignals.Instance.OnGetLevelStatus())
            {
                case LevelStatus.Complete:
                    levelStatusText.text = "Level Complete!";
                    nextLevelButton.SetActive(true);
                    playAgainButton.SetActive(false);
                    levelCompleteImage.SetActive(true);
                    levelFailedImage.SetActive(false);
                    break;
                case LevelStatus.Failed:
                    levelStatusText.text = "Level Failed!";
                    nextLevelButton.SetActive(false);
                    playAgainButton.SetActive(true);
                    
                    levelCompleteImage.SetActive(false);
                    levelFailedImage.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            difficultyLevelText.text = CalculationResultSignals.Instance.OnGetLevelID().ToString();
            difficultyText.text = "Answer 7 questions in 60 seconds to level up!";
        }

        #endregion
    }
}