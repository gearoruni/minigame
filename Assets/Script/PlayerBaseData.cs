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
    Entity eentity = EntityManager.Instance.CreateEntity(4, 2);//SpawnDataID��2 ��ɫʷ��ķ
        // for (int a = 3; a <= 14; a++)//����ĸ�������ɫʷ��ķ,CharacterComponentDara/SpawnDataID��3~14
        // {//CharacterComponentDaraID��SpawnDataID���
        //     eentity = EntityManager.Instance.CreateEntity(4, a);
        // }
        // for (int b = 15; b <= 24; b++)//������С�����,CharacterComponentDara/SpawnDataID��15~24.
        // {//����ɫʷ��ķһ�����ṥ�������д�����Ѫ
        //     eentity = EntityManager.Instance.CreateEntity(4, b);
        // }
        // for (int b = 25; b <= 38; b++)//��ŭʷ��ķ��ID��25~34
        // {
        //     eentity = EntityManager.Instance.CreateEntity(4, b);
        // }
        // for (int b = 35; b <= 44; b++)//����ʷ��ķ��ID��35~44����ˮ��ķ�ŭʷ��ķ(��Ƥ)
        // {
        //     eentity = EntityManager.Instance.CreateEntity(4, b);
        // }
        // for (int b = 45; b <= 49; b++)//����(��̹�)��ID��45~49
        // {
        //     eentity = EntityManager.Instance.CreateEntity(4, b);
        // }
        // for (int b = 50; b <= 54; b++)//����ˮ��(����ˮ�ݹ�)��ID��50~54
        // {
        //     eentity = EntityManager.Instance.CreateEntity(4, b);
        // }
        // for (int b = 55; b <= 63; b++)//�������(����С�ֵ�),ID��55~63
        // {
        //     eentity = EntityManager.Instance.CreateEntity(4, b);
        // }
        // eentity = EntityManager.Instance.CreateEntity(4, 64);//����1-8 BOSS��ŭ֮��
        eentity = EntityManager.Instance.CreateEntity(4, 65);//����2-9 BOSS����֮��

        //for (int b = 66; b <= 76; b++)//��ʯ��(�����Ǳ��ѵ��ɻ���) ID��66~76
        //{
        //    eentity = EntityManager.Instance.CreateEntity(999, b);//��1������������Ҳ�ȷ��
        //}
        //for (int b = 77; b <= 80; b++)//��ʯ��(�����Ǳ��ѵ��ɻ���) ID��77~80
        //{
        //    eentity = EntityManager.Instance.CreateEntity(999, b);//��1������������Ҳ�ȷ��
        //}
        //for (int b = 81; b <= 91; b++)//��ѪĢ�� ID��81~91
        //{
        //    eentity = EntityManager.Instance.CreateEntity(999, b);//��1������������Ҳ�ȷ��
        //}
        //for (int b = 92; b <= 94; b++)//����Կ��
        //{
        //    eentity = EntityManager.Instance.CreateEntity(999, b);//��1������������Ҳ�ȷ��
        //}
        //eentity = EntityManager.Instance.CreateEntity(999, 95);//����1-6 ��ŭ����  ��BOSSսǰ�ĵ���ʯ��
        //eentity = EntityManager.Instance.CreateEntity(999, 96);//����2-8 ���˵���  ��������������ײ�壬������Ҫ�������ɰ�����ʯ��ȥ��
        for (int b = 97; b <= 102; b++)//����ʯ��
        {
            eentity = EntityManager.Instance.CreateEntity(8, b);
        }
        // for (int b = 103; b <= 139; b++)//�������
        // {
        //     eentity = EntityManager.Instance.CreateEntity(9, b);
        // }
        //for (int b = 140; b <= 149; b++)//��������
        //{
        //    eentity = EntityManager.Instance.CreateEntity(999, b);//��1������������Ҳ�ȷ��
        //}
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

