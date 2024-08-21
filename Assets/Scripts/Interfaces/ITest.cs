using System.Collections.Generic;
using Data;
using Data.Tests;

namespace Interfaces
{
    public interface ITest
    {
        public void ResetTest();
        public void SelectAnswer(byte answerIndex);
        public bool ConfirmAnswer();
        
        public string GetTextName();
        public Question GetNextQuestion();
        public List<string> GetAnswers();
        public TestResult GetResults();
        public uint GetNextQuestionIndex();
        public int GetQuestionsCount();
    }
}
