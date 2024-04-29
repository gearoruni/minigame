using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public interface BaseManager
{
    bool Init();
    UniTask<bool> InitAysnc();
    void Release();
}
