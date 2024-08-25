using System;
using Data.Tests;
using TMPro;
using UnityEngine;

namespace GameObjects.Tests
{
    public class IqTest : ScaleTest
    {
        [SerializeField] private TMP_Text timerText;

        private float _scoreFloat;
        private float _timer;
        private void Update()
        {
            if (CurrentQuestionIndex >= questions.Count) return;
            _timer += Time.deltaTime;
            timerText.text = $"Time: {_timer:0.00}";
        }

        public override bool ConfirmAnswer()
        {
            ScaleTestQuestion currentQuestion = (ScaleTestQuestion)questions[(int)CurrentQuestionIndex];
            _scoreFloat += currentQuestion.answers[SelectedAnswerIndex].scorePoint * 10/(_timer + 10);
            Score = (int)_scoreFloat;
            _timer = 0;
            return base.ConfirmAnswer();
        }
    }
}
