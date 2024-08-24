using System;
using Signals;
using UnityEngine;

namespace Games.CardMatch
{
    public class CardMatchUIController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Transform gridParent;

        #endregion

        #region OnEnable

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
        }

        private void OnInstantiateCards()
        {
           foreach (var cardType in CardMatchSignals.Instance.OnGetLevelCards())
            {
                GameObject card = Instantiate(Resources.Load<GameObject>("Games/CardMatch/Card/Card"), gridParent);
                // Kart tipini card objesine ata
                card.GetComponent<CardMatchCard>().OnSetCardTypes(cardType);
            }
        }

        private void OnDestroyAllCard()
        {
            foreach (Transform child in transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }
        }
        
        private void UnSubscribeEvents()
        {
            CardMatchSignals.Instance.OnInstantiateCards -= OnInstantiateCards;
            CardMatchSignals.Instance.OnDestroyAllCards -= OnDestroyAllCard;
        }

        #endregion
    }
}
