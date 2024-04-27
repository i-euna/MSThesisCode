using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParseEnum
{
    public static T Parse<T>(string value)
    {
        return (T)System.Enum.Parse(typeof(T), value, true);
    }
}
