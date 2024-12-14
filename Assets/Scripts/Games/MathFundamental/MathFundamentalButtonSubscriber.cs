using System;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Games.MathFundamental
{
    public class MathFundamentalButtonSubscriber : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private MathFundamentalButtonsType type;

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
                case MathFundamentalButtonsType.Answer1:
                    _button.onClick.AddListener(() =>MathFundamentalSignals.Instance.OnCheckIsCorrect?.Invoke(0));
                    break;
                case MathFundamentalButtonsType.Answer2:
                    _button.onClick.AddListener(() =>MathFundamentalSignals.Instance.OnCheckIsCorrect?.Invoke(1));
                    break;
                case MathFundamentalButtonsType.Answer3:
                    _button.onClick.AddListener(() =>MathFundamentalSignals.Instance.OnCheckIsCorrect?.Invoke(2));
                    break;
                case MathFundamentalButtonsType.NextLevel:
                    _button.onClick.AddListener(() =>MathFundamentalSignals.Instance.OnNextLevel?.Invoke());
                    break;
                case MathFundamentalButtonsType.MainMenu:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MainMenu));
                    _button.onClick.AddListener(() =>CoreGameSignals.Instance.OnQuitGame?.Invoke());
                    break;
                case MathFundamentalButtonsType.PlayAgain:
                    _button.onClick.AddListener(() =>MathFundamentalSignals.Instance.OnNextLevel?.Invoke());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}