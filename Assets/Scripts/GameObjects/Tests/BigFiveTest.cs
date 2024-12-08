using System.Collections.Generic;
using AbstractClasses;
using Data.Tests;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Tests
{
    public class BigFiveTest : TestBase
    {
        [SerializeField] private List<BigFiveTestResult> results;
        
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Image questionImage;
        [SerializeField] private GameObject answers;
        [SerializeField] private TMP_Text resultTitle;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private BigFiveTestTrait traitPentagon;
        
        private List<BigFiveTestAnswer> _answers = new List<BigFiveTestAnswer>(); 
        
        private int _scoreOpenness;
        private int _scoreConscientiousness;
        private int _scoreExtraversion;
        private int _scoreAgreeableness;
        private int _scoreNeuroticism;
        
        
        public override void ResetTest()
        {
            base.ResetTest();
            _scoreOpenness = 0;
            _scoreConscientiousness = 0;
            _scoreExtraversion = 0;
            _scoreAgreeableness = 0;
            _scoreNeuroticism = 0;
            _answers.Clear();
        }

        public override bool ConfirmAnswer()
        {
            BigFiveTestQuestion currentQuestion = (BigFiveTestQuestion)questions[(int)CurrentQuestionIndex];
            BigFiveTestAnswer selectedAnswer = currentQuestion.answers[SelectedAnswerIndex];

            _answers.Add(selectedAnswer);
            _scoreOpenness += selectedAnswer.pointOpenness;
            _scoreConscientiousness += selectedAnswer.pointConscientiousness;
            _scoreExtraversion += selectedAnswer.pointExtraversion;
            _scoreAgreeableness += selectedAnswer.pointAgreeableness;
            _scoreNeuroticism += selectedAnswer.pointNeuroticism;
            
            return base.ConfirmAnswer();
        }

        public override void RemoveAnswer()
        {
            var lastPoint = _answers[(int)CurrentQuestionIndex - 1];
            
            _scoreOpenness -= lastPoint.pointOpenness;
            _scoreConscientiousness -= lastPoint.pointConscientiousness;
            _scoreExtraversion -= lastPoint.pointExtraversion;
            _scoreAgreeableness -= lastPoint.pointAgreeableness;
            _scoreNeuroticism -= lastPoint.pointNeuroticism;
            
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
            
            BigFiveTestQuestion currentQuestion = (BigFiveTestQuestion)questions[(int)CurrentQuestionIndex];
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
            resultPanel.SetActive(true);
            
            traitPentagon.SetTraitIndicator(_scoreOpenness, _scoreConscientiousness, _scoreExtraversion, _scoreAgreeableness, _scoreNeuroticism);
        }
        
        protected override void SetFinalResult()
        {
            if (FinalResult == null) FinalResult = ScriptableObject.CreateInstance<TestResult>();

            var maxScore = Mathf.Max(_scoreOpenness, _scoreConscientiousness, _scoreExtraversion, _scoreAgreeableness, _scoreNeuroticism);

            string finalResultString = "";
            
            if (maxScore == _scoreOpenness) finalResultString = "Openness";
            else if (maxScore == _scoreConscientiousness) finalResultString = "Conscientiousness";
            else if (maxScore == _scoreExtraversion) finalResultString = "Extraversion";
            else if (maxScore == _scoreAgreeableness) finalResultString = "Agreeableness";
            else if (maxScore == _scoreNeuroticism) finalResultString = "Neuroticism";
            
            foreach (BigFiveTestResult result in results)
            {
                if (result.personalityType == finalResultString)
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
