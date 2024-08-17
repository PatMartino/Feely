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
                case ButtonType.TodayButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.TodayMenu)); 
                    break;
                case ButtonType.GameButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.GameMenu)); 
                    break;
                case ButtonType.TestButton:
                    _button.onClick.AddListener(() => UISignals.Instance.OnSwitchMenu?.Invoke(MenuTypes.TestMenu)); 
                    break;
            }
        }

        #endregion
    }
}