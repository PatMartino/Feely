using System;
using UnityEngine;

namespace Data.Tests
{
    public class BigFiveTestTrait : MonoBehaviour
    {
        [SerializeField] GameObject traitIndicatorPoint;
        
        public void SetTraitIndicator(float a, float b, float c, float d, float e)
        {
            var max = Mathf.Max(a, b, c, d, e);
            
            Vector2 a1 = Vector2.up * a;
            Vector2 b1 = Quaternion.Euler(0, 0, -72) * Vector2.up * b;
            Vector2 c1 = Quaternion.Euler(0, 0, -144) * Vector2.up * c;
            Vector2 d1 = Quaternion.Euler(0, 0, -216) * Vector2.up * d;
            Vector2 e1 = Quaternion.Euler(0, 0, -288) * Vector2.up * e;
            
            var final = a1 * a1.magnitude + b1 * b1.magnitude + c1 * c1.magnitude + d1 * d1.magnitude + e1 * e1.magnitude;
            var magnitude = a1.magnitude + b1.magnitude + c1.magnitude + d1.magnitude + e1.magnitude;
            
            traitIndicatorPoint.transform.position = final*11/8/max/magnitude + (Vector2)traitIndicatorPoint.transform.parent.position;
        }
    }
}
