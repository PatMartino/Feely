using System;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CalculationResult
{
    public class CalculationResultButtonSubscriber : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private CalculationResultButtonType type;

        #endregion

        #region Private Field

        private Button _button;

        #endregion

        #region Awake, OnEnable

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        #endregion

        private void SubscribeEvents()
        {
            switch (type)
            {
                case CalculationResultButtonType.True:
                    _button.onClick.AddListener(() =>CalculationResultSignals.Instance.OnCheckIsCorrect?.Invoke(true));
                    break;
                case CalculationResultButtonType.False:
                    _button.onClick.AddListener(() =>CalculationResultSignals.Instance.OnCheckIsCorrect?.Invoke(false));
                    break;
                case CalculationResultButtonType.NextLevel:
                    _button.onClick.AddListener(() =>CalculationResultSignals.Instance.OnNextLevel?.Invoke());
                    break;
                case CalculationResultButtonType.MainMenu:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MainMenu));
                    _button.onClick.AddListener(() =>CoreGameSignals.Instance.OnQuitGame?.Invoke());
                    break;
                case CalculationResultButtonType.PlayAgain:
                    _button.onClick.AddListener(() =>CalculationResultSignals.Instance.OnNextLevel?.Invoke());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}