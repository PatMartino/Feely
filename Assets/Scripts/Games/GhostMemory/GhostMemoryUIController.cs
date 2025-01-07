using Games.CalculationResult;
using Signals;
using TMPro;
using UnityEngine;

namespace Games.GhostMemory
{
    public class GhostMemoryUIController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private GameObject nextLevelUI;

        #endregion
        
        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            UpdateTime();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            GhostMemorySignals.Instance.OnGameUI += OnGameUI;
            GhostMemorySignals.Instance.OnNextLevelUI += OnNextLevelUI;
        }

        private void OnGameUI()
        {
            nextLevelUI.SetActive(false);
            levelText.text = "Level " + GhostMemorySignals.Instance.OnGetChapterIndex();
        }
        
        private void OnNextLevelUI()
        {
            nextLevelUI.SetActive(true);
        }
        
        private void UpdateTime()
        {
            int minutes = Mathf.FloorToInt((int)CoreGameSignals.Instance.OnGetRemainingTime?.Invoke() / 60); // Dakikayı hesaplar
            int seconds = Mathf.FloorToInt((int)CoreGameSignals.Instance.OnGetRemainingTime?.Invoke() % 60); // Saniyeyi hesaplar
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Dakika:saniye formatında yazdırır
        }
        
        private void UnSubscribeEvents()
        {
            GhostMemorySignals.Instance.OnGameUI -= OnGameUI;
            GhostMemorySignals.Instance.OnNextLevelUI -= OnNextLevelUI;
        }

        #endregion
    }
}
