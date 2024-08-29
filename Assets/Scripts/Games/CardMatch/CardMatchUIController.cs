using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CardMatch
{
    public class CardMatchUIController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Transform gridParent;
        [SerializeField] private GameObject nextLevelButton;

        #endregion

        #region Private Field

        private GridLayoutGroup _grid;

        #endregion

        #region OnEnable

        private void Awake()
        {
            _grid = gridParent.gameObject.GetComponent<GridLayoutGroup>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        
        #region Functions

        private void SubscribeEvents()
        {
            CardMatchSignals.Instance.OnInstantiateCards += OnInstantiateCards;
            CardMatchSignals.Instance.OnDestroyAllCards += OnDestroyAllCard;
            CardMatchSignals.Instance.OnActivenessOfNextLevelButton += OnActivenessOfNextLevelButton;
        }

        private void OnInstantiateCards()
        {
           foreach (var cardType in CardMatchSignals.Instance.OnGetLevelCards())
            {
                GameObject card = Instantiate(Resources.Load<GameObject>("Games/CardMatch/Card/Card"), gridParent);
                card.GetComponent<CardMatchCard>().OnSetCardTypes(cardType);
            }
           
           SetConstraintCount();
        }

        private void SetConstraintCount()
        {

            if (CardMatchSignals.Instance.OnGetDifficultyLevel() >= 2 && CardMatchSignals.Instance.OnGetDifficultyLevel() <4)
            {
                _grid.constraintCount = 2;
            }
            else if(CardMatchSignals.Instance.OnGetDifficultyLevel() >= 4 && CardMatchSignals.Instance.OnGetDifficultyLevel() <6)
            {
                _grid.constraintCount = 3;
            }
            else if(CardMatchSignals.Instance.OnGetDifficultyLevel() >= 6 && CardMatchSignals.Instance.OnGetDifficultyLevel() <9)
            {
                _grid.constraintCount = 4;
            }
            else if(CardMatchSignals.Instance.OnGetDifficultyLevel() >= 9 && CardMatchSignals.Instance.OnGetDifficultyLevel() <11)
            {
                _grid.constraintCount = 5;
            }
            else
            {
                _grid.constraintCount = 6;
            }
            
            Debug.LogWarning("Constraint Count: " + _grid.constraintCount);
        }

        private void OnDestroyAllCard()
        {
            foreach (Transform child in transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }
        }

        private void OnActivenessOfNextLevelButton(bool status)
        {
            nextLevelButton.SetActive(status);
        }
        
        private void UnSubscribeEvents()
        {
            CardMatchSignals.Instance.OnInstantiateCards -= OnInstantiateCards;
            CardMatchSignals.Instance.OnDestroyAllCards -= OnDestroyAllCard;
            CardMatchSignals.Instance.OnActivenessOfNextLevelButton -= OnActivenessOfNextLevelButton;
        }

        #endregion
    }
}
