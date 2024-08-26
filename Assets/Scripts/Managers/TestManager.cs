using AbstractClasses;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class TestManager : MonoSingleton<TestManager>
    {
        [SerializeField] GameObject testMenu;
        [SerializeField] TMP_Text progressText;
        [SerializeField] Image fillImage;
        [SerializeField] TMP_Text titleText;
        [SerializeField] Image confirmButtonImage;
        [SerializeField] Image previousButtonImage;
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
            confirmButtonImage.color = new Color(1,1,1,1);
            _currentTest.SelectAnswer(answerIndex);
        }
        
        public void GoNextQuestion()
        {
            if (!_isAnswerSelected) return;
            confirmButtonImage.color = new Color(1,1,1,0.5f);
            
            _isAnswerSelected = false;

            if (_currentTest.ConfirmAnswer())
            {
                previousButtonImage.color = new Color(1,1,1,0.5f);
            }
            else
            {
                previousButtonImage.color = new Color(1,1,1,1);
            }
            DrawProgressBar();
        }
        
        public void GoPreviousQuestion()
        {
            if (_currentTest.GetNextQuestionIndex() <= 0 ||
                _currentTest.GetNextQuestionIndex() >= _currentTest.GetQuestionsCount()) return;
            
            if (_currentTest.GetNextQuestionIndex() == 1) previousButtonImage.color = new Color(1,1,1,0.5f);
            _currentTest.RemoveAnswer();
            DrawProgressBar();
        }
    }
}
