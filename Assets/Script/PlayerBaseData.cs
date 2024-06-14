using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CharacterDataCache
{
    public int id;
    public int maxHp;
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
    // public Dictionary<int, Dictionary<string,float>> playerDataCache = new Dictionary<int, Dictionary<string, float>>();
    public Dictionary<int, CharacterDataCache> playerDatas = new Dictionary<int, CharacterDataCache>();
    public Entity entity;

    private CharacterDataComponent characterDataComponent;
    private SkillComponent skillComponent;

    public int LastSave;
    public override void Init()
    {
        
        characterLevelDir[1001] = 1;
        characterLevelDir[1002] = 1;
        selectedCharacterList.Add(1001);
        // selectedCharacterList.Add(1002);
        // playerDataCache.TryAdd(1001,new Dictionary<string, float>());
        // playerDataCache.TryAdd(1002,new Dictionary<string, float>());
        entity = EntityManager.Instance.CreateEntity(1,1);
        CameraManager.Instance.RegisterFollow(entity);
        TagComponent tagComponent1 = (TagComponent)entity.GetComponent("TagComponent");
        characterDataComponent = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");
        skillComponent = (SkillComponent)entity.GetComponent("SkillComponent");
        //GameObject.Instantiate(Preloader.Instance.GetGameObject("Map"));

    //     Entity eentity = EntityManager.Instance.CreateEntity(4, 13);
    // //    eentity = EntityManager.Instance.CreateEntity(9, 53);
    //     eentity = EntityManager.Instance.CreateEntity(8, 34);
    //     eentity = EntityManager.Instance.CreateEntity(4, 3);
    //     eentity = EntityManager.Instance.CreateEntity(4, 4);
    //     eentity = EntityManager.Instance.CreateEntity(8, 5);
    //     eentity = EntityManager.Instance.CreateEntity(4, 7);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 8);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 9);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 10);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 11);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 12);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 13);
    //     eentity = EntityManager.Instance.CreateEntity(4, 14);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 15);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 16);//
    //     eentity = EntityManager.Instance.CreateEntity(8, 19);
    //     eentity = EntityManager.Instance.CreateEntity(4, 20);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 22);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 23);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 24);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 25);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 26);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 29);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 30);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 31);//
    //     eentity = EntityManager.Instance.CreateEntity(4, 33);//
    //     eentity = EntityManager.Instance.CreateEntity(8, 32);
    //     eentity = EntityManager.Instance.CreateEntity(8, 43);
    //     for(int i = 105;i<142;i++)
    //     {
            
    //          eentity = EntityManager.Instance.CreateEntity(9, i);
    //     }
    
        Entity eentity = EntityManager.Instance.CreateEntity(4, 2);//SpawnDataID：2 紫色史莱姆
        for (int a = 3; a <= 4; a++)//其余的各地区紫色史莱姆,CharacterComponentDara/SpawnDataID：3~14
        {//CharacterComponentDaraID与SpawnDataID相等
            eentity = EntityManager.Instance.CreateEntity(4, a);
        }
        for (int a = 8; a <= 10; a++)//其余的各地区紫色史莱姆,CharacterComponentDara/SpawnDataID：3~14
        {//CharacterComponentDaraID与SpawnDataID相等
            eentity = EntityManager.Instance.CreateEntity(4, a);
        }
        for (int b = 15; b <= 24; b++)//各地区小软泥怪,CharacterComponentDara/SpawnDataID：15~24.
        {//和紫色史莱姆一样不会攻击，仅有触碰掉血
            eentity = EntityManager.Instance.CreateEntity(4, b);
        }
        for (int b = 25; b <= 34; b++)//愤怒史莱姆，ID：25~34
        {
            eentity = EntityManager.Instance.CreateEntity(4, b);
        }
        for (int b = 35; b <= 44; b++)//悲伤史莱姆，ID：35~44，吐水球的愤怒史莱姆(换皮)
        {
            eentity = EntityManager.Instance.CreateEntity(4, b);
        }
        for (int b = 45; b <= 49; b++)//火灵(冲刺怪)，ID：45~49
        {
            eentity = EntityManager.Instance.CreateEntity(4, b);
        }
        for (int b = 50; b <= 54; b++)//悲伤水灵(蓄力水泡怪)，ID：50~54
        {
            eentity = EntityManager.Instance.CreateEntity(4, b);
        }
        for (int b = 55; b <= 63; b++)//大软泥怪(会召小怪的),ID：55~63
        {
            eentity = EntityManager.Instance.CreateEntity(4, b);
        }
        eentity = EntityManager.Instance.CreateEntity(4, 64);//场景1-8 BOSS愤怒之花
        eentity = EntityManager.Instance.CreateEntity(4, 65);//场景2-9 BOSS悲伤之花

        // eentity = EntityManager.Instance.CreateEntity(10, 153);


        for (int b = 66; b <= 76; b++)//横石堆(用主角爆裂弹可击碎) ID：66~76
        {
           eentity = EntityManager.Instance.CreateEntity(10, b);//第1个参数填多少我不确定
        }
        // for (int b = 77; b <= 80; b++)//竖石堆(用主角爆裂弹可击碎) ID：77~80
        // {
        //    eentity = EntityManager.Instance.CreateEntity(999, b);//第1个参数填多少我不确定
        // }
        for (int b = 81; b <= 91; b++)//回血蘑菇 ID：81~91
        {
           eentity = EntityManager.Instance.CreateEntity(8, b);//第1个参数填多少我不确定
        }
        for (int b = 92; b <= 94; b++)//开门钥匙
        {
           eentity = EntityManager.Instance.CreateEntity(8, b);//第1个参数填多少我不确定
        }
        // // eentity = EntityManager.Instance.CreateEntity(999, 95);//场景1-6 愤怒雕塑  是BOSS战前的雕塑石像
        // // eentity = EntityManager.Instance.CreateEntity(999, 96);//场景2-8 悲伤雕塑  两个雕塑是有碰撞体，可能需要交互，可按爱心石像去做
        for (int b = 97; b <= 102; b++)//爱心石像
        {
            eentity = EntityManager.Instance.CreateEntity(8, b);
        }
        for (int b = 103; b <= 139; b++)//尖刺陷阱
        {
            eentity = EntityManager.Instance.CreateEntity(9, b);
        }
        eentity = EntityManager.Instance.CreateEntity(9, 151);
        // for (int b = 140; b <= 149; b++)//定向陷阱
        // {
        //    eentity = EntityManager.Instance.CreateEntity(4, b);//第1个参数填多少我不确定
        // }
    }

    public void ChangePlayer(int idx)
    {
        if(CheckReBirth()){ReStartToKaimi();return;}
        if(changeCD || selectedCharacterList.Count <= idx)return;
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
        data.maxHp = characterDataComponent.maxHp;
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
        // playerDataCache.Clear();
        playerDatas.Clear();
    }

    public void ReBirth()
    {
        foreach(var characterData in playerDatas.Values)
        {
            characterData.hp = characterData.maxHp;
            characterData.dead = false;
        }
        characterDataComponent.nowHp = characterDataComponent.maxHp;
    }

    public void ReStartToKaimi()
    {
        ReBirth();
        CameraManager.Instance.CloseFengsuo();
        EntityManager.Instance.SleepMonster();
        BattleUI.Instance.BossHp.transform.parent.gameObject.SetActive(false);
        var go = GameObject.Find($"Map/ReBirth/{LastSave}");
        var cmp = (TransformComponent)entity.GetComponent("TransformComponent");
        cmp.SetPostion(go.transform.position.x, go.transform.position.y);
    }

    public bool CheckReBirth()
    {
        int count= selectedCharacterList.Count;;
        bool rebirth = true;
        foreach(var playerData in playerDatas)
        {
            if(playerData.Value.dead == false)
            {
                rebirth = false;
            }
            count--;
        }
        if(characterDataComponent.nowHp > 0)rebirth = false;
        if(count>1)rebirth = false;
        return rebirth;
    }
}

