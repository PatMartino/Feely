using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Structs
{
    [Serializable]
    public class BigFiveTestAnswer
    {
        [TextArea(2,4)] public string answerText;
        public int pointOpenness;
        public int pointConscientiousness;
        public int pointExtraversion;
        public int pointAgreeableness;
        public int pointNeuroticism;
    }
}