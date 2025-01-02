using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemDisplay 
{
    void OnDisplayInfo();
    void OffDisplayInfo();
    bool IsActive();
}
