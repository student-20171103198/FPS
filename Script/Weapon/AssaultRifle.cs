using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Weapon
{
    public class AssaultRifle : Firearms
    {
        private Text currentAmmo;
        private PlayerKilledNum killedNum;
        //玩家伤害
        public  float playerDamage = 10f;
        //玩家背包
        private PlayerInventory playerInventory;
        protected override void Reload()
        {
            CurrentAmmo = AmmoInMag;
            CurrentMaxAmmoCarried -= AmmoInMag;
        }

        protected override void Shooting()
        {
            if (CurrentAmmo <= 0) return;
            if (!IsAllowShooting()) return;
            CurrentAmmo -= 1;
            GunAnimator.Play("Shoot_Autoshot_AR", 0, 0);
            CreateBullet();
            LastFireTime = Time.time;
        }

        private void Update()
        {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
            currentAmmo = GameObject.Find("CurrentAmmo").GetComponent<Text>();
            killedNum = GetComponent<PlayerKilledNum>();
            playerDamage = playerInventory.getDamage();
            currentAmmo.text = CurrentAmmo + "/" + CurrentMaxAmmoCarried;
            if (Input.GetMouseButton(0))
            {
                DoAttack();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                GunAnimator.Play("Reload", 0, 0);
                Reload();
            }
        }

        protected void CreateBullet()
        {
            MuzzlePoint = MuzzleTransform;
            GameObject tmp_Bullet = Instantiate(Big_Bullet, MuzzlePoint.position, MuzzlePoint.rotation);
            var tmp_BulletRigidbody = tmp_Bullet.AddComponent<Rigidbody>(); 
            // tmp_Bullet.transform.rotation = MuzzleTransform.rotation;
            tmp_BulletRigidbody.velocity = MuzzleTransform.up * 60f;
            //射线检测
            RaycastHit hit;
            if (Physics.Raycast(tmp_Bullet.transform.position,tmp_Bullet.transform.up,out hit,100f))
            {
                Target target = hit.transform.GetComponent<Target>();
                if(target != null && target.health > 0)
                {
                    //Debug.Log(playerDamage);
                    target.takeDamage(playerDamage);
                }
                Destroy(tmp_Bullet, 5);
            }
        }
    }
}
