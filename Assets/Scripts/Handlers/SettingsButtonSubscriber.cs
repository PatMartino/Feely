using System;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Handlers
{
    public class SettingsButtonSubscriber : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private SettingButtonsType type;

        #endregion

        #region Private Field

        private Button _button;

        #endregion

        #region Awake, OnEnable

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            switch (type)
            {
                case SettingButtonsType.Age:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSettingsPanelController?.Invoke(SettingButtonsType.Age));
                    break;
                case SettingButtonsType.Sound:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSettingsPanelController?.Invoke(SettingButtonsType.Sound));
                    break;
                case SettingButtonsType.DarkTheme:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSettingsPanelController?.Invoke(SettingButtonsType.DarkTheme));
                    break;
                case SettingButtonsType.Language:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSettingsPanelController?.Invoke(SettingButtonsType.Language));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
