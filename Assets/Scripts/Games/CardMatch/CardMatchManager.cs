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

        private int _levelID = 1;
        private CardMatchLevelData _levelData;
        private CardTypes[] _allCardTypes = new CardTypes[] { CardTypes.Blue , CardTypes.Green, CardTypes.Red, CardTypes.Magenta, CardTypes.Yellow, CardTypes.Cyan};
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
            
        }

        private void Start()
        {
            LevelLoader();
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
        }

        private void LevelLoader()
        {
            //Instantiate(Resources.Load<GameObject>($"Games/BallSort/LevelPrefabs/Level{_levelID}"),transform);
            Debug.Log("LevelLoader");
            _levelData = Resources.Load<CardMatchLevelData>($"Games/CardMatch/LevelData/{_levelID}");
            _matchAmount = 0;
            _maxAmount = _levelData.NumberOfCards;
            GenerateLevelCards();
            
            //UISignals.Instance.OnUpdateBallSortLevelIDText?.Invoke();
        }

        private void LevelDestroyer()
        {
            CardMatchSignals.Instance.OnDestroyAllCards?.Invoke();
        }

        private void NextLevel()
        {
            _levelCards.Clear();
            _levelID++;
            _canSelect = true;
            _isSelect = false;
            Debug.Log(_levelID);
            LevelDestroyer();
            LevelLoader();
        }
        
        private void GenerateLevelCards()
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
        }

        private void OnIncreaseMatchAmount()
        {
            _matchAmount++;
            CheckLevelFinished();
        }

        private void CheckLevelFinished()
        {
            if (_matchAmount >= _maxAmount)
            {
                Debug.Log("Level Finished");
                NextLevel();
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
        }

        #endregion
        
    }
}