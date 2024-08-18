using System;
using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class MenuPanelController : MonoBehaviour
    {
        #region SerializedField
        
        [SerializeField] private GameObject[] menuIcons;
        [SerializeField] private Transform gamesAndTestPanelUI;
        [SerializeField] private Transform todayUI;
        [SerializeField] private Transform gamesUI;
        [SerializeField] private Transform testUI;
        [SerializeField] private Transform settingsUI;
        [SerializeField] private Transform notificationUI;

        #endregion

        #region Private Field

        private readonly Color _greenColor = new Color(0.567f, 1, 0.476f);

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
            UISignals.Instance.OnSwitchMenu += OnSwitchMenu;
        }

        private void OnSwitchMenu(MenuTypes type)
        {
            switch (type)
            {
                case MenuTypes.TodayMenu:
                    gamesUI.gameObject.SetActive(false);
                    testUI.gameObject.SetActive(false);
                    todayUI.gameObject.SetActive(true);
                    ChangeMenuIconColor(MenuTypes.TodayMenu);
                    break;
                case MenuTypes.GameMenu:
                    todayUI.gameObject.SetActive(false);
                    testUI.gameObject.SetActive(false);
                    gamesUI.gameObject.SetActive(true);
                    ChangeMenuIconColor(MenuTypes.GameMenu);
                    break;
                case MenuTypes.TestMenu:
                    gamesUI.gameObject.SetActive(false);
                    todayUI.gameObject.SetActive(false);
                    testUI.gameObject.SetActive(true);
                    ChangeMenuIconColor(MenuTypes.TestMenu);
                    break;

                case MenuTypes.SettingsMenu:
                    gamesAndTestPanelUI.gameObject.SetActive(false);
                    settingsUI.gameObject.SetActive(true);
                    break;
                case MenuTypes.NotificationMenu:
                    gamesAndTestPanelUI.gameObject.SetActive(false);
                    notificationUI.gameObject.SetActive(true);
                    break;
                case MenuTypes.GamesAndTestPanel:
                    settingsUI.gameObject.SetActive(false);
                    notificationUI.gameObject.SetActive(false);
                    gamesAndTestPanelUI.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void ChangeMenuIconColor(MenuTypes type)
        {
            switch (type)
            {
                case MenuTypes.TodayMenu:
                    menuIcons[0].GetComponent<Image>().color = _greenColor;
                    menuIcons[1].GetComponent<Image>().color = Color.white;
                    menuIcons[2].GetComponent<Image>().color = Color.white;
                    break;
                case MenuTypes.GameMenu:
                    menuIcons[0].GetComponent<Image>().color = Color.white;
                    menuIcons[1].GetComponent<Image>().color = _greenColor;
                    menuIcons[2].GetComponent<Image>().color = Color.white;
                    break;
                case MenuTypes.TestMenu:
                    menuIcons[0].GetComponent<Image>().color = Color.white;
                    menuIcons[1].GetComponent<Image>().color = Color.white;
                    menuIcons[2].GetComponent<Image>().color = _greenColor;
                    break;
            }
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnSwitchMenu -= OnSwitchMenu;
        }

        #endregion
    }
}
