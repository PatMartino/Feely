using System.Collections.Generic;
using AbstractClasses;
using Data.Tests;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Tests
{
    public class ClassicTest : TestBase
    {
        [SerializeField] private List<ClassicTestResult> results;
        
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Image questionImage;
        [SerializeField] private GameObject answers;
        [SerializeField] private TMP_Text resultTitle;
        [SerializeField] private TMP_Text resultText;
        
        private List<bool> _answers = new List<bool>();
        private int _score;
        
        public override void ResetTest()
        {
            base.ResetTest();
            resultTitle.enabled = false;
            resultText.enabled = false;
            _answers.Clear();
            _score = 0;
        }
        
        public override bool ConfirmAnswer()
        {
            ClassicTestQuestion currentQuestion = (ClassicTestQuestion)questions[(int)CurrentQuestionIndex];
            ClassicTestAnswer selectedAnswer = currentQuestion.answers[SelectedAnswerIndex];
            
            _answers.Add(selectedAnswer.isCorrectAnswer);
            _score += selectedAnswer.isCorrectAnswer ? 1 : 0;
            return base.ConfirmAnswer();
        }
        
        public override void RemoveAnswer()
        {
            var lastPoint = _answers[(int)CurrentQuestionIndex - 1];
            if (lastPoint) _score--;
            _answers.RemoveAt((int)CurrentQuestionIndex - 1);
            base.RemoveAnswer();
        }

        protected override void DrawQuestionUI()
        {
            questionText.enabled = true;
            answers.SetActive(true);
            
            questionText.text = questions[(int)CurrentQuestionIndex].question;
            if (questions[(int)CurrentQuestionIndex].image != null)
            {
                questionImage.sprite = questions[(int)CurrentQuestionIndex].image;
                questionImage.enabled = true;
            }
            else questionImage.enabled = false;
            
            ClassicTestQuestion currentQuestion = (ClassicTestQuestion)questions[(int)CurrentQuestionIndex];
            var answerList = currentQuestion.answers.ConvertAll(answer => answer.answerText);
            
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
        
        protected override void DrawResultUI()
        {
            questionText.enabled = false;
            questionImage.enabled = false;
            answers.SetActive(false);
            
            SetFinalResult();
            resultTitle.text = FinalResult.resultTitle;
            resultText.text = FinalResult.result;
            resultTitle.enabled = true;
            resultText.enabled = true;
        }
        
        protected override void SetFinalResult()
        {
            base.SetFinalResult();
            foreach (ClassicTestResult result in results)
            {
                if (result.range.x <= _score && _score <= result.range.y)
                {
                    var correctResult = result.result;
                    
                    FinalResult.resultTitle = correctResult.resultTitle;
                    FinalResult.result = correctResult.result;
                    FinalResult.result += "\n\n" + _score + "/" + GetQuestionsCount();
                    break;
                }
            }
        }
    }
}
