using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData;

    public CharacterData Init(int charId)
    {
        characterData = GetComponent<CharacterData>();
        characterData.Init(charId);

        return characterData;
    }


}
