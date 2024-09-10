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
                    _button.onClick.AddListener(() => UISignals.Instance.OnHome?.Invoke()); 
                    break;
                case MenuButtonType.GameButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnGame?.Invoke()); 
                    break;
                case MenuButtonType.TestButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnTest?.Invoke()); 
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
                case MenuButtonType.BallSortGame:
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnGameManagement?.Invoke(GamesAndTestsNames.BallSort));
                    break;
                case MenuButtonType.CardMatchGame:
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnGameManagement?.Invoke(GamesAndTestsNames.CardMatch));
                    break;
                case MenuButtonType.BallSortCoverPage:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.BallSortCoverPage));
                    break;
                case MenuButtonType.CardMatchCoverPage:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.CardMatchCoverPage));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}