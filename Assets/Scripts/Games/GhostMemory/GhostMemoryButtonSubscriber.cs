using System;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Games.GhostMemory
{
    public class GhostMemoryButtonSubscriber : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private GhostMemoryButtonType type;

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
                case GhostMemoryButtonType.NextLevel:
                    _button.onClick.AddListener(() => GhostMemorySignals.Instance.OnNextLevel?.Invoke());
                    break;
                case GhostMemoryButtonType.MainMenu:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MainMenu));
                    _button.onClick.AddListener(() =>CoreGameSignals.Instance.OnQuitGame?.Invoke());
                    break;
                case GhostMemoryButtonType.PlayAgain:
                    _button.onClick.AddListener(() =>GhostMemorySignals.Instance.OnRestartLevel?.Invoke());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
