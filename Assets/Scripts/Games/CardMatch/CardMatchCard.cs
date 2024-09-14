using System;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
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
        private bool _isSelect;
        private Sprite _sprite;

        #endregion

        #region Public Field
        
        public Action<CardTypes> OnSetCardTypes;
        public Action OnSetImageFalse;
        public Action OnSetIsMatchTrue;
        public Action OnSetIsSelectTrue;

        #endregion

        #region OnEnable

        private void Awake()
        {
            _sprite = GetComponent<Image>().sprite;
        }

        private void OnEnable()
        {
            OnSetCardTypes += SetCardType;
            OnSetImageFalse += SetImageFalse;
            OnSetIsMatchTrue += SetIsMatchedTrue;
            OnSetIsSelectTrue += SetIsSelectTrue;
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
            _sprite = _type switch
            {
                CardTypes.Red => Resources.Load<Sprite>("Games/CardMatch/Images/Image1"),
                CardTypes.Blue => Resources.Load<Sprite>("Games/CardMatch/Images/Image2"),
                CardTypes.Green => Resources.Load<Sprite>("Games/CardMatch/Images/Image3"),
                CardTypes.Yellow => Resources.Load<Sprite>("Games/CardMatch/Images/Image4"),
                CardTypes.Magenta => Resources.Load<Sprite>("Games/CardMatch/Images/Image5"),
                CardTypes.Cyan => Resources.Load<Sprite>("Games/CardMatch/Images/Image6"),
                CardTypes.Black => Resources.Load<Sprite>("Games/CardMatch/Images/Image7"),
                CardTypes.Grey => Resources.Load<Sprite>("Games/CardMatch/Images/Image8"),
                CardTypes.Orange => Resources.Load<Sprite>("Games/CardMatch/Images/Image9"),
                CardTypes.White => Resources.Load<Sprite>("Games/CardMatch/Images/Image10"),
                CardTypes.Purple => Resources.Load<Sprite>("Games/CardMatch/Images/Image11"),
                CardTypes.Bum => Resources.Load<Sprite>("Games/CardMatch/Images/Image12"),
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
            if (_isSelect) return;
            image.gameObject.SetActive(true);
            if (!CardMatchSignals.Instance.OnGetISelect() )
            {
                
                _isSelect = true;
                
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

        private void SetIsSelectTrue()
        {
            _isSelect = false;
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