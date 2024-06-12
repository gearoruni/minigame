using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public Camera mainCamera;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineConfiner2D confiner;

    public GameObject followRoot;
    public TransformComponent followTransform;
    public List<GameObject> fengsuo;
    public int nowfengsuo;
    public void RegisterFollow(Entity entity)
    {
        followTransform = (TransformComponent)entity.GetComponent("TransformComponent");
    }
    private void Update()
    {
        if (followTransform != null)
        {
            followRoot.transform.position = followTransform.position;
            UpdateBindBox();
        }
    }

    private void UpdateBindBox()
    {
        if(EntityManager.Instance.activeMonsters.Count > 0 && confiner.m_BoundingShape2D!= null)return;
        if (followTransform != null)
        {
            Collider2D collider = PhysicsRay.GetBindBox(followTransform.position);
            if (collider != null && (confiner.m_BoundingShape2D==null || confiner.m_BoundingShape2D != collider))
            {
                confiner.m_BoundingShape2D = collider;
                EntityManager.Instance.AwakeMonsterByBindBox(int.Parse(collider.name));
            }
        }
    }

    public void CloseFengsuo()
    {
        foreach(var go in fengsuo)
        {
           
            go.SetActive(false);
        }
    }
    public void AwakeFengsuo()
    {
        int idx = int.Parse(confiner.m_BoundingShape2D.name);
        int name;
        foreach(var go in fengsuo)
        {
            name = int.Parse(go.name);
            go.SetActive(name == idx || name == idx-1);
        }
    }
}
