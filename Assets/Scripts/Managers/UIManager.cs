using System;
using System.Collections;
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
                case UIStates.BallSortGame:
                    UIDestroyer();
                    Instantiate(Resources.Load<GameObject>("Games/BallSort/UI/SortPuzzle"),canvas.transform);
                    break;
                case UIStates.CardMatchGame:
                    UIDestroyer();
                    Instantiate(Resources.Load<GameObject>("Games/CardMatch/UI/CardMatch"),canvas.transform);
                    break;
                case UIStates.BallSortCoverPage:
                    Instantiate(Resources.Load<GameObject>("UI/CoverPages/BallSortCoverPage"),canvas.transform);
                    UIDestroyer();
                    break;
                case UIStates.CardMatchCoverPage:
                    Instantiate(Resources.Load<GameObject>("UI/CoverPages/CardMatchCoverPage"),canvas.transform);
                    UIDestroyer();
                    break;
                case UIStates.Test:
                    StartCoroutine(WaitForDeActivate());
                    Instantiate(Resources.Load<GameObject>("Test/TestManagerFolder/TestManager"),canvas.transform);
                    break;
                case UIStates.TestToMainMenu:
                    canvas.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case UIStates.TrashSortGame:
                    UIDestroyer();
                    Instantiate(Resources.Load<GameObject>("Games/TrashSort/UI/TrashSort"),canvas.transform);
                    break;
                case UIStates.TrashSortCoverPage:
                    Instantiate(Resources.Load<GameObject>("UI/CoverPages/TrashSortCoverPage"),canvas.transform);
                    UIDestroyer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void UIDestroyer()
        {
            Destroy(canvas.transform.GetChild(0).gameObject);
        }

        private IEnumerator WaitForDeActivate()
        {
            yield return new WaitForSeconds(1);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
        }

        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnMenuUIManagement -= OnMenuUIManagement;
        }

        #endregion
    }
}