using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndLevel : MonoBehaviour
{
    CheckpointController loader;
    private static NextLevel instance2;
    bool deleta;
    float loadTime;
    // Start is called before the first frame update
    void Start()
    {
        loader = FindObjectOfType<CheckpointController>();
        loadTime = 2;
        deleta = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (deleta == true)
        {
            Destroy(loader);
            loadTime -= Time.deltaTime;
        }
        if (loadTime <= 0)
        {
            nextLevel();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            deleta = true;
        }
    }
    public void nextLevel()
    {

        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Destroy(this.gameObject);
    }

}
