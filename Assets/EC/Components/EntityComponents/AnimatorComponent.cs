using cfg;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class AnimatorComponent : Component
{
    public AnimationData animationData;

    public AnimatorListener listener;
    public override void Init()
    {
        animationData = TableDataManager.Instance.tables.AnimationDefine.Get(dataDefind);

        GoComponent goComponent = (GoComponent)entity.GetComponent("GoComponent");
        //Ìí¼Ó¶¯»­
        if (entity.componentNameToIdx.ContainsKey("AnimatorComponent") && goComponent.go.GetComponent<AnimatorListener>() == null)
        {
            goComponent.go.AddComponent<AnimatorListener>();
        }
        AnimatorListener listener = goComponent.go.GetComponent<AnimatorListener>();
 
        BindAnimator(listener);
        listener.Init(animationData.AnimationName, entity);
    }

    public void BindAnimator(AnimatorListener listener)
    {
        this.listener = listener;
    }

    public void PlayerAnime(string animeName,float playTime)
    {

    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<AnimatorComponent>(this);
    }
}
