using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Data
{
    public class TestAnswerButton : MonoBehaviour, IPointerClickHandler
    {
        public byte answerIndex;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SelectAnswer();
        }

        private void SelectAnswer()
        {
            TestManager.Instance.SelectAnswer(answerIndex);
        }
    }
}
