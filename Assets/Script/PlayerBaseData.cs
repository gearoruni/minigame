using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerBaseData : Singleton<PlayerBaseData>
{
    public int nowSelectedCharacter = 1001;
    public List<int> selectedCharacterList = new List<int>();
    
    public Dictionary<int,int>characterLevelDir = new Dictionary<int,int>();

    public Entity entity;
    public override void Init()
    {
        characterLevelDir[1001] = 1;
        characterLevelDir[1002] = 1;

        entity = EntityManager.Instance.CreateEntity(1,1);
        CameraManager.Instance.RegisterFollow(entity);

        EntityManager.Instance.CreateEntity(4,4);
    }
}