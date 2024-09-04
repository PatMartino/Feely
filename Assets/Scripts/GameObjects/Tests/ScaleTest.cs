using System.Collections.Generic;
using AbstractClasses;
using Data.Tests;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Tests
{
    public class ScaleTest : TestBase
    {
        [SerializeField] private List<ScaleTestResult> results;
        
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Image questionImage;
        [SerializeField] private GameObject answers;
        [SerializeField] private TMP_Text resultTitle;
        [SerializeField] private TMP_Text resultText;
        
        protected List<int> Points = new List<int>();
        protected int Score;
        
        public override void ResetTest()
        {
            base.ResetTest();
            resultTitle.enabled = false;
            resultText.enabled = false;
            Points.Clear();
            Score = 0;
        }
        
        public override bool ConfirmAnswer()
        {
            ScaleTestQuestion currentQuestion = (ScaleTestQuestion)questions[(int)CurrentQuestionIndex];
            ScaleTestAnswer selectedAnswer = currentQuestion.answers[SelectedAnswerIndex];
            
            Points.Add(selectedAnswer.scorePoint);
            Score += selectedAnswer.scorePoint;
            return base.ConfirmAnswer();
        }

        public override void RemoveAnswer()
        {
            var lastPoint = Points[(int)CurrentQuestionIndex - 1];
            Score -= lastPoint;
            Points.RemoveAt((int)CurrentQuestionIndex - 1);
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
            
            ScaleTestQuestion currentQuestion = (ScaleTestQuestion)questions[(int)CurrentQuestionIndex];
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
            foreach (ScaleTestResult result in results)
            {
                if (result.range.x <= Score && Score <= result.range.y)
                {
                    var correctResult = result.result;
                    
                    FinalResult.resultTitle = correctResult.resultTitle;
                    FinalResult.result = correctResult.result;
                    break;
                }
            }
        }
    }
}
