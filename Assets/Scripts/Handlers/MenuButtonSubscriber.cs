using System;
using Enums;
using Managers;
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

        private void OnDisable()
        {
            UnSubscribeEvents();
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
                case MenuButtonType.IqTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/IQ Test/IQTest")));
                    break;
                case MenuButtonType.AnxietyTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Anxiety Test/AnxietyTest")));
                    break;
                case MenuButtonType.PersonalityTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Personality Test/PersonalityTest")));
                    break;
                case MenuButtonType.SociabilityTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Sociability (Ryakhovsky) Test/SociabilityTest")));
                    break;
                case MenuButtonType.BigFiveTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/BigFive Test/BigFiveTest")));
                    break;
                case MenuButtonType.TrashSortGame:
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnGameManagement?.Invoke(GamesAndTestsNames.TrashSort));
                    break;
                case MenuButtonType.TrashSortCoverPage:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.TrashSortCoverPage));
                    break;
                case MenuButtonType.MathFundamentalGame:
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnGameManagement?.Invoke(GamesAndTestsNames.MathFundamental));
                    break;
                case MenuButtonType.MathFundamentalCoverPage:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.MathFundamentalCoverPage));
                    break;
                case MenuButtonType.AggressionTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Aggression (Assinger) Test/AggressionTest")));
                    break;
                case MenuButtonType.CapitalTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Capital Test/Capital Test")));
                    break;
                case MenuButtonType.EmotionalTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Emotional Stability (Neuroticism) Test/EmotionalStabilityTest")));
                    break;
                case MenuButtonType.OptimistTest:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Optimist-Pessimist Test/OptimistPessimistTest")));
                    break;
                case MenuButtonType.SocialIntelligence:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.Test));
                    _button.onClick.AddListener(() => TestManager.Instance.StartTest(Resources.Load<GameObject>("Test/Social Intelligence Test/SocialIntelligenceTest")));
                    break;
                case MenuButtonType.CalculationResultCoverPage:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.CalculationResultCoverPage));
                    break;
                case MenuButtonType.CalculationResultGame:
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnGameManagement?.Invoke(GamesAndTestsNames.CalculationResult));
                    break;
                case MenuButtonType.GhostMemoryCoverPage:
                    _button.onClick.AddListener(() => UISignals.Instance.OnMenuUIManagement?.Invoke(UIStates.GhostMemoryCoverPage));
                    break;
                case MenuButtonType.GhostMemoryGame:
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnGameManagement?.Invoke(GamesAndTestsNames.GhostMemory));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnSubscribeEvents()
        {
            _button.onClick.RemoveAllListeners();
        }

        #endregion
    }
}