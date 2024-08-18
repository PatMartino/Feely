using System;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public struct ScaleTestAnswer
    {
        [TextArea(2,4)] public string answerText;
        public int scorePoint;
    }
}
