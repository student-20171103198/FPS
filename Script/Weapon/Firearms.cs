using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Weapon
{
    public abstract class Firearms : MonoBehaviour, IWeapon
    {

        public Transform MuzzlePoint;
        public Transform CasingPoint;
        public Transform MuzzleTransform;
        //枪口粒子
        public ParticleSystem MuzzleParticle;
        public ParticleSystem CasingParticle;

        public GameObject Big_Bullet;
        
        public int AmmoInMag;
        public int MaxAmmoCarried;
        public float FireRate;

        protected float LastFireTime;

        //弹药
        protected int CurrentAmmo;
        protected int CurrentMaxAmmoCarried;

        protected Animator GunAnimator;

        protected virtual void Start()
        {
            CurrentAmmo = AmmoInMag;
            CurrentMaxAmmoCarried = MaxAmmoCarried;
            GunAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }

        public void DoAttack()
        {
            Shooting();
            
        }

        protected abstract void Shooting();
        protected abstract void Reload();

        protected bool IsAllowShooting()
        {
            return Time.time - LastFireTime > 1 / FireRate;

        }

    }
}
