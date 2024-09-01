using System.Collections.Generic;
using Data.Tests;
using UnityEngine;

namespace AbstractClasses
{
    public abstract class TestBase : MonoBehaviour
    {
        [SerializeField] private string testName;
        
        [SerializeField] protected List<Question> questions;
        
        protected TestResult FinalResult;
        protected uint CurrentQuestionIndex;
        protected byte SelectedAnswerIndex;
        
        public virtual void ResetTest()
        {
            CurrentQuestionIndex = 0;
            DrawNecessaryUI();
        }

        public void SelectAnswer(byte answerIndex)
        {
            SelectedAnswerIndex = answerIndex;
        }
        
        public virtual bool ConfirmAnswer()
        {
            CurrentQuestionIndex++;
            DrawNecessaryUI();
            return CheckIsLastQuestion();
        }

        public virtual void PauseTest()
        {
            
        }
        public virtual void UnpauseTest()
        {
            
        }

        public virtual void RemoveAnswer()
        {
            CurrentQuestionIndex--;
            DrawNecessaryUI();
        }

        public string GetTextName()
        {
            return testName;
        }
        
        public uint GetNextQuestionIndex()
        {
            return CurrentQuestionIndex;
        }

        public int GetQuestionsCount()
        {
            return questions.Count;
        }

        private void DrawNecessaryUI()
        {
            if (!CheckIsLastQuestion()) DrawQuestionUI();
            else DrawResultUI();
        }

        protected virtual void DrawQuestionUI()
        {
        }
        
        protected virtual void DrawResultUI()
        {
        }
        
        private bool CheckIsLastQuestion()
        {
            if (CurrentQuestionIndex < questions.Count)
            {
                return false;
            }
            return true;
        }

        protected virtual void SetFinalResult()
        {
            if (FinalResult == null) FinalResult = ScriptableObject.CreateInstance<TestResult>();
        }
    }
}
