using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private SkillComponent skillCmp;
    private CharacterDataComponent characterData;
    private cfg.DialogueConfigs dialogueConfigs;
    public List<Image> maskList;
    public List<Image> lockList;

    public List<Image> characterSkill;
    public List<Image> lizhiSkill;
    public GameObject dialogUI;
    public Text nameTxt;
    public Text mainTxt;
    public Button next;
    public Image leftperson;
    public Image rightperson;
    public Image health;

    public Transform zhujuechange,lizhichange;

    public RectTransform montserRoot;
    public GameObject MonsterHpTemplate;

    public Image BossHp;

    public static BattleUI Instance;
    private Action callBack = null;
    private int nowPlayer = 1001;
    private PlayerBaseData playerBaseData;
    public GameObject txtBG;
    public Text tongyongTxt;
    public Dictionary<int,List<Entity>> activeMonsters;
    private void Awake()
    {
        Instance = this;
        var entitys = EntityManager.Instance.GetEntityFromEntityId(1);
        if(entitys.Count != 0)
        {
            var player = entitys[0];
            characterData = (CharacterDataComponent)player.GetComponent("CharacterDataComponent");
            skillCmp = (SkillComponent)player.GetComponent("SkillComponent");
        }
        
        next.onClick.AddListener(NextTxt);
        SoundManager.Instance.PlayBGM("场景1-3 草地区域");
        var monsterList = EntityManager.Instance.GetEntityFromEntityId(4);
        foreach(var monster in monsterList)
        {
            ChangeMonsterHp(monster.instanceId, true);
        }
    }

    private void Start()
    {
        ShowTxt(7001);
    }

    // Update is called once per frame
    void Update()
    {
        if(skillCmp==null || characterData == null)
        {
            var entitys = EntityManager.Instance.GetEntityFromEntityId(1);
            if(entitys.Count != 0)
             {   
                var player = entitys[0];
                characterData = (CharacterDataComponent)player.GetComponent("CharacterDataComponent");
                skillCmp = (SkillComponent)player.GetComponent("SkillComponent");
            }
        }
        if(skillCmp==null || characterData == null)return;
        foreach(var skill in skillCmp.data.Values)
        {
            Image img = maskList[(int)skill.Type];
            if(img == null)continue;
            lockList[(int)skill.Type].gameObject.SetActive(skill.isLock);
            float cd = 1 - skillCmp.nowCdtime[skill.idx] / skill.cd;
            img.fillAmount = skill.isLock == true ? 1:cd;
        }

        health.fillAmount = characterData.nowHp * 1.0f / characterData.maxHp;
        UpdateMonsterHp();
        playerBaseData = playerBaseData==null ? PlayerBaseData.Instance : playerBaseData;

        if(playerBaseData.selectedCharacterList.Count <= 1)
        {
            lizhichange.gameObject.SetActive(false);
            return;
        }
        lizhichange.gameObject.SetActive(true);
        if(nowPlayer != playerBaseData.nowSelectedCharacter)
        {
            nowPlayer = playerBaseData.nowSelectedCharacter;
            foreach(var img in characterSkill)
            {
                img.gameObject.SetActive(!img.gameObject.activeSelf);
            }
            foreach(var img in lizhiSkill)
            {
                img.gameObject.SetActive(!img.gameObject.activeSelf);
            }
            float x = nowPlayer == 1001 ? 0.7f:0.5f;
            zhujuechange.localScale = new Vector3(x,x,x);
            
            x = nowPlayer == 1002 ? 0.7f:0.5f;
            lizhichange.localScale = new Vector3(x,x,x);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void ShowTxt(int id, Action callback = null)
    {
        EntityManager.Instance.isStop = true;
        callBack = callBack == null ? callback : callBack;
        dialogUI.SetActive(true);
        dialogueConfigs = TableDataManager.Instance.tables.MainTxt.Get(id);
        nameTxt.text = dialogueConfigs.角色名称;
        mainTxt.text = dialogueConfigs.文本内容;
        //是主角
        if(dialogueConfigs.立绘id == 8001)
        {
            leftperson.color = Color.white;
            rightperson.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            if(rightperson.sprite != null)
            {
                Resources.UnloadAsset(rightperson.sprite);
            }
            rightperson.sprite = Resources.Load<Sprite>($"UI/立绘/{dialogueConfigs.立绘id}");
            rightperson.SetNativeSize();
            rightperson.gameObject.SetActive(true);
            rightperson.color = Color.white;
            leftperson.color = new Color(0.5f, 0.5f, 0.5f);
        }
        EntityManager.Instance.SetEntityController(false);
    }
    private void NextTxt()
    {
        if (dialogueConfigs.下一个 == 0)
        {
            dialogUI.SetActive(false);
            EntityManager.Instance.SetEntityController(true);
            Resources.UnloadAsset(rightperson.sprite);
            rightperson.gameObject.SetActive(false);
            callBack?.Invoke();
            callBack = null;

            EntityManager.Instance.isStop = false;
            return;
        }
        ShowTxt(dialogueConfigs.下一个);
    }


    private Stack<Image> SleepItem = new Stack<Image>();
    private Dictionary<int, Image> activeItem = new Dictionary<int, Image>();
    public void ChangeMonsterHp(int id,bool open)
    {
        if(open)
        {
            if(!SleepItem.TryPop(out Image image))
            {
                var go = GameObject.Instantiate(MonsterHpTemplate, montserRoot);
                image = go.GetComponentsInChildren<Image>()[1];
                image.fillMethod = Image.FillMethod.Horizontal;
            }
            activeItem.Add(id, image);
            image.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            if(activeItem.TryGetValue(id, out var image))
            {
                image.transform.parent.gameObject.SetActive(false);
                SleepItem.Push(image);
                activeItem.Remove(id);
                
                var entity = EntityManager.Instance.GetEntityFromInstanceId(id);
                if(entity == null)return;
                var character = (CharacterComponent)entity.GetComponent("CharacterComponent");
                int insid = character.configs.Id;
                if(insid == 1201 || insid == 1202)
                {
                    BossHp.transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }

    private void UpdateMonsterHp()
    {
        Entity entity;
        CharacterDataComponent characterData;
        Transform trs;
        Vector3 screenPos;
        foreach(var pairAndImage in activeItem)
        {
            entity = EntityManager.Instance.GetEntityFromInstanceId(pairAndImage.Key);
            if (entity == null) continue;

            characterData = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");
            var character = (CharacterComponent)entity.GetComponent("CharacterComponent");
            int id = character.configs.Id;
            string bindboxName = CameraManager.Instance.confiner.m_BoundingShape2D.name;
            //boss血条特殊处理
            if(id == 1201 && bindboxName == "8" || id == 1202 && bindboxName == "17")
            {
                
                pairAndImage.Value.transform.parent.gameObject.SetActive(false);
                BossHp.transform.parent.gameObject.SetActive(true);
                BossHp.fillAmount = characterData.nowHp * 1.0f / characterData.maxHp;
                continue;
            }
            // else
            // {
            //     BossHp.transform.parent.gameObject.SetActive(false);
            // }
            trs = entity.go.transform;

            pairAndImage.Value.fillAmount = characterData.nowHp * 1.0f / characterData.maxHp;
            screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, trs.position + new Vector3(0,0.5f,0));
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(montserRoot, screenPos, null, out Vector2 localPoint))
            {
                pairAndImage.Value.transform.parent.localPosition = localPoint;
            }
        }
    }

    public void ShowPiaozi(string message)
    {
        txtBG.SetActive(true);
        tongyongTxt.text = message;
        TimerManager.Instance.RegisterTimer(1.5f,1,()=>{txtBG.SetActive(false);});
    }

    public void ChangeHp(int instanceId, bool open)
    {
        if(activeItem.TryGetValue(instanceId,out var image))
        {
            image.transform.parent.gameObject.SetActive(open);
        }
    }
}
