using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerBaseData : Singleton<PlayerBaseData>
{
    public int nowSelectedCharacter = 1001;
    public List<int> selectedCharacterList = new List<int>();
    public bool changeCD=false;
    private Timer timer;
    public Dictionary<int,int>characterLevelDir = new Dictionary<int,int>();
    public Dictionary<int, Dictionary<string,float>> playerDataCache = new Dictionary<int, Dictionary<string, float>>();
    public Entity entity;
    public override void Init()
    {
        characterLevelDir[1001] = 1;
        characterLevelDir[1002] = 1;
        selectedCharacterList.Add(1001);
        selectedCharacterList.Add(1002);
        playerDataCache.Add(1001,new Dictionary<string, float>());
        playerDataCache.Add(1002,new Dictionary<string, float>());
        entity = EntityManager.Instance.CreateEntity(1,1);
        CameraManager.Instance.RegisterFollow(entity);
        TagComponent tagComponent1 = (TagComponent)entity.GetComponent("TagComponent");
        //GameObject.Instantiate(Preloader.Instance.GetGameObject("Map"));

        Entity eentity = EntityManager.Instance.CreateEntity(4, 13);
        eentity = EntityManager.Instance.CreateEntity(9, 53);
        //eentity = EntityManager.Instance.CreateEntity(8, 34);
        //eentity = EntityManager.Instance.CreateEntity(4, 3);
        //eentity = EntityManager.Instance.CreateEntity(4, 4);
        // eentity = EntityManager.Instance.CreateEntity(8, 5);
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
        // eentity = EntityManager.Instance.CreateEntity(4, 33);//
        // eentity = EntityManager.Instance.CreateEntity(8, 32);
        // eentity = EntityManager.Instance.CreateEntity(8, 43);
    }

    public void ChangePlayer(int idx)
    {
        if(changeCD)return;
        if(timer != null)TimerManager.Instance.RemoveTimer(timer);
        timer = TimerManager.Instance.RegisterTimer(0.5f,1,ChangePlayerCD);
        float hp=0,e=-1,shift=-1;
        playerDataCache[nowSelectedCharacter]["hp"] = ((CharacterDataComponent)entity.GetComponent("CharacterDataComponent")).nowHp;
        playerDataCache[nowSelectedCharacter]["e"] = ((SkillComponent)entity.GetComponent("SkillComponent")).data[SkillType.ESKILL].isLock == true ? 1 : 0;
        playerDataCache[nowSelectedCharacter]["shift"] = ((SkillComponent)entity.GetComponent("SkillComponent")).data[SkillType.QSKILL].isLock == true ? 1 : 0;
        
        nowSelectedCharacter = selectedCharacterList[idx];
        
        playerDataCache[nowSelectedCharacter].TryGetValue("hp",out hp);
        playerDataCache[nowSelectedCharacter].TryGetValue("e",out e);
        playerDataCache[nowSelectedCharacter].TryGetValue("shift",out shift);
        // e = playerDataCache["e"];
        // shift = playerDataCache["shift"];
        foreach(var cmp in entity.components)
        {
            cmp.DataInit();
        }
        changeCD = true;
        if(hp==0f)return;
        ((CharacterDataComponent)entity.GetComponent("CharacterDataComponent")).nowHp = (int)hp;
        ((SkillComponent)entity.GetComponent("SkillComponent")).data[SkillType.ESKILL].isLock = e==1?true:false;
        ((SkillComponent)entity.GetComponent("SkillComponent")).data[SkillType.QSKILL].isLock = shift == 1?true:false;
    }
    private void ChangePlayerCD()
    {
        changeCD = false;
    }
}

