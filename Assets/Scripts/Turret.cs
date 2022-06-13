using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float range = 15f;

    public string playerTag = "Player";
    public string playerAfterDamage = "PlayerAfterDamage";
  

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    AudioSource t_Audio;
    public AudioClip tiro;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        t_Audio = GetComponent<AudioSource>();
        
    }
    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
   
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;
        foreach(GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if(distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }
        if(nearestPlayer != null && shortestDistance <= range)
        {
            target = nearestPlayer.transform;
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {
        if(target == null)
        {
            return;
        }

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
           
        }
        
        fireCountdown -= Time.deltaTime;
        
    }

    private void LateUpdate()
    {
        if (fireCountdown > 0f)
        {
            anim.SetBool("Disparou", false);
        }
    }

    void Shoot()
    {
        anim.SetBool("Disparou", true);
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        t_Audio.PlayOneShot(tiro, 0.4f);

        if(bullet != null)
        {
            bullet.Seek(target);
        }

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
