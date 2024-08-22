using System;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Transform gameHolder;

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

        #region Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnGameManagement += OnGameManagement;
        }

        private void OnGameManagement(GamesAndTestsNames type)
        {
            switch (type)
            {
                case GamesAndTestsNames.BallSort:
                    UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.BallSortGame);
                    Instantiate(Resources.Load<GameObject>("Games/BallSort/GameManagerObject/BallSort"),gameHolder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnGameManagement -= OnGameManagement;
        }

        #endregion
    }
}