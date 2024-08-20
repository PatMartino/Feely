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
                    break;
                case UIStates.BallSortInfo:
                    UIDestroyer();
                    break;
                case UIStates.BallSortGame:
                    UIDestroyer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void UIDestroyer()
        {
            Destroy(canvas.transform.GetChild(0));
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnMenuUIManagement -= OnMenuUIManagement;
        }

        #endregion
    }
}