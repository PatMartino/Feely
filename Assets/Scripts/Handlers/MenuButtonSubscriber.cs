using System;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Handlers
{
    public class MenuButtonSubscriber : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private MenuButtonType type;

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
                case MenuButtonType.TodayButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.TodayMenu)); 
                    break;
                case MenuButtonType.GameButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.GameMenu)); 
                    break;
                case MenuButtonType.TestButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.TestMenu)); 
                    break;
                case MenuButtonType.SettingsButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.SettingsMenu));
                    break;
                case MenuButtonType.NotificationsButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.NotificationMenu));
                    break;
                case MenuButtonType.SettingsBack:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.GamesAndTestPanel));
                    break;
                case MenuButtonType.NotificationBack:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.GamesAndTestPanel));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}