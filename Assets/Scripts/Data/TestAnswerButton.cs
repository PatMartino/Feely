using System;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Data
{
    public class TestAnswerButton : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public byte answerIndex;
        [SerializeField] private Image selectedImage;


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
            selectedImage.enabled = true;
        }
        
        private void UnselectAnswer(int index)
        {
            if (index == answerIndex) return;
            
            selectedImage.enabled = false;
        }
    }
}
