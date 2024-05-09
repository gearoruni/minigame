using cfg.Character;
using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : Component
{
    public int CharacterId;
    public CharacterConfigs configs;
    public int level = 1;

    public GoComponent goComponent;
    public Dictionary<int, int> weaponDir = new Dictionary<int, int>();
    public override void Init()
    {
        CharacterId = dataDefind;
        if(CharacterId == 0)
        {
            CharacterId = PlayerBaseData.Instance.nowSelectedCharacter;
            level = PlayerBaseData.Instance.characterLevelDir[CharacterId];
        }

        configs = TableDataManager.Instance.tables.CharacterDefine.DataMap[CharacterId];

        goComponent = (GoComponent)entity.GetComponent("GoComponent");
        goComponent.CreateGameObject(configs.Id.ToString());

        for (int i = 0; i < configs.Level.Count; i++)
        {
            weaponDir.Add(configs.Level[i], configs.WeaponId[i]);
        }
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<CharacterComponent>(this);
    }
}
