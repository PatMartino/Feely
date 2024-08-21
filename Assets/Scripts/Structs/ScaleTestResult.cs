using System;
using Data.Tests;
using Unity.Mathematics;

namespace Structs
{
    [Serializable]
    public struct ScaleTestResult
    {
        public TestResult result;
        public int2 range;
    }
}