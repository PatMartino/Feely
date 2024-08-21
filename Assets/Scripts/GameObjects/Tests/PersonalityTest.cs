using System.Collections.Generic;
using Data;
using Data.Tests;
using Interfaces;
using UnityEngine;

namespace GameObjects.Tests
{
    public class PersonalityTest : MonoBehaviour, ITest
    {
        [SerializeField] private string testName;
        [SerializeField] private List<PersonalityTestQuestion> questions;
        private TestResult _result;
        private int _scoreEToI;
        private int _scoreIToO;
        private int _scoreTToF;
        private int _scoreJToP;
        private int _scoreAToT;
        
        private uint _currentQuestionIndex;
        private byte _selectedAnswerIndex;
        
        public void ResetTest()
        {
            _currentQuestionIndex = 0;
            _scoreEToI = 0;
            _scoreIToO = 0;
            _scoreTToF = 0;
            _scoreJToP = 0;
            _scoreAToT = 0;
        }

        public void SelectAnswer(byte answerIndex)
        {
            _selectedAnswerIndex = answerIndex;
        }

        public bool ConfirmAnswer()
        {
            _scoreEToI += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].pointEToI;
            _scoreIToO += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].pointIToO;
            _scoreTToF += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].pointTToF;
            _scoreJToP += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].pointJToP;
            _scoreAToT += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].pointAToT;

            return CheckIsLastQuestion();
        }

        private bool CheckIsLastQuestion()
        {
            _currentQuestionIndex++;
            if (_currentQuestionIndex < questions.Count)
            {
                return false;
            }

            return true;
        }

        public string GetTextName()
        {
            return testName;
        }
        
        public Question GetNextQuestion()
        {
            return questions[(int)_currentQuestionIndex];
        }

        public List<string> GetAnswers()
        {
            return questions[(int)_currentQuestionIndex].answers.ConvertAll(answer => answer.answerText);
        }

        public TestResult GetResults()
        {
            return _result;
        }
        
        public uint GetNextQuestionIndex()
        {
            return _currentQuestionIndex;
        }

        public int GetQuestionsCount()
        {
            return questions.Count;
        }
    }
}
