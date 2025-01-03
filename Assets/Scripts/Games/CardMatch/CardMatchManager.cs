using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.CardMatch
{
    public class CardMatchManager : MonoBehaviour
    {
        #region Private Field

        private int _levelID;
        private int _section = 1;
        private float _timeDuration = 120f;
        private int _difficultLevel;
        private CardMatchLevelData _levelData;
        private CardTypes[] _allCardTypes = new CardTypes[] { CardTypes.Blue , CardTypes.Green, CardTypes.Red, CardTypes.Magenta, CardTypes.Yellow, CardTypes.Cyan, CardTypes.Black,CardTypes.Grey, CardTypes.Bum,CardTypes.Orange,CardTypes.White,CardTypes.Purple};
        private List<CardTypes> _levelCards = new List<CardTypes>();
        private List<CardTypes> selectedCardTypes =new List<CardTypes>();
        private bool _isSelect;
        private CardTypes _selectedCardType;
        private GameObject _selectedCard;
        private int _matchAmount;
        private int _maxAmount;
        private bool _canSelect = true;

        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
            LoadLevel();
        }

        private void Start()
        {
            SetDifficultyLevel();
            SectionLoader();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            CardMatchSignals.Instance.OnGetLevelCards += OnGetLevelCards;
            CardMatchSignals.Instance.OnSetIsSelectTrue += OnSetIsSelectTrue;
            CardMatchSignals.Instance.OnSetIsSelectFalse += OnSetIsSelectFalse;
            CardMatchSignals.Instance.OnGetISelect += OnGetIsSelect;
            CardMatchSignals.Instance.OnSetSelectedCardType += OnSetSelectedCardType;
            CardMatchSignals.Instance.OnGetSelectedCardType += OnGetSelectedCardType;
            CardMatchSignals.Instance.OnSelectCard += OnSelectCard;
            CardMatchSignals.Instance.OnWrongSelection += OnWrongSelection;
            CardMatchSignals.Instance.OnTrueSelection += OnTrueSelection;
            CardMatchSignals.Instance.OnIncreaseMatchAmount += OnIncreaseMatchAmount;
            CardMatchSignals.Instance.OnSetCanSelectTrue += OnSetCanSelectTrue;
            CardMatchSignals.Instance.OnSetCanSelectFalse += OnSetCanSelectFalse;
            CardMatchSignals.Instance.OnGetCanSelect += OnGetCanSelect;
            CardMatchSignals.Instance.OnNextPlay += OnNextLevel;
            CardMatchSignals.Instance.OnGetDifficultyLevel += OnGetDifficultyLevel;
            CardMatchSignals.Instance.OnGetLevelID += OnGetLevelID;
            CardMatchSignals.Instance.OnGetSectionID += OnGetSectionID;
            CardMatchSignals.Instance.OnRestartLevel += OnRestartLevel;
        }

        private void LoadLevel()
        {
            _levelID = ES3.KeyExists("CardMatchLevelID") ? ES3.Load<int>("CardMatchLevelID") : 1;
        }

        private void OnNextLevel()
        {
            _levelCards.Clear();
            _levelID++;
            ES3.Save("CardMatchLevelID",_levelID);
            _section = 1;
            _canSelect = true;
            _isSelect = false;
            SetDifficultyLevel();
            SectionDestroyer();
            SectionLoader();
            CardMatchSignals.Instance.OnActivenessOfNextLevelButton?.Invoke(false);
        }

        private void SectionLoader()
        {
            CardMatchSignals.Instance.OnUpdateLevelText?.Invoke();
            //Instantiate(Resources.Load<GameObject>($"Games/BallSort/LevelPrefabs/Level{_levelID}"),transform);
            Debug.Log("LevelLoader");
            _levelData = Resources.Load<CardMatchLevelData>($"Games/CardMatch/LevelData/{_difficultLevel}");
            _matchAmount = 0;
            _maxAmount = _levelData.NumberOfCards;
            CardMatchSignals.Instance.OnUpdateStageText?.Invoke();
            GenerateSectionCards();
            
            //UISignals.Instance.OnUpdateBallSortLevelIDText?.Invoke();
        }

        private void OnRestartLevel()
        {
            _levelCards.Clear();
            _section = 1;
            _canSelect = true;
            _isSelect = false;
            SetDifficultyLevel();
            SectionDestroyer();
            SectionLoader();
            CardMatchSignals.Instance.OnActivenessOfNextLevelButton?.Invoke(false);
        }

        private void SectionDestroyer()
        {
            CardMatchSignals.Instance.OnDestroyAllCards?.Invoke();
        }

        private void NextSection()
        {
            _levelCards.Clear();
            _difficultLevel++;
            _section++;
            _canSelect = true;
            _isSelect = false;
            CardMatchSignals.Instance.OnUpdateStageText?.Invoke();
            Debug.LogWarning("Difficulty Level: " + _difficultLevel);
            SectionDestroyer();
            SectionLoader();
        }
        
        private void GenerateSectionCards()
        {
            Debug.Log("GenerateLevelCards");

            selectedCardTypes = _allCardTypes.OrderBy(x => Random.Range(0, _allCardTypes.Length)).Take(_maxAmount)
                .ToList();

            foreach (var cardType in selectedCardTypes)
            {
                _levelCards.Add(cardType);
                _levelCards.Add(cardType);
            }

            // Kartları karıştır
            _levelCards = _levelCards.OrderBy(x => Random.value).ToList();
            CardMatchSignals.Instance.OnInstantiateCards.Invoke();

        }

        private void SetDifficultyLevel()
        {
            var difficulty = _levelID + _section;
            _difficultLevel = difficulty;
            var timer = 120f - ((_levelID - 1) * 5);
            _timeDuration = timer;
            if (_difficultLevel>8)
            {
                _timeDuration = 80f;
                _difficultLevel = 8;
            }
            CoreGameSignals.Instance.OnStartTimer?.Invoke(_timeDuration);
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }

        private int OnGetSectionID()
        {
            return _section;
        }

        private int OnGetDifficultyLevel()
        {
            return _difficultLevel;
        }

        private void OnSetIsSelectTrue()
        {
            _isSelect = true;
        }
        
        private void OnSetIsSelectFalse()
        {
            _isSelect = false;
        }

        private bool OnGetIsSelect()
        {
            return _isSelect;
        }

        private void OnSetSelectedCardType(CardTypes type)
        {
            _selectedCardType = type;
        }

        private CardTypes OnGetSelectedCardType()
        {
            return _selectedCardType;
        }

        private void OnSelectCard(GameObject gO)
        {
            _selectedCard = gO;
        }

        private void OnTrueSelection()
        {
            _selectedCard.GetComponent<CardMatchCard>().OnSetIsMatchTrue?.Invoke();
        }

        private void OnWrongSelection()
        {
            _selectedCard.GetComponent<CardMatchCard>().OnSetImageFalse?.Invoke();
            _selectedCard.GetComponent<CardMatchCard>().OnSetIsSelectTrue?.Invoke();
        }

        private void OnIncreaseMatchAmount()
        {
            _matchAmount++;
            CheckSectionFinished();
        }

        private void CheckSectionFinished()
        {
            if (_matchAmount >= _maxAmount)
            {
                if (_section<=4)
                {
                    Debug.LogWarning("NextLevel");
                    NextSection();
                }
                else
                {
                    Debug.LogWarning("LevelFinished");
                    CoreGameSignals.Instance.OnStopTimer?.Invoke();
                    CardMatchSignals.Instance.OnActivenessOfNextLevelButton?.Invoke(true);
                }
                
            }
        }

        private List<CardTypes> OnGetLevelCards()
        {
            return _levelCards;
        }

        private void OnSetCanSelectTrue()
        {
            _canSelect = true;
        }

        private void OnSetCanSelectFalse()
        {
            _canSelect = false;
        }

        private bool OnGetCanSelect()
        {
            return _canSelect;
        }
        
        private void UnSubscribeEvents()
        {
            CardMatchSignals.Instance.OnGetLevelCards -= OnGetLevelCards;
            CardMatchSignals.Instance.OnSetIsSelectTrue -= OnSetIsSelectTrue;
            CardMatchSignals.Instance.OnSetIsSelectFalse -= OnSetIsSelectFalse;
            CardMatchSignals.Instance.OnGetISelect -= OnGetIsSelect;
            CardMatchSignals.Instance.OnSetSelectedCardType -= OnSetSelectedCardType;
            CardMatchSignals.Instance.OnGetSelectedCardType -= OnGetSelectedCardType;
            
            CardMatchSignals.Instance.OnSelectCard -= OnSelectCard;
            CardMatchSignals.Instance.OnWrongSelection -= OnWrongSelection;
            CardMatchSignals.Instance.OnTrueSelection -= OnTrueSelection;
            CardMatchSignals.Instance.OnIncreaseMatchAmount -= OnIncreaseMatchAmount;
            
            
            CardMatchSignals.Instance.OnSetCanSelectTrue -= OnSetCanSelectTrue;
            CardMatchSignals.Instance.OnSetCanSelectFalse -= OnSetCanSelectFalse;
            CardMatchSignals.Instance.OnGetCanSelect -= OnGetCanSelect;
            CardMatchSignals.Instance.OnNextPlay -= OnNextLevel;
            
            
            CardMatchSignals.Instance.OnGetDifficultyLevel -= OnGetDifficultyLevel;
            CardMatchSignals.Instance.OnGetLevelID -= OnGetLevelID;
            CardMatchSignals.Instance.OnGetSectionID -= OnGetSectionID;
        }

        #endregion
        
    }
}
