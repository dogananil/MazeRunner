using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumbersOfLen 
{
    public static int GetRandomTaskId()
    {
        return Random.Range(100000000, 999999999);
    }
}
