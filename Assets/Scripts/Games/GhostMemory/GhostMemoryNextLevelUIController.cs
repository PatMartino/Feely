using System;
using Enums;
using Games.CalculationResult;
using TMPro;
using UnityEngine;

namespace Games.GhostMemory
{
    public class GhostMemoryNextLevelUIController : MonoBehaviour
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
            switch (GhostMemorySignals.Instance.OnGetLevelStatus())
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
            difficultyLevelText.text = GhostMemorySignals.Instance.OnGetChapterIndex().ToString();
            difficultyText.text = "Pass 7 levels in 60 seconds to level up!";
        }

        #endregion
    }
}
