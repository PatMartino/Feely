using Data.Skills;
using UnityEngine;

namespace Data.CoverPages
{
    [CreateAssetMenu(fileName = "CoverPage", menuName = "CoverPageData")]
    public class CoverPageData : ScriptableObject
    {
        #region Serialize Field

        [SerializeField] private string gameName;
        [SerializeField] private Sprite gameImage;
        [SerializeField] private string category;
        [SerializeField] private SkillData firstSkill;
        [SerializeField] private SkillData secondSkill;
        [SerializeField] private SkillData thirdSkill;

        #endregion

        #region Public Field

        public string GameName => gameName;
        public Sprite GameImage => gameImage;
        public string Category => category;
        public SkillData FirstSkill => firstSkill;
        public SkillData SecondSkill => secondSkill;
        public SkillData ThirdSkill => thirdSkill;

        #endregion
    }
}