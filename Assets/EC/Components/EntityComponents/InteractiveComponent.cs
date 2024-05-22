using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveComponent : Component
{
    private bool isInteracting;
    private CollisionComponent collisionComponent;
    private GameObject interactiveGO;
    public override void Init()
    {
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        collisionComponent.OnBaseTriggerEnter2D += ChangeState;
        collisionComponent.OnBaseTriggerExit2D += Exit;
        interactiveGO = GameObject.Instantiate(Preloader.Instance.GetGameObject("9999"));
        interactiveGO.SetActive(false);
    }

    public override void OnCache()
    {
        base.OnCache();
        collisionComponent.OnBaseTriggerEnter2D -= ChangeState;
        collisionComponent.OnBaseTriggerExit2D -= Exit;
    }

    private void ChangeState(Entity entity)
    {
        if (entity == null || entity.Tag != Tag.Player) return;
        isInteracting = true;
        
        interactiveGO.SetActive(true);
        interactiveGO.transform.position = this.entity.go.transform.position + new Vector3(0, 0.5f, 0); 
    }

    private void Exit(Entity entity)
    {
        isInteracting = false;
        interactiveGO.SetActive(false);
    }
}
