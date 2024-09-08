using System;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class SwipeController : MonoBehaviour, IEndDragHandler
    {
        #region Serialize Field

        [SerializeField] private int maxPage;
        [SerializeField] private int currentPage;
        [SerializeField] private RectTransform levelPagesRect;
        [SerializeField] private float tweenTime;
        [SerializeField] private Ease tweenType;

        #endregion

        #region Private Field
        
        private float _targetPos;
        private float _homeTransform = 0;
        private float _gameTransform = -1080;
        private float _testTransform = -2160;
        private float _dragThreshold;

        #endregion

        #region Awake

        private void Awake()
        {
            _dragThreshold = Screen.width / 15;
            currentPage = 1;
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
            UISignals.Instance.OnHome += OnHome;
            UISignals.Instance.OnGame += OnGame;
            UISignals.Instance.OnTest += OnTest;
        }

        private void OnHome()
        {
            _targetPos = _homeTransform;
            MovePage();
            UISignals.Instance.OnChangeMenuIconColor?.Invoke(MenuTypes.TodayMenu);
            UISignals.Instance.OnChangeHeaderText?.Invoke(MenuTypes.TodayMenu);
            currentPage = 1;
        }

        private void OnGame()
        {
            _targetPos = _gameTransform;
            MovePage();
            UISignals.Instance.OnChangeMenuIconColor?.Invoke(MenuTypes.GameMenu);
            UISignals.Instance.OnChangeHeaderText?.Invoke(MenuTypes.GameMenu);
            currentPage = 2;
        }

        private void OnTest()
        {
            _targetPos = _testTransform;
            MovePage();
            UISignals.Instance.OnChangeMenuIconColor?.Invoke(MenuTypes.TestMenu);
            UISignals.Instance.OnChangeHeaderText?.Invoke(MenuTypes.TestMenu);
            currentPage = 3;
        }

        private void PreviousPage()
        {
            switch (currentPage)
            {
                case 2:
                    OnHome();
                    break;
                case 3:
                    OnGame();
                    break;
            }
        }

        private void NextPage()
        {
            switch (currentPage)
            {
                case 1:
                    OnGame();
                    break;
                case 2:
                    OnTest();
                    break;
            }
        }

        private void MovePage()
        {
            levelPagesRect.DOLocalMoveX(_targetPos, tweenTime).SetEase(tweenType);
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnHome -= OnHome;
            UISignals.Instance.OnGame -= OnGame;
            UISignals.Instance.OnTest -= OnTest;
        }

        #endregion

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > _dragThreshold)
            {
                if (eventData.position.x > eventData.pressPosition.x)
                {
                    PreviousPage();
                }
                else
                {
                    NextPage();
                }
            }
            else
            {
                MovePage();
            }
        }
    }
}
