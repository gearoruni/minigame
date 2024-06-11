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
        if (followTransform != null)
        {
            Collider2D collider = PhysicsRay.GetBindBox(followTransform.position);
            if (collider != null)
            {
                confiner.m_BoundingShape2D = collider;
            }
        }
    }

}
