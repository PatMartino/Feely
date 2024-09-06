using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Tests
{
    public class PersonalityTestTrait : MonoBehaviour
    {
        [SerializeField] private int maxPointsOnTrait;
        [SerializeField] private string traitName1;
        [SerializeField] private string traitName2;
        [SerializeField] private TMP_Text traitText;
        [SerializeField] private Slider slider;

        public void SetTraitUp(int score)
        {
            var percentage = Mathf.Abs(score) / (float)maxPointsOnTrait * 50 + 50;
            if (score < 0)
            {
                slider.value = (float)(100 - percentage) / 100;
                traitText.text = $"{traitName1} {percentage}%";
            }
            else
            {
                slider.value = (float)percentage / 100;
                traitText.text = $"{traitName2} {percentage}%";
            }
        }

    }
}
