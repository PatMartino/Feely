using AbstractClasses;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class TestManager : MonoSingleton<TestManager>
    {
        [SerializeField] GameObject testMenu;
        [SerializeField] TMP_Text progressText;
        [SerializeField] Image fillImage;
        [SerializeField] TMP_Text titleText;
        [SerializeField] Button confirmButton;
        [SerializeField] Button previousButton;
        private TestBase _currentTest;

        private bool _isAnswerSelected;

        public UnityAction<int> OnAnswerSelected;

        protected override void Awake()
        {
            base.Awake();
            _currentTest = GetComponentInChildren<TestBase>();
        }

        private void Start()
        {
            StartTest();
        }

        public void StartTest()
        {
            testMenu.SetActive(true);
            _currentTest.ResetTest();
            titleText.text = _currentTest.GetTextName();
            DrawProgressBar();
        }

        public void EndTest()
        {
            testMenu.SetActive(false);
        }

        private void DrawProgressBar()
        {
            progressText.text = $"Question {_currentTest.GetNextQuestionIndex()}/{_currentTest.GetQuestionsCount()}";
            fillImage.fillAmount = (float)_currentTest.GetNextQuestionIndex() / _currentTest.GetQuestionsCount();
        }
        
        public void SelectAnswer(byte answerIndex)
        {
            _isAnswerSelected = true;
            confirmButton.interactable = true;

            _currentTest.SelectAnswer(answerIndex);
        }
        
        public void GoNextQuestion()
        {
            if (!_isAnswerSelected) return;
            confirmButton.interactable = false;
            
            _isAnswerSelected = false;

            if (_currentTest.ConfirmAnswer())
            {
                previousButton.interactable = false;
            }
            else
            {
                previousButton.interactable = true;
            }
            DrawProgressBar();
        }
        
        public void GoPreviousQuestion()
        {
            if (_currentTest.GetNextQuestionIndex() <= 0 ||
                _currentTest.GetNextQuestionIndex() >= _currentTest.GetQuestionsCount()) return;

            if (_currentTest.GetNextQuestionIndex() == 1) previousButton.interactable = false;
            _currentTest.RemoveAnswer();
            DrawProgressBar();
        }
    }
}
