using cfg.Character;
using cfg;

using UnityEngine;
using System;
using UnityEditor;
using System.Collections.Generic;

public class WeaponComponent : Component
{
    public int weaponId;
    public Entity weapon;
    CharacterComponent character;
    TransformComponent characterTransform;

    public TransformComponent weaponTransform;
    GoComponent WeaponGo;

    ControllerComponent controller;
    StateComponent state;

    //默认武器信息
    //没有配置，写死radis
    public float radis = 0.4f;

    public override void Init()
    {
        //基础数据
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");
        characterTransform = (TransformComponent)entity.GetComponent("TransformComponent");
        controller = (ControllerComponent)entity.GetComponent("ControllerComponent");
        state = (StateComponent)entity.GetComponent("StateComponent");

    }

    public override void DataInit()
    {
        weaponId = character.weaponDir[character.level];
        if (weapon != null)
        {
            // GameObject.Destroy(weapon.go);
            ObjectPool.Instance.ReturnObjectToPool(WeaponGo.goName, WeaponGo.go);
            WeaponGo.go = null;
            weapon = null;
        }
        //创建weapon entity
        weapon = EntityManager.Instance.ParentCreateEntity(entity, 2, 0);

        weaponTransform = (TransformComponent)weapon.GetComponent("TransformComponent");
        weaponTransform.position = characterTransform.position;
        WeaponGo = (GoComponent)weapon.GetComponent("GoComponent");
        WeaponGo.CreateGameObject(weaponId.ToString());
    }
    public Vector2 GetWeaponFace()
    {
        // Debug.Log($"{weaponTransform.position},{weaponTransform.rotation},,{WeaponGo.go.transform.up},{WeaponGo.go.transform.rotation}");
        if(WeaponGo.go == null)return Vector2.zero;
        return WeaponGo.go.transform.up;
    }
    public Vector2 GetWeaponPos()
    {
        return weaponTransform.position;
    }

    public void SetWeaponTransform(Vector3 pos)
    {
        if (weaponTransform == null) return;
        weaponTransform.SetRotationLookAt(pos, characterTransform.position);

        Vector2 dir = pos - characterTransform.position;
        dir = dir.normalized * radis;
        weaponTransform.position.x = characterTransform.position.x;
        weaponTransform.position.y = characterTransform.position.y - 0.2f;
        weaponTransform.SetPostionOffset(dir.x, dir.y);
    }
    public Quaternion GetRotation()
    {
        return weaponTransform.rotation;
    }
    public Vector2 GetWeaponTopPos()
    {
        Vector2 pos = weaponTransform.position;
        Vector2 dir = GetWeaponFace().normalized * 0.5f;
        return pos + dir;
    }
    public override void Update()
    {
        if (controller != null)
        {
            SetWeaponTransform(controller.facepos);
        }
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<WeaponComponent>(this);
    }
}
