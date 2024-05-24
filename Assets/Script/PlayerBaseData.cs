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
    
    public Dictionary<int,int>characterLevelDir = new Dictionary<int,int>();

    public Entity entity;
    public override void Init()
    {
        characterLevelDir[1001] = 1;
        characterLevelDir[1002] = 1;

        entity = EntityManager.Instance.CreateEntity(1,1);
        CameraManager.Instance.RegisterFollow(entity);
        TagComponent tagComponent1 = (TagComponent)entity.GetComponent("TagComponent");
        //ʹ��4��ģ�� ����һ��component ���� Ϊ 2��ֵ
        GameObject.Instantiate(Preloader.Instance.GetGameObject("Map"));

        Entity eentity = EntityManager.Instance.CreateEntity(4, 2);
        eentity = EntityManager.Instance.CreateEntity(4, 3);
        //eentity = EntityManager.Instance.CreateEntity(4, 4);
        //eentity = EntityManager.Instance.CreateEntity(4, 7);//����3-��ʷ��ķ1
        //eentity = EntityManager.Instance.CreateEntity(4, 8);//����3-��ʷ��ķ2
        //eentity = EntityManager.Instance.CreateEntity(4, 9);//����3-��ʷ��ķ3
        //eentity = EntityManager.Instance.CreateEntity(4, 10);//����3-��ʷ��ķ4
        //eentity = EntityManager.Instance.CreateEntity(4, 11);//����3-��ʷ��ķ5
        //eentity = EntityManager.Instance.CreateEntity(4, 12);//����3-��ʷ��ķ6
        //eentity = EntityManager.Instance.CreateEntity(4, 13);//����3-��ŭʷ��ķ1
        //eentity = EntityManager.Instance.CreateEntity(4, 14);//����3-��ŭʷ��ķ2
        //eentity = EntityManager.Instance.CreateEntity(4, 15);//����3-��ŭʷ��ķ3
        //eentity = EntityManager.Instance.CreateEntity(4, 16);//����3-��ŭʷ��ķ4
        //eentity = EntityManager.Instance.CreateEntity(4, 20);//����4-����
        //eentity = EntityManager.Instance.CreateEntity(4, 22);//����5-����
        //eentity = EntityManager.Instance.CreateEntity(4, 23);//����5-��ŭʷ��ķ1
        //eentity = EntityManager.Instance.CreateEntity(4, 24);//����5-��ŭʷ��ķ2
        //eentity = EntityManager.Instance.CreateEntity(4, 25);//����5-��ŭʷ��ķ3
        //eentity = EntityManager.Instance.CreateEntity(4, 26);//����5-��ŭʷ��ķ4
        //eentity = EntityManager.Instance.CreateEntity(4, 29);//����6-����
        //eentity = EntityManager.Instance.CreateEntity(4, 30);//����6-����
        //eentity = EntityManager.Instance.CreateEntity(4, 31);//����6-����
        //eentity = EntityManager.Instance.CreateEntity(4, 33);//����6-����
    }
}

