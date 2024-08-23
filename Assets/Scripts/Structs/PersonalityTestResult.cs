using System;
using Data.Tests;
using UnityEngine.Serialization;

namespace Structs
{
    [Serializable]
    public struct PersonalityTestResult
    {
        public TestResult result;
        public char typeEOrI;
        public char typeNOrS;
        public char typeTOrF;
        public char typeJOrP;
        public char typeAOrT;
    }
}