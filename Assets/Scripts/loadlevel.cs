using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadlevel: MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     
    }

    // Update is called once per frame
    void Update()
    {
        Start();
    }
}
