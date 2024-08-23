using System;
using System.Collections.Generic;
using Data.Tests;
using Interfaces;
using Structs;
using UnityEngine;

namespace GameObjects.Tests
{
    public class PersonalityTest : MonoBehaviour, ITest
    {
        [SerializeField] private string testName;
        [SerializeField] private List<PersonalityTestQuestion> questions;
        [SerializeField] private List<PersonalityTestResult> results;
        private TestResult _finalResult;
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
        
        private uint _currentQuestionIndex;
        private byte _selectedAnswerIndex;
        
        public void ResetTest()
        {
            _currentQuestionIndex = 0;
            _scoreEToI = 0;
            _scoreNToS = 0;
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
            _scoreNToS += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].pointNToS;
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
            return GetFinalResult();
        }

        public uint GetNextQuestionIndex()
        {
            return _currentQuestionIndex;
        }

        public int GetQuestionsCount()
        {
            return questions.Count;
        }
        
        private TestResult GetFinalResult()
        {
            if (_finalResult == null) _finalResult = ScriptableObject.CreateInstance<TestResult>();
            
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
                    _finalResult.result = result.result.result;
                }
            }

            return _finalResult;
        }
    }
}
