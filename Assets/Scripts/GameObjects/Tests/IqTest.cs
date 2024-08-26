using System.Collections.Generic;
using Data.Tests;
using Structs;
using TMPro;
using UnityEngine;

namespace GameObjects.Tests
{
    public class IqTest : ScaleTest
    {
        [SerializeField] private TMP_Text timerText;

        private List<float> _pointsFloat = new List<float>();
        private float _scoreFloat;
        private float _timer;

        public override void ResetTest()
        {
            _pointsFloat.Clear();
            _scoreFloat = 0;
            _timer = 0;
            base.ResetTest();
        }
        private void Update()
        {
            if (CurrentQuestionIndex >= questions.Count) return;
            _timer += Time.deltaTime;
            timerText.text = $"Time: {_timer:0.00}";
        }

        public override bool ConfirmAnswer()
        {
            ScaleTestQuestion currentQuestion = (ScaleTestQuestion)questions[(int)CurrentQuestionIndex];
            ScaleTestAnswer selectedAnswer = currentQuestion.answers[SelectedAnswerIndex];
            var lastPoint = selectedAnswer.scorePoint * 10 / (_timer + 10);
            _pointsFloat.Add(lastPoint);
            _scoreFloat += lastPoint;
            
            Score = (int)_scoreFloat;
            _timer = 0;
            return base.ConfirmAnswer();
        }
        
        public override void RemoveAnswer()
        {
            var lastPoint = _pointsFloat[(int)CurrentQuestionIndex - 1];
            _scoreFloat -= lastPoint;
            
            Score = (int)_scoreFloat;
            _timer = 0;
            Points.RemoveAt((int)CurrentQuestionIndex - 1);
            base.RemoveAnswer();
        }
    }
}
