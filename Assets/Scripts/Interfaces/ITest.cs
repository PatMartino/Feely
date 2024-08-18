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
        
        public void GetResults();
        public string GetTextName();
    }
}
