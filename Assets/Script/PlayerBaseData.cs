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
        //Ê¹ÓÃ4ºÅÄ£°æ ´´½¨Ò»¸öcomponent Êý¾Ý Îª 2µÄÖµ
        GameObject.Instantiate(Preloader.Instance.GetGameObject("Map"));

        Entity eentity = EntityManager.Instance.CreateEntity(4, 2);
        eentity = EntityManager.Instance.CreateEntity(4, 3);
        //eentity = EntityManager.Instance.CreateEntity(4, 4);
        //eentity = EntityManager.Instance.CreateEntity(4, 7);//³¡¾°3-×ÏÊ·À³Ä·1
        //eentity = EntityManager.Instance.CreateEntity(4, 8);//³¡¾°3-×ÏÊ·À³Ä·2
        //eentity = EntityManager.Instance.CreateEntity(4, 9);//³¡¾°3-×ÏÊ·À³Ä·3
        //eentity = EntityManager.Instance.CreateEntity(4, 10);//³¡¾°3-×ÏÊ·À³Ä·4
        //eentity = EntityManager.Instance.CreateEntity(4, 11);//³¡¾°3-×ÏÊ·À³Ä·5
        //eentity = EntityManager.Instance.CreateEntity(4, 12);//³¡¾°3-×ÏÊ·À³Ä·6
        //eentity = EntityManager.Instance.CreateEntity(4, 13);//³¡¾°3-·ßÅ­Ê·À³Ä·1
        //eentity = EntityManager.Instance.CreateEntity(4, 14);//³¡¾°3-·ßÅ­Ê·À³Ä·2
        //eentity = EntityManager.Instance.CreateEntity(4, 15);//³¡¾°3-·ßÅ­Ê·À³Ä·3
        //eentity = EntityManager.Instance.CreateEntity(4, 16);//³¡¾°3-·ßÅ­Ê·À³Ä·4
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
}

