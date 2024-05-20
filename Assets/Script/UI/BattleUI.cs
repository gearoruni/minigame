using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private SkillComponent skillCmp;
    public List<Image> maskList;
    private void Awake()
    {
        var player = EntityManager.Instance.GetEntityFromEntityId(1);
        skillCmp = (SkillComponent)player.GetComponent("SkillComponent");
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
}
