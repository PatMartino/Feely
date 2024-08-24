using System;
using UnityEngine;
using System.Collections;
using Signals;
using UnityEngine.UI;

namespace Games.CardMatch
{
    public class CardMatchCard : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Image image;

        #endregion
        
        #region Private Field

        private CardTypes _type;
        private bool _isMatched;

        #endregion

        #region Public Field
        
        public Action<CardTypes> OnSetCardTypes;
        public Action OnSetImageFalse;
        public Action OnSetIsMatchTrue;

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            OnSetCardTypes += SetCardType;
            OnSetImageFalse += SetImageFalse;
            OnSetIsMatchTrue += SetIsMatchedTrue;
            StartCoroutine(DisableImages());
        }

        private void OnDisable()
        {
            OnSetCardTypes -= SetCardType;
        }

        #endregion

        #region Functions

        private void Init()
        {
            image.color = _type switch
            {
                CardTypes.Red => Color.red,
                CardTypes.Blue => Color.blue,
                CardTypes.Green => Color.green,
                CardTypes.Yellow => Color.yellow,
                CardTypes.Magenta => Color.magenta,
                CardTypes.Cyan => Color.cyan,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IEnumerator DisableImages()
        {
            yield return new WaitForSeconds(2);
            image.gameObject.SetActive(false);
        }

        public void SelectCard()
        {
            if (!CardMatchSignals.Instance.OnGetCanSelect()) return;
            if (_isMatched) return;
            image.gameObject.SetActive(true);
            if (!CardMatchSignals.Instance.OnGetISelect())
            {
                Debug.Log("Select1");
                CardMatchSignals.Instance.OnSetIsSelectTrue?.Invoke();
                CardMatchSignals.Instance.OnSetSelectedCardType?.Invoke(_type);
                CardMatchSignals.Instance.OnSelectCard?.Invoke(gameObject);
            }
            else
            {
                Debug.Log("Select2");
                CardMatchSignals.Instance.OnSetIsSelectFalse?.Invoke();
                CardMatchSignals.Instance.OnSetCanSelectFalse?.Invoke();
                if (CardMatchSignals.Instance.OnGetSelectedCardType() == _type)
                {
                    _isMatched = true;
                    CardMatchSignals.Instance.OnTrueSelection?.Invoke();
                    CardMatchSignals.Instance.OnIncreaseMatchAmount?.Invoke();
                    CardMatchSignals.Instance.OnSetCanSelectTrue?.Invoke();
                }
                else
                {
                    StartCoroutine(Wait());
                }
            }

        }

        private void SetImageFalse()
        {
            image.gameObject.SetActive(false);
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(1);
            CardMatchSignals.Instance.OnWrongSelection?.Invoke();
            image.gameObject.SetActive(false);
            CardMatchSignals.Instance.OnSetCanSelectTrue?.Invoke();
        }
        private void SetCardType(CardTypes type)
        {
            _type = type;
            Init();
        }

        private void SetIsMatchedTrue()
        {
            _isMatched = true;
        }

        #endregion
    }
}