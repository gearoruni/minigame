//using Cysharp.Threading.Tasks;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class PlayerManager : BaseManager
//{

//    // ������ɫ����
//    public GameObject playerRoot;
//    public GameObject playerGo;
//    public Character character;
//    public CharacterData characterData;

//    //������������
//    public GameObject weaponRoot;
//    public GameObject weaponGo;
//    public Weapon weapon;
//    public WeaponData weaponData;

//    //�������
//    public Dictionary<int, int> characterLevelDir = new Dictionary<int, int>();
//    public int currentSelectedIdx = 0;
//    public int currentSelectedId = 0;
//    public List<int> teamCharacterId = new List<int>() ;
    
//    public void SetData()
//    {
//        characterData = character.Init(currentSelectedId);
//        weaponData = weapon.Init(characterData.weaponDir[characterLevelDir[currentSelectedId]]);
//    }
//    public void SetPrefabInstance()
//    {
//        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PathUtils.GetCharacterPrefabFromID(currentSelectedId));
//        GameObject go = ObjectPoolManager.Instance.GetPrefabInstance(currentSelectedId, prefab);
//        go.transform.SetParent(playerRoot.transform, false);
//        playerGo = go;

//        prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PathUtils.GetWeaponPrefabFromID(weaponData.id));
//        go = ObjectPoolManager.Instance.GetPrefabInstance(weaponData.id, prefab);
//        go.transform.SetParent(weaponRoot.transform, false);
//        weaponGo = go;
//    }
//    bool BaseManager.Init()
//    {
//        //��װ��ʼ��һ�£�����Ӧ�ö���ұ��������
//        teamCharacterId.Add(1001);
//        characterLevelDir.Add(1001, 1);
//        teamCharacterId.Add(1002);
//        characterLevelDir.Add(1002, 1);

//        currentSelectedId = teamCharacterId[currentSelectedIdx];

//        playerRoot = GameObject.Find("Player");
//        character = playerRoot.GetComponent<Character>();
        
//        weaponRoot = GameObject.Find("Weapon");
//        weapon = weaponRoot.GetComponent<Weapon>();

//        SetData();

//        SetPrefabInstance();

//        return true;
//    }

//    UniTask<bool> BaseManager.InitAysnc()
//    {
        
//        return UniTask.FromResult(true);
//    }

//    void BaseManager.Release()
//    {
        
//    }
//}
