using System.Collections.Generic;
using Data;

namespace Interfaces
{
    public interface ITest
    {
        public void ResetTest();
        
        public void SelectAnswer(byte answerIndex);
        public bool ConfirmAnswer();
        public Question GetNextQuestion();
        
        public List<string> GetAnswers();
        
        public TestResult GetResults();
        public string GetTextName();
        
        public uint GetNextQuestionIndex();
        
        public int GetQuestionsCount();
    }
}
