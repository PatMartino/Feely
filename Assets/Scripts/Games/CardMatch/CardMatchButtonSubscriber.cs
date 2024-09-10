using System;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CardMatch
{
    public class CardMatchButtonSubscriber : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private GameButtonTypes type;

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
                case GameButtonTypes.NextLevelButton:
                    _button.onClick.AddListener(() => CardMatchSignals.Instance.OnNextPlay?.Invoke()); 
                    break;
                case GameButtonTypes.QuitGame:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MainMenu));
                    _button.onClick.AddListener(() =>CoreGameSignals.Instance.OnQuitGame?.Invoke());
                    break;
                case GameButtonTypes.PauseGame:
                    _button.onClick.AddListener(() => CardMatchSignals.Instance.OnOpenPauseMenu.Invoke());
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnStopTimer.Invoke());
                    break;
                case GameButtonTypes.ReturnGame:
                    _button.onClick.AddListener(() => CardMatchSignals.Instance.OnClosePauseMenu.Invoke());
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnContinuousTimer.Invoke());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
