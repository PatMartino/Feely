using System;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Games.TrashSort
{
    public class TrashSortButtonSubscriber : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private ButtonType type;

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
                case ButtonType.Left:
                    _button.onClick.AddListener(() => TrashSortSignals.Instance.OnThrowTrashToBin?.Invoke(ButtonType.Left));
                    break;
                case ButtonType.Right:
                    _button.onClick.AddListener(() => TrashSortSignals.Instance.OnThrowTrashToBin?.Invoke(ButtonType.Right)); 
                    break;
                case ButtonType.NextLevel:
                    _button.onClick.AddListener(() => TrashSortSignals.Instance.OnNextLevel?.Invoke());
                    break;
                case ButtonType.PlayAgain:
                    _button.onClick.AddListener(() => TrashSortSignals.Instance.OnPlayAgain?.Invoke());
                    break;
                case ButtonType.MainMenu:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MainMenu));
                    _button.onClick.AddListener(() =>CoreGameSignals.Instance.OnQuitGame?.Invoke());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}