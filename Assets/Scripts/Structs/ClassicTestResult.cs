using System;
using Data.Tests;
using Unity.Mathematics;

namespace Structs
{
    [Serializable]
    public struct ClassicTestResult
    {
        public TestResult result;
        public int2 range;
    }
}