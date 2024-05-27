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
    public GameObject dialogUI;
    public Text nameTxt;
    public Text mainTxt;
    public Button next;
    public Image leftperson;
    public Image rightperson;
    public Image health;
    private void Awake()
    {
       
        var player = EntityManager.Instance.GetEntityFromEntityId(1);
        characterData = (CharacterDataComponent)player.GetComponent("CharacterDataComponent");
        skillCmp = (SkillComponent)player.GetComponent("SkillComponent");
        next.onClick.AddListener(NextTxt);
        SoundManager.Instance.PlayBGM("����1-3 �ݵ�����");
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
            if (img == null) continue;
            float cd = 1 - skillCmp.nowCdtime[skill.idx] / skill.cd;
            img.fillAmount = cd;
        }

        health.fillAmount = characterData.nowHp * 1.0f / characterData.maxHp;
    }
    public void ShowTxt(int id)
    {
        dialogUI.SetActive(true);
        dialogueConfigs = TableDataManager.Instance.tables.MainTxt.Get(id);
        nameTxt.text = dialogueConfigs.��ɫ����;
        mainTxt.text = dialogueConfigs.�ı�����;
        //������
        if(dialogueConfigs.����id == 8001)
        {
            leftperson.color = Color.white;
            rightperson.color = new Color(125, 125, 125);
        }
        else
        {
            if(rightperson.sprite == null)
            {
                rightperson.sprite = Resources.Load<Sprite>($"{dialogueConfigs.����id}");
                rightperson.gameObject.SetActive(true);
            }
            rightperson.color = Color.white;
            leftperson.color = new Color(125, 125, 125);
        }
        EntityManager.Instance.SetEntityController(false);
    }
    private void NextTxt()
    {
        if (dialogueConfigs.��һ�� == 0)
        {
            dialogUI.SetActive(false);
            EntityManager.Instance.SetEntityController(true);
            Resources.UnloadAsset(rightperson.sprite);
            rightperson.gameObject.SetActive(false);
            return;
        }
        ShowTxt(dialogueConfigs.��һ��);
    }
}
