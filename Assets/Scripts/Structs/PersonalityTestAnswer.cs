using System;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public class PersonalityTestAnswer
    {
        [TextArea(2,4)] public string answerText;
        public int pointEToI;
        public int pointNToS;
        public int pointTToF;
        public int pointJToP;
        public int pointAToT;
    }
}