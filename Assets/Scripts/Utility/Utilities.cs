using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    public static class Utilities
    {
        public static IEnumerator DelayedMethodCall(float delay, Action method)
        {
            yield return new WaitForSeconds(delay);
            method();
        }
    }
}