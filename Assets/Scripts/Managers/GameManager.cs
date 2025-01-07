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
            CoreGameSignals.Instance.OnQuitGame += OnQuitGame;
        }

        private void OnGameManagement(GamesAndTestsNames type)
        {
            switch (type)
            {
                case GamesAndTestsNames.BallSort:
                    UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.BallSortGame);
                    Instantiate(Resources.Load<GameObject>("Games/BallSort/GameManagerObject/BallSort"),gameHolder);
                    break;
                case GamesAndTestsNames.CardMatch:
                    UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.CardMatchGame);
                    Instantiate(Resources.Load<GameObject>("Games/CardMatch/GameManagerObject/CardMatch"),gameHolder);
                    break;
                case GamesAndTestsNames.TrashSort:
                    UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.TrashSortGame);
                    Instantiate(Resources.Load<GameObject>("Games/TrashSort/GameManager/TrashSort"),gameHolder);
                    break;
                case GamesAndTestsNames.MathFundamental:
                    UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MathFundamentalGame);
                    Instantiate(Resources.Load<GameObject>("Games/MathFundamental/GameManager/MathFundamentalManager"),gameHolder);
                    break;
                case GamesAndTestsNames.CalculationResult:
                    UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.CalculationResultGame);
                    Instantiate(Resources.Load<GameObject>("Games/CalculationResult/GameManager/CalculationResultManager"),gameHolder);
                    break;
                case GamesAndTestsNames.GhostMemory:
                    UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.GhostMemoryGame);
                    Instantiate(Resources.Load<GameObject>("Games/GhostMemory/GameManagerObject/GhostMemoryManager"),gameHolder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void OnQuitGame()
        {
            if (gameHolder.childCount == 0) return;
            Destroy(gameHolder.GetChild(0).gameObject);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnGameManagement -= OnGameManagement;
            CoreGameSignals.Instance.OnQuitGame -= OnQuitGame;
        }

        #endregion
    }
}