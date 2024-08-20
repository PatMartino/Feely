using System.Collections.Generic;
using Data;
using Interfaces;
using UnityEngine;

namespace GameObjects
{
    public class ClassicTest : MonoBehaviour, ITest
    {
        [SerializeField] private string testName;
        [SerializeField] private List<ClassicTestQuestion> questions;
        private TestResult _result;
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
            _score += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].isCorrectAnswer ? 1 : 0;
            
            
            if (_currentQuestionIndex < questions.Count-1)
            {
                _currentQuestionIndex++;
                return false;
            }
            return true;
        }

        public Question GetNextQuestion()
        {
            return questions[(int)_currentQuestionIndex];
        }

        public uint GetNextQuestionIndex()
        {
            return _currentQuestionIndex;
        }
        
        public int GetQuestionsCount()
        {
            return questions.Count;
        }
        public List<string> GetAnswers()
        {
            return questions[(int)_currentQuestionIndex].answers.ConvertAll(answer => answer.answerText);
        }

        public TestResult GetResults()
        {
            return _result;
        }

        public string GetTextName()
        {
            return testName;
        }
    }
}
