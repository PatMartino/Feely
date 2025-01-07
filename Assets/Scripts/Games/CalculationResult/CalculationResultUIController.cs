using Signals;
using TMPro;
using UnityEngine;

namespace Games.CalculationResult
{
    public class CalculationResultUIController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI number1;
        [SerializeField] private TextMeshProUGUI number2;
        [SerializeField] private TextMeshProUGUI answer;
        [SerializeField] private TextMeshProUGUI sign;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private GameObject gameUI;
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
            CalculationResultSignals.Instance.OnSetTexts += OnSetTexts;
            CalculationResultSignals.Instance.OnGameUI += OnGameUI;
            CalculationResultSignals.Instance.OnNextLevelUI += OnNextLevelUI;
        }

        private void OnGameUI()
        {
            gameUI.SetActive(true);
            nextLevelUI.SetActive(false);
        }
        
        private void OnNextLevelUI()
        {
            gameUI.SetActive(false);
            nextLevelUI.SetActive(true);
        }

        private void OnSetTexts(int num1, int num2, string s, int result)
        {
            number1.text = num1.ToString();
            number2.text = num2.ToString();
            sign.text = s.ToString();
            answer.text = result.ToString();
        }
        
        private void UpdateTime()
        {
            int minutes = Mathf.FloorToInt((int)CoreGameSignals.Instance.OnGetRemainingTime?.Invoke() / 60); // Dakikayı hesaplar
            int seconds = Mathf.FloorToInt((int)CoreGameSignals.Instance.OnGetRemainingTime?.Invoke() % 60); // Saniyeyi hesaplar
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Dakika:saniye formatında yazdırır
        }
        
        private void UnSubscribeEvents()
        {
            CalculationResultSignals.Instance.OnSetTexts += OnSetTexts;
            CalculationResultSignals.Instance.OnGameUI -= OnGameUI;
            CalculationResultSignals.Instance.OnNextLevelUI -= OnNextLevelUI;
        }

        #endregion
    }
}