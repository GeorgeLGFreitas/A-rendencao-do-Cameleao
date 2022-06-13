using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private int bS;
    public float speed;
    public int damage = 10;
    public float explosionRadius = 0f;
    public Vector2 dir;

    private void Start()
    {
     
        
    }
    public void Seek(Transform _target)
    {
        target = _target;
        dir = target.position - transform.position;
        
    }
   
    void Update()
    {
        
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        float distanceThisFrame = speed * Time.deltaTime;

       /* if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }*/
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);    
    }

    public void HitTarget()
    {
       
        Damage(target);
        Destroy(gameObject);
    }

    
    public void HitGround()
    {
        
        Destroy(this.gameObject);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !PlayerMover.invencivel && !PlayerMover.invDash)
        {
            HitTarget();
        }
        if (collision.tag == "TileMap")
        {
            
            HitGround();
        }
        
    }

    public void Damage(Transform player)
    {
        PlayerMover p = player.GetComponent<PlayerMover>();
        p.TakeDamage(damage);
    }
 
}
