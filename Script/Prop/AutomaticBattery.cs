using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Weapon
{
    public class AutomaticBattery : Firearms
    {
        private Transform enemy;   //敌人位置

        private Animator batteryAnimator;  //动画控制器
        private float lastActionTime;  //炮台转向间隔

        private Quaternion targetRotation;  //目标朝向
        private float distanceToEnemy;  //与敌人距离

        public float batteryDamage;
        public float attackRadius = 7f;  //攻击半径
        public float turnSpeed;  //旋转速度


        private void OnTriggerStay(Collider other)
        {
            batteryAnimator = GetComponent<Animator>();
            //Debug.Log(other.gameObject.tag);

            if (other.gameObject.tag == "Enemy")
            {
                
                enemy = other.transform;
                distanceToEnemy = Vector3.Distance(enemy.position, transform.position);

                if (distanceToEnemy > attackRadius)
                {
                    if (Time.time - lastActionTime > 3)
                    {
                        targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                        //batteryAnimator.SetBool("Attack", false);
                        lastActionTime = Time.time;
                    }
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(enemy.position - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                    DoAttack();
                    //batteryAnimator.SetBool("Attack", true);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
            //enemy = GameObject.FindWithTag("Enemy").transform;
            
        }

        protected override void Shooting()
        {
            if (CurrentAmmo <= 0) return;
            if (!IsAllowShooting()) return;
            //CurrentAmmo -= 1;
            batteryAnimator.Play("Take 001 0", 0, 0);
            CreateBullet();
            LastFireTime = Time.time;
        }

        protected override void Reload()
        {

        }

        protected void CreateBullet()
        {
            MuzzlePoint = MuzzleTransform;
            GameObject tmp_Bullet = Instantiate(Big_Bullet, MuzzlePoint.position, MuzzlePoint.rotation);
            var tmp_BulletRigidbody = tmp_Bullet.AddComponent<Rigidbody>();
            // tmp_Bullet.transform.rotation = MuzzleTransform.rotation;
            tmp_BulletRigidbody.velocity = MuzzleTransform.up * 60f;
            //碰撞检测
            RaycastHit hit;
            if (Physics.Raycast(tmp_Bullet.transform.position, tmp_Bullet.transform.up, out hit, 100f))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null && target.health > 0)
                {
                    //Debug.Log(batteryDamage);
                    target.takeDamage(batteryDamage);
                }
                Destroy(tmp_Bullet, 5);
            }
        }
    }
}
