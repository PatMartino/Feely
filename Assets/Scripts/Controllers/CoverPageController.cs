using System;
using Data.CoverPages;
using Data.Skills;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CoverPageController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private GamesAndTestsNames gameType;
        [SerializeField] private GameObject gameImage;
        [SerializeField] private TextMeshProUGUI gameName;
        [SerializeField] private TextMeshProUGUI category;
        [SerializeField] private Transform firstSkill;
        [SerializeField] private Transform secondSkill;
        [SerializeField] private Transform thirdSkill;

        #endregion

        #region Private Field

        private CoverPageData _gameData;

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            AssignGame();
        }

        #endregion

        #region Functions

        private void AssignGame()
        {
            _gameData = Resources.Load<CoverPageData>($"Data/CoverPageData/{gameType.ToString()}");
            Init();
        }

        private void Init()
        {
            gameImage.GetComponent<Image>().sprite = _gameData.GameImage;
            gameName.text = _gameData.GameName;
            category.text = _gameData.Category;
            firstSkill.GetChild(0).GetComponent<Image>().sprite = _gameData.FirstSkill.SkillImage;
            firstSkill.GetChild(1).GetComponent<TextMeshProUGUI>().text = _gameData.FirstSkill.SkillName;
            firstSkill.GetChild(2).GetComponent<TextMeshProUGUI>().text = _gameData.FirstSkill.SkillDescription;
                    
            secondSkill.GetChild(0).GetComponent<Image>().sprite = _gameData.SecondSkill.SkillImage;
            secondSkill.GetChild(1).GetComponent<TextMeshProUGUI>().text = _gameData.SecondSkill.SkillName;
            secondSkill.GetChild(2).GetComponent<TextMeshProUGUI>().text = _gameData.SecondSkill.SkillDescription;
                    
            thirdSkill.GetChild(0).GetComponent<Image>().sprite = _gameData.ThirdSkill.SkillImage;
            thirdSkill.GetChild(1).GetComponent<TextMeshProUGUI>().text = _gameData.ThirdSkill.SkillName;
            thirdSkill.GetChild(2).GetComponent<TextMeshProUGUI>().text = _gameData.ThirdSkill.SkillDescription;
        }

        #endregion
    }
}
