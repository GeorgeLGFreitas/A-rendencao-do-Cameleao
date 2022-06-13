using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    CheckpointController loader;
   
    private static NextLevel instance2;
    public bool carrega;
    public Fadeout fade;
    float loadTime;
    public int cenaL;
    // Start is called before the first frame update
    void Start()
    {
        loader = FindObjectOfType<CheckpointController>();
        carrega = false;
        loadTime = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadTime <= 0)
        {
            nextLevel();

        }
        if (carrega)
        {
            loadTime -= Time.deltaTime;
            fade.FadeIn();
            Destroy(loader.gameObject);
        }
       
    }
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            carrega = true;
            Destroy(loader.gameObject);
        }
    }*/
    public void nextLevel()
    {
        
        SceneManager.LoadScene(cenaL, LoadSceneMode.Single);
        Destroy(this.gameObject);
    }
    
}
