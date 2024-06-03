using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private SkillComponent skillCmp;
    private CharacterDataComponent characterData;
    private cfg.DialogueConfigs dialogueConfigs;
    public List<Image> maskList;
    public List<Image> lockList;
    public GameObject dialogUI;
    public Text nameTxt;
    public Text mainTxt;
    public Button next;
    public Image leftperson;
    public Image rightperson;
    public Image health;

    public RectTransform montserRoot;
    public GameObject MonsterHpTemplate;

    public static BattleUI Instance;
    private Action callBack = null;
    private void Awake()
    {
        Instance = this;
        var player = EntityManager.Instance.GetEntityFromEntityId(1)[0];
        characterData = (CharacterDataComponent)player.GetComponent("CharacterDataComponent");
        skillCmp = (SkillComponent)player.GetComponent("SkillComponent");
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
        foreach(var skill in skillCmp.data.Values)
        {
            Image img = maskList[(int)skill.Type];
            lockList[(int)skill.Type].gameObject.SetActive(skill.isLock);
            if (img == null) continue;
            float cd = 1 - skillCmp.nowCdtime[skill.idx] / skill.cd;
            img.fillAmount = cd;
        }

        health.fillAmount = characterData.nowHp * 1.0f / characterData.maxHp;
        UpdateMonsterHp();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void ShowTxt(int id, Action callback = null)
    {
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
            trs = entity.go.transform;

            pairAndImage.Value.fillAmount = characterData.nowHp * 1.0f / characterData.maxHp;
            screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, trs.position);
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(montserRoot, screenPos, null, out Vector2 localPoint))
            {
                pairAndImage.Value.transform.parent.localPosition = localPoint;
            }
        }
    }
}
