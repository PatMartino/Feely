using System.Collections.Generic;
using Data;
using Interfaces;
using UnityEngine;

namespace GameObjects
{
    public class ScaleTest : MonoBehaviour, ITest
    {
        [SerializeField] private string testName;
        [SerializeField] private List<ScaleTestQuestion> questions;
        private int _score;
        
        private uint _currentQuestionIndex;
        private byte _selectedAnswerIndex;
        
        public void ResetTest()
        {
            _currentQuestionIndex = 0;
        }

        public void SelectAnswer(byte answerIndex)
        {
            _selectedAnswerIndex = answerIndex;
        }
        
        public bool ConfirmAnswer()
        {
            _score += questions[(int)_currentQuestionIndex].answers[_selectedAnswerIndex].scorePoint;
            if (_currentQuestionIndex < questions.Count-1)
            {
                _currentQuestionIndex++;
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

        public void GetResults()
        {
            
        }
    }
}
