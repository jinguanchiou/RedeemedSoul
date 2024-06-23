using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2_2Controller : MonoBehaviour
{
    public ConversationFunction Level2_2;
    public GameObject DialogBox;
    public Text DialogBoxText;
    public Transform PlayerTreanform;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;

    private GameObject Image_Log;
    private PlayerController playerController;
    private bool StartConversation = false;
    private int currentTextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (Level2_2.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Level2_2.LevelAlreadyTold && StartConversation)
        {
            if (currentTextIndex == 0)
            {
                playerController.IsConversation();
                DialogBoxText.text = Level2_2.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level2_2.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = Level2_2.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= Level2_2.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                Level2_2.LevelAlreadyTold = true;
            }
        }
        if (Level2_2.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
        }
    }
    void GenerateImage()
    {
        if (Level2_2.avatar[currentTextIndex] != null)
        {
            if (Level2_2.avatar[currentTextIndex].name == "PlayerAvatar")
            { Image_Log = Instantiate(Level2_2.avatar[currentTextIndex], PlayerAvatar); }
            else
            { Image_Log = Instantiate(Level2_2.avatar[currentTextIndex], OtherAvatar); }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartConversation = true;
        }
    }
}
