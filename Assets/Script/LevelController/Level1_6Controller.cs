using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1_6Controller : MonoBehaviour
{
    public ConversationFunction Level1_6;
    public GameObject DialogBox;
    public GameObject woltor;
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
        if (Level1_6.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
            Destroy(woltor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Level1_6.LevelAlreadyTold && StartConversation)
        {
            if (currentTextIndex == 0)
            {
                playerController.IsConversation();
                DialogBoxText.text = Level1_6.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level1_6.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = Level1_6.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= Level1_6.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                Level1_6.LevelAlreadyTold = true;
            }
        }
        if (Level1_6.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
            Destroy(woltor);
        }
    }
    void GenerateImage()
    {
        if (Level1_6.avatar[currentTextIndex] != null)
        {
            if (Level1_6.avatar[currentTextIndex].name == "PlayerAvatar")
            { Image_Log = Instantiate(Level1_6.avatar[currentTextIndex], PlayerAvatar); }
            else
            { Image_Log = Instantiate(Level1_6.avatar[currentTextIndex], OtherAvatar); }
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