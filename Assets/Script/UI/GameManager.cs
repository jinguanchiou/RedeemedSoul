using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager: MonoBehaviour
{
    static public GameManager instance;
    public GameObject audioManagerPrefab;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void ChangeScene(string sceneName, int positionIndex)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
