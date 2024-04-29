using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        var go = new GameObject("gameCore");
        DontDestroyOnLoad(go);
        var core = go.AddComponent<GameCore>();
        await core.Active();
    }
}
