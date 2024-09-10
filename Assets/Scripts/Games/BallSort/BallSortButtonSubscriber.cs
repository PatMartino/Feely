using System;
using Enums;
using Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Games.BallSort
{
    public class BallSortButtonSubscriber : MonoBehaviour
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
                    _button.onClick.AddListener(() =>BallSortSignals.Instance.OnNextLevel?.Invoke()); 
                    break;
                case GameButtonTypes.RestartButton:
                    _button.onClick.AddListener(() =>BallSortSignals.Instance.OnRestartLevel?.Invoke()); 
                    break;
                case GameButtonTypes.QuitGame:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MainMenu));
                    _button.onClick.AddListener(() =>CoreGameSignals.Instance.OnQuitGame?.Invoke());
                    break;
                case GameButtonTypes.PauseGame:
                    _button.onClick.AddListener(() => BallSortSignals.Instance.OnOpenPauseMenu?.Invoke());
                    _button.onClick.AddListener(() => BallSortSignals.Instance.OnDeActivateGame?.Invoke());
                    break;
                case GameButtonTypes.ReturnGame:
                    _button.onClick.AddListener(() => BallSortSignals.Instance.OnClosePauseMenu?.Invoke());
                    _button.onClick.AddListener(() => BallSortSignals.Instance.OnActivateGame?.Invoke());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}