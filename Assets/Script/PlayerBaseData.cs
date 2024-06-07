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

    public Entity entity;
    public override void Init()
    {
        characterLevelDir[1001] = 1;
        characterLevelDir[1002] = 1;
        selectedCharacterList.Add(1001);
        selectedCharacterList.Add(1002);
        entity = EntityManager.Instance.CreateEntity(1,1);
        CameraManager.Instance.RegisterFollow(entity);
        TagComponent tagComponent1 = (TagComponent)entity.GetComponent("TagComponent");
        //Ê¹ÓÃ4ºÅÄ£°æ ´´½¨Ò»¸öcomponent Êý¾ÝÎª2µÄÖµ
        //GameObject.Instantiate(Preloader.Instance.GetGameObject("Map"));

        Entity eentity = EntityManager.Instance.CreateEntity(4, 13);
        //eentity = EntityManager.Instance.CreateEntity(8, 34);
        //eentity = EntityManager.Instance.CreateEntity(4, 3);
        //eentity = EntityManager.Instance.CreateEntity(4, 4);
        eentity = EntityManager.Instance.CreateEntity(8, 5);
        eentity = EntityManager.Instance.CreateEntity(4, 7);//³¡¾°3-×ÏÊ·À³Ä·1
        eentity = EntityManager.Instance.CreateEntity(4, 8);//³¡¾°3-×ÏÊ·À³Ä·2
        eentity = EntityManager.Instance.CreateEntity(4, 9);//³¡¾°3-×ÏÊ·À³Ä·3
        //eentity = EntityManager.Instance.CreateEntity(4, 10);//³¡¾°3-×ÏÊ·À³Ä·4
        //eentity = EntityManager.Instance.CreateEntity(4, 11);//³¡¾°3-×ÏÊ·À³Ä·5
        //eentity = EntityManager.Instance.CreateEntity(4, 12);//³¡¾°3-×ÏÊ·À³Ä·6
        //eentity = EntityManager.Instance.CreateEntity(4, 13);//³¡¾°3-·ßÅ­Ê·À³Ä·1
        //eentity = EntityManager.Instance.CreateEntity(4, 14);//³¡¾°3-·ßÅ­Ê·À³Ä·2
        //eentity = EntityManager.Instance.CreateEntity(4, 15);//³¡¾°3-·ßÅ­Ê·À³Ä·3
        //eentity = EntityManager.Instance.CreateEntity(4, 16);//³¡¾°3-·ßÅ­Ê·À³Ä·4
          eentity = EntityManager.Instance.CreateEntity(8, 19);
        //eentity = EntityManager.Instance.CreateEntity(4, 20);//³¡¾°4-»ðÁé
        //eentity = EntityManager.Instance.CreateEntity(4, 22);//³¡¾°5-»ðÁé
        //eentity = EntityManager.Instance.CreateEntity(4, 23);//³¡¾°5-·ßÅ­Ê·À³Ä·1
        //eentity = EntityManager.Instance.CreateEntity(4, 24);//³¡¾°5-·ßÅ­Ê·À³Ä·2
        //eentity = EntityManager.Instance.CreateEntity(4, 25);//³¡¾°5-·ßÅ­Ê·À³Ä·3
        //eentity = EntityManager.Instance.CreateEntity(4, 26);//³¡¾°5-·ßÅ­Ê·À³Ä·4
        //eentity = EntityManager.Instance.CreateEntity(4, 29);//³¡¾°6-»ðÁé
        //eentity = EntityManager.Instance.CreateEntity(4, 30);//³¡¾°6-»ðÁé
        //eentity = EntityManager.Instance.CreateEntity(4, 31);//³¡¾°6-»ðÁé
        //eentity = EntityManager.Instance.CreateEntity(4, 33);//³¡¾°6-»ðÁé
    }

    public void ChangePlayer(int idx)
    {
        if(changeCD)return;
        if(timer != null)TimerManager.Instance.RemoveTimer(timer);
        timer = TimerManager.Instance.RegisterTimer(0.5f,1,ChangePlayerCD);
        nowSelectedCharacter = selectedCharacterList[idx];
        foreach(var cmp in entity.components)
        {
            cmp.DataInit();
        }
        changeCD = true;
    }
    private void ChangePlayerCD()
    {
        changeCD = false;
    }
}

