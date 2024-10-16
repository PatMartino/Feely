using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameObjects.Tests
{
    public class IqTest : ScaleTest
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private int minIqScore;
        [FormerlySerializedAs("expectedEndTimeInMin")] [SerializeField] private int estimatedEndTimeInMin;
        
        private float _timer;
        private bool _isPaused;

        public override void ResetTest()
        {
            _timer = 0;
            base.ResetTest();
        }
        private void Update()
        {
            if (CurrentQuestionIndex >= questions.Count && !_isPaused) return;
            _timer += Time.deltaTime;
            
            int minutes = Mathf.FloorToInt(_timer / 60); // Dakikayı bulmak için saniyeyi 60'a böl
            int seconds = Mathf.FloorToInt(_timer % 60); // Geriye kalan saniyeleri bulmak için 60'a göre mod al
            
            timerText.text = $"Time: {minutes}:{seconds:D2}";
        }

        public override void PauseTest()
        {
            base.PauseTest();
            _isPaused = true;
        }

        public override void UnpauseTest()
        {
            base.UnpauseTest();
            _isPaused = false;
        }
        
        protected override void SetFinalResult()
        {
            var endTime = _timer / 60 /estimatedEndTimeInMin;
            var scoreFloat = Score/((endTime * endTime /4) + (endTime / 4) + 0.5f);
            Score = (int)scoreFloat + minIqScore;
            base.SetFinalResult();
            FinalResult.resultTitle += Score;
        }
    }
}
