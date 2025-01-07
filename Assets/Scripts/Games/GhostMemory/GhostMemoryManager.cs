using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Extensions;
using Signals;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Games.GhostMemory
{
    public class GhostMemoryManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> levelPrefabs;
        [SerializeField] private int difficultyPercentage;
        [SerializeField] private int difficultyIncreaseAfterChapter;
        [SerializeField] private int maxLevelsInChapter;
        public float hidePicturesDelay;

        private List<GhostMemoryTile> tileList = new List<GhostMemoryTile>();
        private List<GhostMemoryTile> markedTiles = new List<GhostMemoryTile>();
        
        private int _currentLevelIndex;
        private int _currentChapterIndex = 1;
        private int _currentDifficulty;
        private LevelStatus _levelStatus;

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GhostMemorySignals.Instance.OnTileSelected += OnTileSelect;
            GhostMemorySignals.Instance.OnGetLevelStatus += GetLevelStatus;
            GhostMemorySignals.Instance.OnGetDifficulty += GetDifficulty;
            GhostMemorySignals.Instance.OnGetChapterIndex += GetChapterIndex;
            GhostMemorySignals.Instance.OnNextLevel += PassChapter;
            GhostMemorySignals.Instance.OnRestartLevel += ResetGame;
            GhostMemorySignals.Instance.OnFailLevel += FailLevel;
            GhostMemorySignals.Instance.OnGetHidePicturesDelay += GetHidePicturesDelay;
        }

        private LevelStatus GetLevelStatus()
        {
            return _levelStatus;
        }
        
        private int GetDifficulty()
        {
            return _currentDifficulty;
        }
        
        private int GetChapterIndex()
        {
            return _currentChapterIndex;
        }

        private float GetHidePicturesDelay()
        {
            return hidePicturesDelay;
        }

        private void UnsubscribeEvents()
        {
            GhostMemorySignals.Instance.OnTileSelected -= OnTileSelect;
            GhostMemorySignals.Instance.OnGetLevelStatus -= GetLevelStatus;
            GhostMemorySignals.Instance.OnGetDifficulty -= GetDifficulty;
            GhostMemorySignals.Instance.OnGetChapterIndex -= GetChapterIndex;
            GhostMemorySignals.Instance.OnNextLevel -= PassChapter;
            GhostMemorySignals.Instance.OnRestartLevel -= ResetGame;
            GhostMemorySignals.Instance.OnFailLevel -= FailLevel;
            GhostMemorySignals.Instance.OnGetHidePicturesDelay -= GetHidePicturesDelay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Start()
        {
            ResetGame();
        }

        private void SetupLevel()
        {
            foreach (Transform obj in transform)
            {
                Destroy(obj.gameObject);
            }
            tileList.Clear();
            markedTiles.Clear();
            
            
            var level = Instantiate(GetRandomOfType(levelPrefabs,1)[0], transform);

            foreach (var tile in level.GetComponentsInChildren<GhostMemoryTile>())
            {
                tileList.Add(tile); 
            }
            
            markedTiles = GetRandomOfType(tileList, GetNecessaryNumberOfTiles(tileList.Count, _currentDifficulty));
            foreach (GhostMemoryTile tile in markedTiles)
            {
                tile.Mark();
            }
        }

        private void ResetGame()
        {
            _currentLevelIndex = 0;
            CoreGameSignals.Instance.OnStartTimer?.Invoke(60);
            SetupLevel();
            GhostMemorySignals.Instance.OnGameUI?.Invoke();
        }

        private void OnTileSelect(GhostMemoryTile tile)
        {
            if (markedTiles.Contains(tile))
            {
                markedTiles.Remove(tile);
                if (markedTiles.Count == 0) PassLevel();
            }
            else
            {
                SetTilesUnselectable();
                CoreGameSignals.Instance.OnStopTimer?.Invoke();
                StartCoroutine(DelayedAction(1f, FailLevel));
            }
        }

        private IEnumerator DelayedAction(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }
        private void PassLevel()
        {
            SetTilesUnselectable();
            if (_currentLevelIndex >= maxLevelsInChapter)
            {
                StartCoroutine(DelayedAction(1f, EndChapter));
            }
            else
            {
                _currentLevelIndex++;
                StartCoroutine(DelayedAction(1f, SetupLevel));
            }
        }

        private void EndChapter()
        {
            _levelStatus = LevelStatus.Complete;
            CoreGameSignals.Instance.OnStopTimer?.Invoke();
            GhostMemorySignals.Instance.OnNextLevelUI?.Invoke();
                
            foreach (Transform obj in transform)
            {
                Destroy(obj.gameObject);
            }
        }

        private void PassChapter()
        {
            _currentDifficulty += difficultyIncreaseAfterChapter;
            _currentDifficulty = Mathf.Clamp(_currentDifficulty, 0, 100);
            _currentLevelIndex = 0;
            _currentChapterIndex++;
            ResetGame();
        }
        
        private void FailLevel()
        {
            foreach (Transform obj in transform)
            {
                Destroy(obj.gameObject);
            }

            _levelStatus = LevelStatus.Failed;
            GhostMemorySignals.Instance.OnNextLevelUI?.Invoke();
        }

        private void SetTilesUnselectable()
        {
            foreach (GhostMemoryTile tile in tileList)
            {
                tile.isSelectable = false;
            }
        }
        private int GetNecessaryNumberOfTiles(int totalTiles, float difficulty)
        {
            return Mathf.RoundToInt(totalTiles * (difficulty / 100) / 2) + 1;
        }
        
        private List<T> GetRandomOfType<T>(List<T> list, int amount)
        {
            if (amount <= 0) return new List<T>();
            if (list.Count <= amount) return list;
            
            List<T> copyList = new List<T>(list);
            List<T> newList = new List<T>();
            for (int i = 0; i < amount; i++)
            {
                var index = Random.Range(0, copyList.Count);
                T instance = copyList[index];
                copyList.Remove(instance);
                newList.Add(instance);
            }

            return newList;
        }
    }
}
