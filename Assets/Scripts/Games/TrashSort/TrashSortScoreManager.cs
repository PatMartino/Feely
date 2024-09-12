using Signals;
using UnityEngine;

namespace Games.TrashSort
{
    public class TrashSortScoreManager : MonoBehaviour
    {
        #region Private Field

        private int _score;

        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Function

        private void SubscribeEvents()
        {
            TrashSortSignals.Instance.OnIncreaseScore += OnIncreaseScore;
            TrashSortSignals.Instance.OnDecreaseScore += OnDecreaseScore;
            TrashSortSignals.Instance.OnGetScore += OnGetScore;
            TrashSortSignals.Instance.OnResetScore += OnResetScore;
        }

        private void OnIncreaseScore()
        {
            _score += 10;
            TrashSortSignals.Instance.OnUpdateScore?.Invoke();
        }

        private void OnDecreaseScore()
        {
            _score -= 20;
            TrashSortSignals.Instance.OnUpdateScore?.Invoke();
        }

        private void OnResetScore()
        {
            _score = 0;
        }

        private int OnGetScore()
        {
            return _score;
        }

        private void UnSubscribeEvents()
        {
            TrashSortSignals.Instance.OnIncreaseScore -= OnIncreaseScore;
            TrashSortSignals.Instance.OnDecreaseScore -= OnDecreaseScore;
            TrashSortSignals.Instance.OnGetScore -= OnGetScore;
            TrashSortSignals.Instance.OnResetScore -= OnResetScore;
        }

        #endregion
    }
}