using UnityEngine;

namespace Games.BallSort
{
    [CreateAssetMenu(fileName = "GameData", menuName = "TubeData")]
    public class TubeData : ScriptableObject
    {
        [SerializeField] private int tubeAmount;
        public int TubeAmount => tubeAmount;
    }
}
