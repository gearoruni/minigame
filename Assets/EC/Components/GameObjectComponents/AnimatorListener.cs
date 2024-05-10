using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorListener : MonoBehaviour
{
    Animator animator;
    
    AnimatorController controller;
    public void Init(string animationName,Entity entity)
    {
        
        animator = this.GetComponent<Animator>();
        if(animator == null)
        {
            animator = this.AddComponent<Animator>();
        }
        controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(PathUtils.GetAnimationController(animationName));

        animator.runtimeAnimatorController = controller;
    }

    public void SetStateAnime(string animationName, float playTime)
    {

    }
}
