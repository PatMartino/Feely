using System.Collections.Generic;
using Structs;
using UnityEngine;

namespace Data.Tests
{
    [CreateAssetMenu(fileName = "PT Question", menuName = "Data/PT Question")]
    public class PersonalityTestQuestion: Question
    {
        public List<PersonalityTestAnswer> answers;
    }
}