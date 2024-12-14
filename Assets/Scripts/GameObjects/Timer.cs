using System;
using Signals;
using UnityEngine;

namespace GameObjects
{
    public class Timer : MonoBehaviour
    {
        #region Private Field

        private float _remainingTime; // Kalan süre

        private bool _isRunning = false; // Timer'ın çalışıp çalışmadığını kontrol eder

        #endregion

        #region Start, Update

        void OnEnable()
        {
            SubscribeEvents();
        }

        void Update()
        {
            if (_isRunning)
            {
                _remainingTime -= Time.deltaTime;
                CardMatchSignals.Instance.OnUpdateTime?.Invoke();// Kalan süreyi güncelle

                if (_remainingTime <= 0f)
                {
                    _isRunning = false; // Timer'ı durdur
                    _remainingTime = 0f; // Kalan süreyi sıfırla
                    
                    CardMatchSignals.Instance.OnRestartLevel?.Invoke();
                    MathFundamentalSignals.Instance.OnLevelFailed?.Invoke();
                    // Timer tamamlandığında olayı tetikle
                    
                }
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnGetRemainingTime += OnGetRemainingTime;
            CoreGameSignals.Instance.OnStartTimer += OnStartTimer;
            CoreGameSignals.Instance.OnStopTimer += OnStopTimer;
            CoreGameSignals.Instance.OnContinuousTimer += OnContinuousTimer;
        }

        // Timer'ı başlatır
        private void OnStartTimer(float time)
        {
            _remainingTime = time; // Kalan süreyi timer süresi ile başlat
            _isRunning = true; // Timer'ı çalıştır
        }
        

        // Timer'ı durdurur
        private void OnStopTimer()
        {
            _isRunning = false; 
        }
        
        private void OnContinuousTimer()
        {
            _isRunning = true; 
        }

        private float OnGetRemainingTime()
        {
            return _remainingTime;
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnGetRemainingTime -= OnGetRemainingTime;
            CoreGameSignals.Instance.OnStartTimer -= OnStartTimer;
            CoreGameSignals.Instance.OnStopTimer -= OnStopTimer;
            CoreGameSignals.Instance.OnContinuousTimer -= OnContinuousTimer;
        }

        #endregion
        
    }
}
