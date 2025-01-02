using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    void RevertStat(int stat, int baseStat);
    void RevertStat(float stat, float baseStat);

    void ModifyStat(int stat, int baseStat, int maxStat, int mod, float seconds);
    void ModifyStat(float stat, float baseStat, float maxStat, float mod, float seconds);

    void RegenerateStat(int stat, int baseStat, float perSec, float baseSec);
    void RegenerateStat(float stat, float baseStat, float perSec, float baseSec);

    IEnumerator WhileUnderStatMod(int stat, int baseStat, float seconds);
    IEnumerator WhileUnderStatMod(float stat, float baseStat, float seconds);
}
