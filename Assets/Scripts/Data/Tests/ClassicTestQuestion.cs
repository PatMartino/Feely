using System.Collections.Generic;
using Structs;
using UnityEngine;

namespace Data.Tests
{
    [CreateAssetMenu(fileName = "CT Question", menuName = "Data/CT Question")]
    public class ClassicTestQuestion : Question
    {
        public List<ClassicTestAnswer> answers;
    }
}
