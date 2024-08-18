using System.Collections.Generic;
using Data;
using Extensions;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TestManager : MonoSingleton<TestManager>
    {
        [SerializeField] TMP_Text testNameText;
        [SerializeField] TMP_Text questionText;
        [SerializeField] Image questionImage;
        [SerializeField] GameObject answers;
        private ITest _currentTest;

        protected override void Awake()
        {
            base.Awake();
            _currentTest = GetComponentInChildren<ITest>();
        }

        private void Start()
        {
            StartTest();
        }

        public void StartTest()
        {
            _currentTest.ResetTest();
            testNameText.text = _currentTest.GetTextName();
            DrawQuestionAndAnswers(_currentTest.GetNextQuestion(), _currentTest.GetAnswers());
        }

        private void DrawQuestionAndAnswers(Question question, List<string> answerList)
        {
            questionText.text = question.question;
            if (question.image != null)
            {
                questionImage.sprite = question.image;
                questionImage.enabled = true;
            }
            else questionImage.enabled = false;
            
            foreach (Transform go in answers.transform)
            {
                Destroy(go.gameObject);
            }
            
            for (byte i = 0; i < answerList.Count; i++)
            {
                var answer = Instantiate(Resources.Load<GameObject>("Test/Answer"), answers.transform);
                answer.GetComponentInChildren<TMP_Text>().text = answerList[i];
                answer.GetComponent<TestAnswerButton>().answerIndex = i;
            }
        }

        public void SelectAnswer(byte answerIndex)
        {
            _currentTest.SelectAnswer(answerIndex);
        }
        
        public void ConfirmAnswer()
        {
            if (!_currentTest.ConfirmAnswer())
            {
                DrawQuestionAndAnswers(_currentTest.GetNextQuestion(), _currentTest.GetAnswers());
            }
            else
            {
                _currentTest.GetResults();
            }
        }
    }
}
