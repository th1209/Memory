using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// CPUのレベルだけを管理するクラス。
/// (Scene間でLevel情報をやり取りする。)
/// </summary>
public class CpuLevel : MonoBehaviour
{
    private static int _level = 1;

    public void SetLevel(int level)
    {
        if (level < 0 || level > 2)
            throw new ArgumentException("");
        _level = level;
    }

    public int GetLevel()
    {
        return _level;
    }

    public const int Easy = 0;
    public const int Normal = 1;
    public const int Hard = 2;
}


