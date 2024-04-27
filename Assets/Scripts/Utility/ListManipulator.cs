using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListManipulator
{
    public static void AddMultipleTimes<T>(List<T> list, T element, int times)
    {
        for (int i = 0; i < times; i++)
        {
            list.Add(element);
        }
    }

    public static List<T> ShuffleList<T>(List<T> list)
    {
       return list.OrderBy(x => Random.value).ToList();
    }
}
