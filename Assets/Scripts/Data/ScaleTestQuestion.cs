using System.Collections.Generic;
using Structs;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ST Question", menuName = "Data/ST Question")]
    public class ScaleTestQuestion : Question
    {
        public List<ScaleTestAnswer> answers;
    }
}
