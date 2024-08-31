using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Data.Tests
{
    public class TestAnswerButton : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public byte answerIndex;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Color selectedColor;
        
        private Color _defaultColor;

        private void Start()
        {
            _defaultColor = buttonImage.color;
        }

        private void OnEnable()
        {
            TestManager.Instance.OnAnswerSelected += UnselectAnswer;
        }

        private void OnDisable()
        {
            TestManager.Instance.OnAnswerSelected -= UnselectAnswer;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectAnswer();
            TestManager.Instance.OnAnswerSelected?.Invoke(answerIndex);
        }

        private void SelectAnswer()
        {
            TestManager.Instance.SelectAnswer(answerIndex);
            buttonImage.color = selectedColor;
        }
        
        private void UnselectAnswer(int index)
        {
            if (index == answerIndex) return;
            buttonImage.color = _defaultColor;
        }
    }
}
