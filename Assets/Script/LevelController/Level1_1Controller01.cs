using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1_1Controller01 : MonoBehaviour
{
    public ConversationFunction Level1_1_01;
    public GameObject DialogBox;
    public Text DialogBoxText;

    private PlayerController playerController;
    private bool StartConversation = false;
    private int currentTextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Level1_1_01.LevelAlreadyTold && StartConversation)
        {
            if (currentTextIndex == 0)
            {
                DialogBoxText.text = Level1_1_01.LevelTextMesh[0];
                DialogBox.SetActive(true);
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level1_1_01.LevelTextMesh.Count)
            {
                DialogBoxText.text = Level1_1_01.LevelTextMesh[currentTextIndex];
                currentTextIndex++;
            }
            if(currentTextIndex >= Level1_1_01.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                Level1_1_01.LevelAlreadyTold = true;
            }
        }
        if(Level1_1_01.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartConversation = true;
            playerController.IsConversation();
        }
    }
}
