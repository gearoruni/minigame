using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CharacterDataCache
{
    public int id;
    public int hp;
    public bool ELock, ShiftLock;
    public bool dead;
}
public class PlayerBaseData : Singleton<PlayerBaseData>
{
    public int nowSelectedCharacter = 1001;
    public List<int> selectedCharacterList = new List<int>();
    public bool changeCD=false;
    private Timer timer;
    public Dictionary<int,int>characterLevelDir = new Dictionary<int,int>();
    public Dictionary<int, Dictionary<string,float>> playerDataCache = new Dictionary<int, Dictionary<string, float>>();
    public Dictionary<int, CharacterDataCache> playerDatas = new Dictionary<int, CharacterDataCache>();
    public Entity entity;

    private CharacterDataComponent characterDataComponent;
    private SkillComponent skillComponent;
    public override void Init()
    {
        characterLevelDir[1001] = 1;
        characterLevelDir[1002] = 1;
        selectedCharacterList.Add(1001);
        selectedCharacterList.Add(1002);
        playerDataCache.TryAdd(1001,new Dictionary<string, float>());
        playerDataCache.TryAdd(1002,new Dictionary<string, float>());
        entity = EntityManager.Instance.CreateEntity(1,1);
        CameraManager.Instance.RegisterFollow(entity);
        TagComponent tagComponent1 = (TagComponent)entity.GetComponent("TagComponent");
        characterDataComponent = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");
        skillComponent = (SkillComponent)entity.GetComponent("SkillComponent");
        //GameObject.Instantiate(Preloader.Instance.GetGameObject("Map"));

        // Entity eentity = EntityManager.Instance.CreateEntity(4, 13);
       var eentity = EntityManager.Instance.CreateEntity(9, 53);
        //eentity = EntityManager.Instance.CreateEntity(8, 34);
        //eentity = EntityManager.Instance.CreateEntity(4, 3);
        //eentity = EntityManager.Instance.CreateEntity(4, 4);
        eentity = EntityManager.Instance.CreateEntity(8, 5);
        // eentity = EntityManager.Instance.CreateEntity(4, 7);//
        // eentity = EntityManager.Instance.CreateEntity(4, 8);//
        // eentity = EntityManager.Instance.CreateEntity(4, 9);//
        // eentity = EntityManager.Instance.CreateEntity(4, 10);//
        // eentity = EntityManager.Instance.CreateEntity(4, 11);//
        // eentity = EntityManager.Instance.CreateEntity(4, 12);//
        // eentity = EntityManager.Instance.CreateEntity(4, 13);
        // eentity = EntityManager.Instance.CreateEntity(4, 14);//
        // eentity = EntityManager.Instance.CreateEntity(4, 15);//
        // eentity = EntityManager.Instance.CreateEntity(4, 16);//
        // eentity = EntityManager.Instance.CreateEntity(8, 19);
        // eentity = EntityManager.Instance.CreateEntity(4, 20);//
        // eentity = EntityManager.Instance.CreateEntity(4, 22);//
        // eentity = EntityManager.Instance.CreateEntity(4, 23);//
        // eentity = EntityManager.Instance.CreateEntity(4, 24);//
        // eentity = EntityManager.Instance.CreateEntity(4, 25);//
        // eentity = EntityManager.Instance.CreateEntity(4, 26);//
        // eentity = EntityManager.Instance.CreateEntity(4, 29);//
        // eentity = EntityManager.Instance.CreateEntity(4, 30);//
        // eentity = EntityManager.Instance.CreateEntity(4, 31);//
        eentity = EntityManager.Instance.CreateEntity(4, 33);//
        // eentity = EntityManager.Instance.CreateEntity(8, 32);
        // eentity = EntityManager.Instance.CreateEntity(8, 43);
    }

    public void ChangePlayer(int idx)
    {
        if(changeCD)return;
        if (playerDatas.TryGetValue(selectedCharacterList[idx], out var data) && data.dead) return;
        if(timer != null)TimerManager.Instance.RemoveTimer(timer);
        timer = TimerManager.Instance.RegisterTimer(0.5f,1,ChangePlayerCD);
        if(!playerDatas.TryGetValue(nowSelectedCharacter,out data))
        {
            data = new CharacterDataCache();
            playerDatas.Add(nowSelectedCharacter, data);
            data.id = nowSelectedCharacter;
        }
        data.hp = characterDataComponent.nowHp;
        data.ELock = skillComponent.data[SkillType.ESKILL].isLock;
        data.ShiftLock = skillComponent.data[SkillType.QSKILL].isLock;
        data.dead = data.hp<=0;


        nowSelectedCharacter = selectedCharacterList[idx];
        foreach(var cmp in entity.components)
        {
            cmp.DataInit();
        }
        changeCD = true;
        if (!playerDatas.TryGetValue(nowSelectedCharacter, out data)) return;
        characterDataComponent.nowHp = data.hp;
        skillComponent.data[SkillType.ESKILL].isLock = data.ELock;
        skillComponent.data[SkillType.QSKILL].isLock = data.ShiftLock;
    }
    private void ChangePlayerCD()
    {
        changeCD = false;
    }
    public void Clear()
    {
        selectedCharacterList.Clear();
        characterLevelDir.Clear();
        playerDataCache.Clear();
        playerDatas.Clear();
    }
}

