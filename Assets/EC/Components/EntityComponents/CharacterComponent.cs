using cfg.Character;
using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : Component
{

    public CharacterConfigs configs;
    public int level = 1;

    public GoComponent goComponent;
    public StateComponent stateComponent;
    CharacterDataComponent characterDataComponent;
    public Dictionary<int, int> weaponDir = new Dictionary<int, int>();
    public override void Init()
    {

        goComponent = (GoComponent)entity.GetComponent("GoComponent");

        stateComponent = (StateComponent)entity.GetComponent("StateComponent");
        characterDataComponent = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");

    }
    public override void DataInit()
    {
        int dataDefine;
        if (entity.componentDatas.TryGetValue("CharacterComponent", out dataDefine))
        {
            configs = TableDataManager.Instance.tables.CharacterDefine.DataMap[dataDefine == 0 ? PlayerBaseData.Instance.nowSelectedCharacter:dataDefine];
        }

        for (int i = 0; i < configs.Level.Count; i++)
        {
            weaponDir.Add(configs.Level[i], configs.WeaponId[i]);
        }

        //characterDataComponent.SetHealth(configs.Hp); 
        //goComponent.CreateGameObject(configs.Id.ToString());
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<CharacterComponent>(this);
    }
}
