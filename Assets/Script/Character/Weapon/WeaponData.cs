using cfg;

using cfg.Character;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class WeaponData : MonoBehaviour
//{
//    public int id;
//    public string prefabName;
//    public int bulletsInClip;
//    public float reloadTime;

//    public float fireRate;

//    [ShowInInspector]
//    public List<FireDefine> fireDefines = new List<FireDefine>();

//    public void Init(int weaponId)
//    {
//        WeaponDefine weaponDefine = TableDataManager.Instance.tables.WeaponDefine;
//        WeaponConfigs configs = weaponDefine.DataMap[weaponId];

//        id = configs.Id;
//        prefabName = configs.Name;
//        bulletsInClip = configs.BulletsInClip;
//        reloadTime = configs.ReloadTime;
//        fireRate = configs.FireRate;

//        for (int i = 0;i<configs.BulletId.Count;i++)
//        {
//            FireDefine fireDefine = new FireDefine( configs.BulletId[i], 
//                                                    configs.UpLimit[i], 
//                                                    configs.DownLimit[i], 
//                                                    configs.VolleyCount[i],
//                                                    configs.BulletsPerVolley[i],
//                                                    configs.TimeBetweenBullets[i]);
//            fireDefines.Add(fireDefine);
//        }
//    }
//}
