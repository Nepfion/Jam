﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public PlayerDrowse PlayerDrowse;
    public GameObject PlayerAmmo;
    public int damagePerShot = 1;
    public float gunShootSoundRadius = 15;

    public int bulletCount = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    float timer;


    Animator anim;

    public RectTransform ammo;
    Ray shootRay;
    RaycastHit shootHit;                            
    int shootableMask;
    //ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    //Light gunLight;
    float effectsDisplayTime = 0.2f;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        anim = PlayerDrowse.gameObject.GetComponentInChildren<Animator>();
        currentAmmoIndex = 6;
        /*
        gunParticles = GetComponent<ParticleSystem>();
        */
        gunLine = GetComponent<LineRenderer>();
        
        gunAudio = GetComponent<AudioSource>();
        //gunLight = GetComponent<Light>();
        
    }

    private void Update()
    {

        if (PlayerDrowse.Sleeping) return;

        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        //gunLight.enabled = false;
    }

    public Image[] bullets;
    private int currentAmmoIndex;
    public void updateBulletCount(int change)
    {
        bulletCount = Mathf.Clamp(bulletCount + change, 0, 7);
        if (currentAmmoIndex == 7)
            return;
            

        if (change > 0)
        {
            bullets[currentAmmoIndex++ + 1].enabled = true;
            Debug.Log("Incr " + bulletCount + " || " + currentAmmoIndex);
        }
        else
        {
            bullets[currentAmmoIndex--].enabled = false;
            Debug.Log("Decr " + bulletCount + " || " + currentAmmoIndex);

        }

    }
    void Shoot()
    {
        timer = 0f;

        if (bulletCount <= 0)
            return;
        
        gunAudio.Play();
        /*
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        */
        //gunLine.enabled = true;
        //gunLine.SetPosition(0, transform.position);

        PlayerDrowse.CurrentDrowse += 25f;

        anim.SetTrigger("Shooting");
        

        GameObject bullet = Instantiate(PlayerAmmo, transform.position, Quaternion.LookRotation(transform.forward));
        //GetComponentInParent<Rigidbody>().AddForceAtPosition(-100 * transform.forward, transform.position, ForceMode.Impulse);


        LayerMask mask = LayerMask.GetMask("Shootable");
        Collider[] colliders = Physics.OverlapSphere(transform.position, gunShootSoundRadius);

        foreach (var collider in colliders)
        {
            EnemyMovement enemyMovement = collider.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.Wakeup();
            }
        }

        updateBulletCount(-1);
        
        //shootRay.origin = transform.position;
        //shootRay.direction = transform.forward;

        //if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        //{
        //    EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

        //    if (enemyHealth != null)
        //    {
        //        enemyHealth.TakeDamage(damagePerShot, shootHit.point);
        //    }

        //    gunLine.SetPosition(1, shootHit.point);
        //} else
        //{
        //    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        //}

    }


}
