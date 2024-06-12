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
        if(this.gameObject == null)return;
        animator.SetFloat("x", x);
        animator.SetFloat("y", y);
    }

    public void SetStateAnime(string animationName)
    {
        if(this.gameObject == null)return;
        animator.Play(animationName);
        nowPlayAnim = animationName;
    }

    public bool CheckAnime(string animeName)
    {
        if(this.gameObject == null)return false;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �����ǰ��������ʱ�䳬���˶������ȣ�˵�������Ѿ��������
        if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsName(animeName))
        {
            return true;
        }
        return false;
    }

    public bool CheckDestroyAnime()
    {
        if(this.gameObject == null)return false;
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
