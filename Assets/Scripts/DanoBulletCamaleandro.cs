using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoBulletCamaleandro : MonoBehaviour
{
    AudioSource gridAudio;
    public AudioClip[] bClips;
    private int bS;
    // Start is called before the first frame update
    void Start()
    {
        gridAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bS = Random.Range(0, bClips.Length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            gridAudio.PlayOneShot(bClips[bS]);
            Destroy(collision.gameObject);
        }
    }
}
