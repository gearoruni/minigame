using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private SkillComponent skillCmp;
    private cfg.DialogueConfigs dialogueConfigs;
    public List<Image> maskList;
    public GameObject dialogUI;
    public Text nameTxt;
    public Text mainTxt;
    public Button next;
    private void Awake()
    {
       
        var player = EntityManager.Instance.GetEntityFromEntityId(1);
        skillCmp = (SkillComponent)player.GetComponent("SkillComponent");
        next.onClick.AddListener(NextTxt);
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
    }
    public void ShowTxt(int id)
    {
        dialogUI.SetActive(true);
        dialogueConfigs = TableDataManager.Instance.tables.MainTxt.Get(id);
        nameTxt.text = dialogueConfigs.��ɫ����;
        mainTxt.text = dialogueConfigs.�ı�����;
    }
    private void NextTxt()
    {
        if (dialogueConfigs.��һ�� == 0)
        {
            dialogUI.SetActive(false);
            return;
        }
        ShowTxt(dialogueConfigs.��һ��);
    }
}
