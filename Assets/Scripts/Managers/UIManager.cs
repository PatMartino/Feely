using System;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Canvas canvas;

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
            UISignals.Instance.OnMenuUIManagement += OnMenuUIManagement;
        }

        private void OnMenuUIManagement(UIStates state)
        {
            switch (state)
            {
                case UIStates.MainMenu:
                    UIDestroyer();
                    Instantiate(Resources.Load<GameObject>("UI/MainMenu New"),canvas.transform);
                    break;
                case UIStates.BallSortInfo:
                    UIDestroyer();
                    break;
                case UIStates.BallSortGame:
                    UIDestroyer();
                    Instantiate(Resources.Load<GameObject>("Games/BallSort/UI/SortPuzzle"),canvas.transform);
                    break;
                case UIStates.CardMatchGame:
                    UIDestroyer();
                    Instantiate(Resources.Load<GameObject>("Games/CardMatch/UI/CardMatch"),canvas.transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void UIDestroyer()
        {
            Destroy(canvas.transform.GetChild(0).gameObject);
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnMenuUIManagement -= OnMenuUIManagement;
        }

        #endregion
    }
}