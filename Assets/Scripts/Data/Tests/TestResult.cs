using UnityEngine;

namespace Data.Tests
{
    [CreateAssetMenu(fileName = "Test Result", menuName = "Data/Test Result")]
    public class TestResult : ScriptableObject
    {
        [TextArea(2, 4)] public string resultTitle;
        [TextArea(4, 14)] public string result;
    }
}
