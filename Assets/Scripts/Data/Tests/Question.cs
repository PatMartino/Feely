using UnityEngine;

namespace Data.Tests
{
    public class Question : ScriptableObject
    {
        [TextArea(5,14)]public string question;
        public Sprite image;
    }
}