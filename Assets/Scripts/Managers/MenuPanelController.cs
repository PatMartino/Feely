using System;
using System.Collections.Generic;
using Enums;
using Signals;
using TMPro;
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
        [SerializeField] private GameObject headerText;

        #endregion

        #region Private Field

        private readonly Color _greenColor = new Color(0.32f, 0.71f, 0.6f);

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
            UISignals.Instance.OnChangeMenuIconColor += OnChangeMenuIconColor;
            UISignals.Instance.OnChangeHeaderText += OnChangeHeaderText;
        }

        private void OnSwitchMenu(MenuTypes type)
        {
            switch (type)
            {
                case MenuTypes.TodayMenu:
                    gamesUI.gameObject.SetActive(false);
                    testUI.gameObject.SetActive(false);
                    todayUI.gameObject.SetActive(true);
                    OnChangeMenuIconColor(MenuTypes.TodayMenu);
                    break;
                case MenuTypes.GameMenu:
                    todayUI.gameObject.SetActive(false);
                    testUI.gameObject.SetActive(false);
                    gamesUI.gameObject.SetActive(true);
                    OnChangeMenuIconColor(MenuTypes.GameMenu);
                    break;
                case MenuTypes.TestMenu:
                    gamesUI.gameObject.SetActive(false);
                    todayUI.gameObject.SetActive(false);
                    testUI.gameObject.SetActive(true);
                    OnChangeMenuIconColor(MenuTypes.TestMenu);
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

        private void OnChangeMenuIconColor(MenuTypes type)
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

        private void OnChangeHeaderText(MenuTypes type)
        {
            switch (type)
            {
                case MenuTypes.GameMenu:
                    headerText.GetComponent<TextMeshProUGUI>().text = "Games";
                    break;
                case MenuTypes.TodayMenu:
                    headerText.GetComponent<TextMeshProUGUI>().text = "Home";
                    break;
                case MenuTypes.TestMenu:
                    headerText.GetComponent<TextMeshProUGUI>().text = "Tests";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnSwitchMenu -= OnSwitchMenu;
            UISignals.Instance.OnChangeMenuIconColor -= OnChangeMenuIconColor;
            UISignals.Instance.OnChangeHeaderText -= OnChangeHeaderText;
        }

        #endregion
    }
}
