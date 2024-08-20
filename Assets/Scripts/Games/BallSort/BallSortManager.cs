using System;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Games.BallSort
{
    public class BallSortManager : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Transform levelHolder;

        #endregion
        #region Private Field

        private bool _isSelect;
        private int _levelID =1;
        private GameObject _ball;
        private TubeData _tubeData;
        
        private int _tubeAmount;
        private int _completedTubes;

        #endregion

        #region OnEnable, Start, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
            LevelLoader();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Function

        private void SubscribeEvents()
        {
            GameSignals.Instance.OnGetIsSelect += OnGetIsSelect;
            GameSignals.Instance.OnSetIsSelectTrue += OnSetIsSelectTrue;
            GameSignals.Instance.OnSetIsSelectFalse += OnSetIsSelectFalse;
            GameSignals.Instance.OnAssignSelectBall += OnAssignSelectBall;
            GameSignals.Instance.OnGetBall += OnGetBall;
            GameSignals.Instance.OnIncreaseCompletedTubes += OnIncreaseCompletedTubes;
        }

        private void LevelLoader()
        {
            Instantiate(Resources.Load<GameObject>($"Games/BallSort/LevelPrefabs/Level{_levelID}"),levelHolder);
            _tubeData = Resources.Load<TubeData>($"Games/BallSort/TubeData/{_levelID}");
            _tubeAmount = _tubeData.TubeAmount;
        }

        private void LevelDestroyer()
        {
            Destroy(levelHolder.GetChild(0).gameObject);
        }

        private void LevelComplete()
        {
            
        }

        private void NextLevel()
        {
            LevelDestroyer();
            _levelID++;
            ResetAmounts();
            LevelLoader();
        }

        private void OnIncreaseCompletedTubes()
        {
            _completedTubes++;
            CheckLevelFinish();
        }

        private void CheckLevelFinish()
        {
            if (_completedTubes>=_tubeAmount)
            {
                Debug.LogWarning("LevelComplete");
                NextLevel();
            }
        }

        private void ResetAmounts()
        {
            _completedTubes = 0;
            _tubeAmount = 0;
        }

        private bool OnGetIsSelect()
        {
            return _isSelect;
        }

        private void OnSetIsSelectTrue()
        {
            _isSelect = true;
        }
        
        private void OnSetIsSelectFalse()
        {
            _isSelect = false;
        }

        private void OnAssignSelectBall(GameObject obj)
        {
            _ball = obj;
        }

        private GameObject OnGetBall()
        {
            return _ball;
        }

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.OnGetIsSelect -= OnGetIsSelect;
            GameSignals.Instance.OnSetIsSelectTrue -= OnSetIsSelectTrue;
            GameSignals.Instance.OnSetIsSelectFalse -= OnSetIsSelectFalse;
            GameSignals.Instance.OnAssignSelectBall -= OnAssignSelectBall;
            GameSignals.Instance.OnGetBall -= OnGetBall;
            GameSignals.Instance.OnIncreaseCompletedTubes -= OnIncreaseCompletedTubes;
        }

        #endregion
    }
}
