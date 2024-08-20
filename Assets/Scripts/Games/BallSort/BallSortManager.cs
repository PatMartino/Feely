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
        private bool _isLevelFinished;
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
            GameSignals.Instance.OnGetLevelID += OnGetLevelID;
            GameSignals.Instance.OnGetIsLevelFinished += OnGetIsLevelFinished;
            GameSignals.Instance.OnRestartLevel += OnRestartLevel;
            GameSignals.Instance.OnNextLevel += OnNextLevel;
        }

        private void LevelLoader()
        {
            Instantiate(Resources.Load<GameObject>($"Games/BallSort/LevelPrefabs/Level{_levelID}"),levelHolder);
            _tubeData = Resources.Load<TubeData>($"Games/BallSort/TubeData/{_levelID}");
            _tubeAmount = _tubeData.TubeAmount;
            UISignals.Instance.OnUpdateBallSortLevelIDText?.Invoke();
        }

        private void LevelDestroyer()
        {
            Destroy(levelHolder.GetChild(0).gameObject);
        }

        private void LevelComplete()
        {
            UISignals.Instance.OnCompleteBallSortLevel?.Invoke();
            _isLevelFinished = true;
        }

        private void OnNextLevel()
        {
            LevelDestroyer();
            _levelID++;
            ResetAmounts();
            LevelLoader();
            UISignals.Instance.OnStartBallSortLevel?.Invoke();
            _isLevelFinished = false;
        }

        private void OnRestartLevel()
        {
            LevelDestroyer();
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
                LevelComplete();
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

        private int OnGetLevelID()
        {
            return _levelID;
        }

        private GameObject OnGetBall()
        {
            return _ball;
        }

        private bool OnGetIsLevelFinished()
        {
            return _isLevelFinished;
        }

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.OnGetIsSelect -= OnGetIsSelect;
            GameSignals.Instance.OnSetIsSelectTrue -= OnSetIsSelectTrue;
            GameSignals.Instance.OnSetIsSelectFalse -= OnSetIsSelectFalse;
            GameSignals.Instance.OnAssignSelectBall -= OnAssignSelectBall;
            GameSignals.Instance.OnGetBall -= OnGetBall;
            GameSignals.Instance.OnIncreaseCompletedTubes -= OnIncreaseCompletedTubes;
            GameSignals.Instance.OnGetLevelID -= OnGetLevelID;
            GameSignals.Instance.OnGetIsLevelFinished -= OnGetIsLevelFinished;
            GameSignals.Instance.OnRestartLevel -= OnRestartLevel;
            GameSignals.Instance.OnNextLevel -= OnNextLevel;
        }

        #endregion
    }
}
