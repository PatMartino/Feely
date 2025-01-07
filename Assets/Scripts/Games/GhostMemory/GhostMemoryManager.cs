using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Games.GhostMemory
{
    public class GhostMemoryManager : MonoSingleton<GhostMemoryManager>
    {
        [SerializeField] private List<GameObject> levelPrefabs;
        [SerializeField] private int difficultyPercentage;
        [SerializeField] private int difficultyIncreaseAfterChapter;
        [SerializeField] private int maxLevelsInChapter;
        public float hidePicturesDelay;

        public UnityAction<GhostMemoryTile> OnTileSelected;

        private List<GhostMemoryTile> tileList = new List<GhostMemoryTile>();
        private List<GhostMemoryTile> markedTiles = new List<GhostMemoryTile>();
        
        private int _currentLevelIndex;
        private int _currentDifficulty;

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            OnTileSelected += OnTileSelect;
        }


        private void UnsubscribeEvents()
        {
            OnTileSelected -= OnTileSelect;
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
            _currentDifficulty = difficultyPercentage;

            SetupLevel();
        }

        private void OnTileSelect(GhostMemoryTile tile)
        {
            if (markedTiles.Contains(tile))
            {
                markedTiles.Remove(tile);
                if (markedTiles.Count == 0) PassLevel();
            }
            else FailLevel();
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
                _currentDifficulty += difficultyIncreaseAfterChapter;
                _currentDifficulty = Mathf.Clamp(_currentDifficulty, 0, 100);
                _currentLevelIndex = 0;
            }
            else _currentLevelIndex++;
            
            StartCoroutine(DelayedAction(1f, SetupLevel));
        }

        private void FailLevel()
        {
            SetTilesUnselectable();
            StartCoroutine(DelayedAction(1f, ResetGame));
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
