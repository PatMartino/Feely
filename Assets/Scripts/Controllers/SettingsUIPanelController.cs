using System;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers
{
    public class SettingsUIPanelController : MonoBehaviour
    {
        #region Serialize Field

        [Header("UI Panels")]
        [SerializeField] private GameObject ageUIPanel;
        [FormerlySerializedAs("flagUIPanel")] [SerializeField] private GameObject languageUIPanel;

        [Header("Images")] 
        [SerializeField] private Image soundOpenImage;
        [SerializeField] private Image soundCloseImage;
        [SerializeField] private Image darkThemeOpenImage;
        [SerializeField] private Image darkThemeCloseImage;

        #endregion

        #region Private Field
        
        private bool _sound =true;
        private bool _darkTheme;

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
            UISignals.Instance.OnSettingsPanelController += OnSettingsPanelController;
        }

        private void OnSettingsPanelController(SettingButtonsType type)
        {
            switch (type)
            {
                case SettingButtonsType.Age:
                    ageUIPanel.SetActive(true);
                    break;
                case SettingButtonsType.Sound:
                    if (_sound)
                    {
                        _sound = false;
                        soundOpenImage.gameObject.SetActive(false);
                        soundCloseImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        _sound = true;
                        soundOpenImage.gameObject.SetActive(true);
                        soundCloseImage.gameObject.SetActive(false);
                    }
                    break;
                case SettingButtonsType.DarkTheme:
                    if (_darkTheme)
                    {
                        _darkTheme = false;
                        darkThemeOpenImage.gameObject.SetActive(false);
                        darkThemeCloseImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        _darkTheme = true;
                        darkThemeOpenImage.gameObject.SetActive(true);
                        darkThemeCloseImage.gameObject.SetActive(false);
                    }
                    break;
                case SettingButtonsType.Language:
                    languageUIPanel.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnSettingsPanelController -= OnSettingsPanelController;
        }

        #endregion
    }
}
