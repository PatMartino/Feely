using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Skills
{
    [CreateAssetMenu(fileName = "Skill", menuName = "SkillData")]
    public class SkillData : ScriptableObject
    {
        #region Serialize Field

        [SerializeField] private Sprite skillImage;
        [SerializeField] private string skillName;
        [TextArea] [SerializeField] private string skillDescription;

        #endregion

        #region Public Field

        public Sprite SkillImage => skillImage;
        public string SkillName => skillName;
        public string SkillDescription => skillDescription;
        
        #endregion
    }
}