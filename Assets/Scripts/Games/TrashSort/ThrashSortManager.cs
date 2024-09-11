using System;
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
        }

        private void Play()
        {
            TrashSortSignals.Instance.OnTrashGeneration?.Invoke();
            TrashSortSignals.Instance.OnTrashGeneration?.Invoke();
            TrashSortSignals.Instance.OnTrashGeneration?.Invoke();
            TrashSortSignals.Instance.OnTrashGeneration?.Invoke();
            TrashSortSignals.Instance.OnTrashGeneration?.Invoke();
            TrashSortSignals.Instance.OnAssignBins?.Invoke();
            TrashSortSignals.Instance.OnUpdateScore?.Invoke();
        }

        private void OnNextLevel()
        {
            _levelID++;
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
        }

        #endregion
    }
}
