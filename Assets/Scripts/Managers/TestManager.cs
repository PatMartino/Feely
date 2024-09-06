using AbstractClasses;
using DG.Tweening;
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
        [SerializeField] GameObject testHolder;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] TMP_Text progressText;
        [SerializeField] Image fillImage;
        [SerializeField] TMP_Text titleText;
        [SerializeField] Button confirmButton;
        [SerializeField] Button previousButton;
        [SerializeField] Button endTestButton;
        private TestBase _currentTest;

        private bool _isAnswerSelected;

        public UnityAction<int> OnAnswerSelected;

        private void Start()
        {
            StartTest(Resources.Load<GameObject>("Test/Personality Test/PersonalityTest"));
        }

        public void StartTest(GameObject test)
        {
            _currentTest = Instantiate(test, testHolder.transform).GetComponent<TestBase>();
            testMenu.SetActive(true);
            pauseMenu.SetActive(false);
            _currentTest.ResetTest();
            titleText.text = _currentTest.GetTextName();
            endTestButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
            DrawProgressBar();
            
            testMenu.transform.DOMoveY(transform.position.y, 1f).SetEase(Ease.OutQuint);
        }

        public void EndTest()
        {
            testMenu.transform.DOMoveY(-transform.position.y, 1f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                testMenu.SetActive(false);
                pauseMenu.SetActive(false);
                foreach (Transform go in testHolder.transform)
                {
                    Destroy(go.gameObject);
                }
            });
            
            foreach (Image image in testMenu.GetComponentsInChildren<Image>())
            {
                var tween = image.DOFade(0, 1f).SetEase(Ease.InQuint);
                tween.OnComplete(() => tween.Rewind());
            }

            foreach (TMP_Text text in testMenu.GetComponentsInChildren<TMP_Text>())
            {
                var tween = text.DOFade(0, 1f).SetEase(Ease.InQuint);
                tween.OnComplete(() => tween.Rewind());
            }
        }

        public void PauseTest()
        {
            pauseMenu.SetActive(true);
            _currentTest.PauseTest();
            pauseMenu.GetComponentInChildren<Image>().DOFade(1, 0.5f).SetEase(Ease.Linear).From(0);
        }

        public void UnpauseTest()
        {
            pauseMenu.SetActive(false);
            _currentTest.UnpauseTest();
        }

        private void DrawProgressBar()
        {
            progressText.text = $"Question {_currentTest.GetNextQuestionIndex()}/{_currentTest.GetQuestionsCount()}";
            var ratio = (float)_currentTest.GetNextQuestionIndex() / _currentTest.GetQuestionsCount();
            fillImage.DOFillAmount(ratio, 0.5f).SetEase(Ease.OutCirc);
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
                endTestButton.gameObject.SetActive(true);
                confirmButton.gameObject.SetActive(false);
                previousButton.gameObject.SetActive(false);
                titleText.text += " Result";
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
