using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    Entity entity1;

    public void Start()
    {
        entity1 = EntityManager.Instance.CreateEntity(1001);
    }
    public void Create()
    {
        TransformComponent transformComponent = (TransformComponent)entity1.GetComponent("TransformComponent");
        transformComponent.position.x += 1;
    }

}
