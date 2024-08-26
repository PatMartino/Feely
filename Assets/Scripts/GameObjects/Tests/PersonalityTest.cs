using System;
using System.Collections.Generic;
using AbstractClasses;
using Data.Tests;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Tests
{
    public class PersonalityTest : TestBase
    {
        [SerializeField] private List<PersonalityTestResult> results;
        
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Image questionImage;
        [SerializeField] private GameObject answers;
        [SerializeField] private TMP_Text resultTitle;
        [SerializeField] private TMP_Text resultText;
        
        private List<PersonalityTestAnswer> _answers = new List<PersonalityTestAnswer>(); 
        
        private int _scoreEToI;
        private int _scoreNToS;
        private int _scoreTToF;
        private int _scoreJToP;
        private int _scoreAToT;
        
        private char _typeEOrI;
        private char _typeNOrS;
        private char _typeTOrF;
        private char _typeJOrP;
        private char _typeAOrT;
        
        public override void ResetTest()
        {
            base.ResetTest();
            resultTitle.enabled = false;
            resultText.enabled = false;
            _scoreEToI = 0;
            _scoreNToS = 0;
            _scoreTToF = 0;
            _scoreJToP = 0;
            _scoreAToT = 0;
            _answers.Clear();
        }

        public override bool ConfirmAnswer()
        {
            PersonalityTestQuestion currentQuestion = (PersonalityTestQuestion)questions[(int)CurrentQuestionIndex];
            PersonalityTestAnswer selectedAnswer = currentQuestion.answers[SelectedAnswerIndex];

            _answers.Add(selectedAnswer);
            _scoreEToI += selectedAnswer.pointEToI;
            _scoreNToS += selectedAnswer.pointNToS;
            _scoreJToP += selectedAnswer.pointJToP;
            _scoreTToF += selectedAnswer.pointTToF;
            _scoreAToT += selectedAnswer.pointAToT;
            
            return base.ConfirmAnswer();
        }

        public override void RemoveAnswer()
        {
            var lastPoint = _answers[(int)CurrentQuestionIndex - 1];
            
            _scoreEToI += lastPoint.pointEToI;
            _scoreNToS += lastPoint.pointNToS;
            _scoreJToP += lastPoint.pointJToP;
            _scoreTToF += lastPoint.pointTToF;
            _scoreAToT += lastPoint.pointAToT;
            
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
            
            PersonalityTestQuestion currentQuestion = (PersonalityTestQuestion)questions[(int)CurrentQuestionIndex];
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
            if (FinalResult == null) FinalResult = ScriptableObject.CreateInstance<TestResult>();
            
            _typeEOrI = _scoreEToI < 0 ? 'E' : 'I';
            _typeNOrS = _scoreNToS < 0 ? 'N' : 'S';
            _typeTOrF = _scoreTToF < 0 ? 'T' : 'F';
            _typeJOrP = _scoreJToP < 0 ? 'J' : 'P';
            _typeAOrT = _scoreAToT < 0 ? 'A' : 'T';
            
            foreach (PersonalityTestResult result in results)
            {
                if (Char.ToUpper(result.typeEOrI) == _typeEOrI &&
                    Char.ToUpper(result.typeNOrS) == _typeNOrS &&
                    Char.ToUpper(result.typeTOrF) == _typeTOrF &&
                    Char.ToUpper(result.typeJOrP) == _typeJOrP &&
                    Char.ToUpper(result.typeAOrT) == _typeAOrT)
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
