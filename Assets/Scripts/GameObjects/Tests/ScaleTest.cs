using System.Collections.Generic;
using Data.Tests;
using Interfaces;
using Structs;
using UnityEngine;

namespace GameObjects.Tests
{
    public class ScaleTest : MonoBehaviour, ITest
    {
        [SerializeField] private string testName;
        [SerializeField] private List<ScaleTestQuestion> questions;
        [SerializeField] private List<ScaleTestResult> results;
        [SerializeField] private TestResult finalResult;
        private int _score;
        
        private uint _currentQuestionIndex;
        private byte _selectedAnswerIndex;
        
        public void ResetTest()
        {
            _currentQuestionIndex = 0;
            _score = 0;
        }

        public void SelectAnswer(byte answerIndex)
        {
            _selectedAnswerIndex = answerIndex;
        }
        
        public bool ConfirmAnswer()
        {
            _score += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].scorePoint;
            return CheckIsLastQuestion();
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
        
        private bool CheckIsLastQuestion()
        {
            _currentQuestionIndex++;
            if (_currentQuestionIndex < questions.Count)
            {
                return false;
            }

            return true;
        }

        private TestResult GetFinalResult()
        {
            if (finalResult == null) finalResult = ScriptableObject.CreateInstance<TestResult>();
            foreach (ScaleTestResult result in results)
            {
                if (result.range.x <= _score && _score <= result.range.y)
                {
                    finalResult.result = result.result.result;
                    
                    finalResult.result += "\n\n" + _score + " points.";
                }
            }

            return finalResult;
        }
    }
}
