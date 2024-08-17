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
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(0).gameObject.SetActive(true);
                    ChangeMenuIconColor(MenuTypes.TodayMenu);
                    break;
                case MenuTypes.GameMenu:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    ChangeMenuIconColor(MenuTypes.GameMenu);
                    break;
                case MenuTypes.TestMenu:
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                    ChangeMenuIconColor(MenuTypes.TestMenu);
                    break;
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
