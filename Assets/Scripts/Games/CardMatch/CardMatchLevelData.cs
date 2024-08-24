using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CardMatch
{
    [CreateAssetMenu(fileName = "GameData", menuName = "CardData")]
    public class CardMatchLevelData : ScriptableObject
    {
        [SerializeField] private int numberOfCards;
        public int NumberOfCards => numberOfCards;
    }
}