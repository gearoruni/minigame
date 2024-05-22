using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerBaseData : Singleton<PlayerBaseData>
{
    public int nowSelectedCharacter = 1001;
    public List<int> selectedCharacterList = new List<int>();
    
    public Dictionary<int,int>characterLevelDir = new Dictionary<int,int>();

    public Entity entity;

    public List<bool> skillLocked ;
    public Dictionary<string,int> items = new Dictionary<string,int>();
    public override void Init()
    {
        //Entity health = EntityManager.Instance.CreateEntity(8, 9);
        Entity health = EntityManager.Instance.CreateEntity(9, 10);

        characterLevelDir[1001] = 1;
        characterLevelDir[1002] = 1;

        skillLocked = new List<bool> { false, true, true, true, true };
        items.Clear();

        entity = EntityManager.Instance.CreateEntity(1, 1);
        CameraManager.Instance.RegisterFollow(entity);
        TagComponent tagComponent1 = (TagComponent)entity.GetComponent("TagComponent");

        //Entity eentity = EntityManager.Instance.CreateEntity(4, 2);
        Entity eentity = EntityManager.Instance.CreateEntity(10, 11);
        //eentity = EntityManager.Instance.CreateEntity(4, 6);
        //eentity = EntityManager.Instance.CreateEntity(4, 7);
    }
}
