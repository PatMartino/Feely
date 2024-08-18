using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Question : ScriptableObject
    {
        [TextArea(5,14)]public string question;
        public Sprite image;
    }
}