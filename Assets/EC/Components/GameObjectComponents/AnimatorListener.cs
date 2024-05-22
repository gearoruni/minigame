using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatorListener : MonoBehaviour
{
    public float x;
    public float y;

    Animator animator;

    RuntimeAnimatorController runcontroller;
    public bool isEnd;
    public string nowPlayAnim;
    public void Init(string animationName,Entity entity)
    {
        
        animator = this.GetComponent<Animator>();
        if(animator == null)
        {
            animator = this.AddComponent<Animator>();
        }

        runcontroller = Resources.Load<RuntimeAnimatorController>(PathUtils.GetResAnimationController(animationName));
        animator.runtimeAnimatorController = runcontroller;

    }

    public void SetParam(float x, float y)
    {
        animator.SetFloat("x", x);
        animator.SetFloat("y", y);
    }

    public void SetStateAnime(string animationName)
    {
        animator.Play(animationName);
        nowPlayAnim = animationName;
    }
    public bool CheckDestroyAnime()
    {
        // ��⵱ǰ����״̬��Ϣ
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �����ǰ��������ʱ�䳬���˶������ȣ�˵�������Ѿ��������
        if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsName("Destroy"))
        {
            return true;
        }
        return false;
    }
}
