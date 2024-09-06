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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}