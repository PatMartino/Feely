using System;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Games.BallSort
{
    public class BallSortManager : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Transform levelHolder;

        #endregion
        #region Private Field

        private bool _isSelect;
        private int _levelID;
        private int _levelType;
        private GameObject _ball;
        private TubeData _tubeData;
        private bool _isLevelFinished;
        private int _tubeAmount;
        private int _completedTubes;
        private bool _canDrop;
        private List<GameObject> _tubeList;
        private List<GameObject> _ballPlaces;

        #endregion

        #region OnEnable, Start, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
            LoadLevel();
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
            BallSortSignals.Instance.OnGetIsSelect += OnGetIsSelect;
            BallSortSignals.Instance.OnSetIsSelectTrue += OnSetIsSelectTrue;
            BallSortSignals.Instance.OnSetIsSelectFalse += OnSetIsSelectFalse;
            BallSortSignals.Instance.OnAssignSelectBall += OnAssignSelectBall;
            BallSortSignals.Instance.OnGetBall += OnGetBall;
            BallSortSignals.Instance.OnIncreaseCompletedTubes += OnIncreaseCompletedTubes;
            BallSortSignals.Instance.OnGetLevelID += OnGetLevelID;
            BallSortSignals.Instance.OnGetIsLevelFinished += OnGetIsLevelFinished;
            BallSortSignals.Instance.OnRestartLevel += OnRestartLevel;
            BallSortSignals.Instance.OnNextLevel += OnNextLevel;
            BallSortSignals.Instance.OnGetPreviousTubeList += OnGetPreviousTubeList;
            BallSortSignals.Instance.OnGetPreviousBallPlaces+= OnGetPreviousBallPlaces;
            BallSortSignals.Instance.OnGetLevelType += OnGetLevelType;
            BallSortSignals.Instance.OnActivateGame += OnActivateGame;
            BallSortSignals.Instance.OnDeActivateGame += OnDeActivateGame;
        }

        private void LoadLevel()
        {
            _levelID = ES3.KeyExists("BallSortLevelID") ? ES3.Load<int>("BallSortLevelID") : 1;
        }

        private void LevelLoader()
        {
            RandomLevelGenerator();
            Instantiate(Resources.Load<GameObject>($"Games/BallSort/LevelPrefabs/{_levelType}"),levelHolder);
            _tubeData = Resources.Load<TubeData>($"Games/BallSort/TubeData/{_levelID}");
            //_tubeAmount = _tubeData.TubeAmount;
            _tubeAmount = _levelType - 2;
            BallSortSignals.Instance.OnGenerateLevel?.Invoke();
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
            ES3.Save("BallSortLevelID", _levelID);
            ResetAmounts();
            LevelLoader();
            UISignals.Instance.OnStartBallSortLevel?.Invoke();
            _isLevelFinished = false;
        }

        private void OnRestartLevel()
        {
            //LevelDestroyer();
            ResetAmounts();
            BallSortSignals.Instance.OnClearLevel?.Invoke();
            BallSortSignals.Instance.OnAssignBallsToTubes?.Invoke();
            //LevelLoader();
        }

        private void RandomLevelGenerator()
        {
            var level = Random.Range(5, 9);
            _levelType = level;
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

        private void OnActivateGame()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        private void OnDeActivateGame()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void ResetAmounts()
        {
            _completedTubes = 0;
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

        private void OnAssignSelectBall(GameObject obj, List<GameObject> tubelist, List<GameObject> _ballPlace)
        {
            _ball = obj;
            _tubeList = tubelist;
            _ballPlaces = _ballPlace;
        }

        private List<GameObject> OnGetPreviousTubeList()
        {
            return _tubeList;
        }

        private List<GameObject> OnGetPreviousBallPlaces()
        {
            return _ballPlaces;
        }

        private int OnGetLevelType()
        {
            return _levelType;
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
            BallSortSignals.Instance.OnGetIsSelect -= OnGetIsSelect;
            BallSortSignals.Instance.OnSetIsSelectTrue -= OnSetIsSelectTrue;
            BallSortSignals.Instance.OnSetIsSelectFalse -= OnSetIsSelectFalse;
            BallSortSignals.Instance.OnAssignSelectBall -= OnAssignSelectBall;
            BallSortSignals.Instance.OnGetBall -= OnGetBall;
            BallSortSignals.Instance.OnIncreaseCompletedTubes -= OnIncreaseCompletedTubes;
            BallSortSignals.Instance.OnGetLevelID -= OnGetLevelID;
            BallSortSignals.Instance.OnGetIsLevelFinished -= OnGetIsLevelFinished;
            BallSortSignals.Instance.OnRestartLevel -= OnRestartLevel;
            BallSortSignals.Instance.OnNextLevel -= OnNextLevel;
            BallSortSignals.Instance.OnGetPreviousTubeList -= OnGetPreviousTubeList;
            BallSortSignals.Instance.OnGetPreviousBallPlaces -= OnGetPreviousBallPlaces;
            
            BallSortSignals.Instance.OnGetLevelType -= OnGetLevelType;
            BallSortSignals.Instance.OnActivateGame -= OnActivateGame;
            BallSortSignals.Instance.OnDeActivateGame -= OnDeActivateGame;
        }

        #endregion
    }
}
