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
    private List<bool> txt = new List<bool>(){true,true,true,true,true,true};
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
        int idx = GetBindBox();
        if(idx == 10 && txt[0])
        {
            BattleUI.Instance.ShowTxt(7030,()=>{BattleUI.Instance.ShowPiaozi("幻想之林");});
            txt[0] = false;
        }
        else if(idx == 12 && txt[1])
        {
            BattleUI.Instance.ShowTxt(7038,()=>{BattleUI.Instance.ShowPiaozi("悲泣海岛");});
            txt[1] = false;
        }
        else if(idx == 13 && txt[2])
        {
            BattleUI.Instance.ShowTxt(7041);
            txt[2] = false;
        }else if(idx == 14 && txt[3])
        {
            BattleUI.Instance.ShowTxt(7045);
            txt[3] = false;
        }else if(idx == 16 && txt[4])
        {
            BattleUI.Instance.ShowTxt(7049);
            txt[4] = false;
        }
        else if(idx == 4&& txt[5])
        {
            BattleUI.Instance.ShowPiaozi("怒火之林");
            txt[5] = false;
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
                int idx = int.Parse(collider.name);
                EntityManager.Instance.AwakeMonsterByBindBox(idx);
                
                //进入boss领地播剧情
                if(idx == 8)TimerManager.Instance.RegisterTimer(0.5f,1,()=>{BattleUI.Instance.ShowTxt(7020);});
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

    public int GetBindBox()
    {
            Collider2D collider = PhysicsRay.GetBindBox(followTransform.position);
            if(int.TryParse(collider.name,out int result))
            {
                return result;
            }
            return -1;
    }
}
