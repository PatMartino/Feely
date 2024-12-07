using System.Collections.Generic;
using Structs;
using UnityEngine;

namespace Data.Tests
{
    [CreateAssetMenu(fileName = "BFT Question", menuName = "Data/BFT Question")]
    public class BigFiveTestQuestion : Question
    {
        public List<BigFiveTestAnswer> answers;
    }
}