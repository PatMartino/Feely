using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Structs
{
    [Serializable]
    public struct ClassicTestAnswer
    {
        [TextArea(2,4)] public string answerText;
        public bool isCorrectAnswer;
    }
}