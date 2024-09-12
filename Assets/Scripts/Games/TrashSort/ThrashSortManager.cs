using System;
using Enums;
using Signals;
using UnityEngine;

namespace Games.TrashSort
{
    public class ThrashSortManager : MonoBehaviour
    {
        #region Private Field

        private int _levelID =1;
        private TrashSortGameStates _state = TrashSortGameStates.Play;

        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            Play();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            TrashSortSignals.Instance.OnGetLevelID += OnGetLevelID;
            TrashSortSignals.Instance.OnPauseGame += OnPauseGame;
            TrashSortSignals.Instance.OnContinueGame += OnContinueGame;
            TrashSortSignals.Instance.OnGetGameState += OnGetGameState;
            TrashSortSignals.Instance.OnNextLevel += OnNextLevel;
            TrashSortSignals.Instance.OnPlayAgain += OnPlayAgain;
        }

        private void Play()
        {
            TrashSortSignals.Instance.OnAssignBins?.Invoke();
            TrashSortSignals.Instance.OnStartLevel?.Invoke();
            TrashSortSignals.Instance.OnUpdateScore?.Invoke();
            TrashSortSignals.Instance.OnStartTimer?.Invoke(40);
        }

        private void OnNextLevel()
        {
            TrashSortSignals.Instance.OnAssignBins?.Invoke();
            _levelID++;
            TrashSortSignals.Instance.OnStartLevel?.Invoke();
            TrashSortSignals.Instance.OnStartTimer?.Invoke(40);
            TrashSortSignals.Instance.OnResetScore?.Invoke();
            TrashSortSignals.Instance.OnGameUI?.Invoke();
        }

        private void OnPlayAgain()
        {
            TrashSortSignals.Instance.OnAssignBins?.Invoke();
            TrashSortSignals.Instance.OnStartLevel?.Invoke();
            TrashSortSignals.Instance.OnStartTimer?.Invoke(40);
            TrashSortSignals.Instance.OnResetScore?.Invoke();
            TrashSortSignals.Instance.OnGameUI?.Invoke();
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }

        private void OnPauseGame()
        {
            _state = TrashSortGameStates.Pause;
        }

        private void OnContinueGame()
        {
            _state = TrashSortGameStates.Play;
        }

        private TrashSortGameStates OnGetGameState()
        {
            return _state;
        }

        private void UnSubscribeEvents()
        {
            TrashSortSignals.Instance.OnGetLevelID -= OnGetLevelID;
            TrashSortSignals.Instance.OnPauseGame -= OnPauseGame;
            TrashSortSignals.Instance.OnContinueGame -= OnContinueGame;
            TrashSortSignals.Instance.OnGetGameState -= OnGetGameState;
            TrashSortSignals.Instance.OnNextLevel -= OnNextLevel;
            TrashSortSignals.Instance.OnPlayAgain -= OnPlayAgain;
        }

        #endregion
    }
}
