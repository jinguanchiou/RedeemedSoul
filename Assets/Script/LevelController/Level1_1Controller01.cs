using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1_1Controller01 : MonoBehaviour
{
    public ConversationFunction Level1_1_01;
    public GameObject DialogBox;
    public Text DialogBoxText;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;

    private GameObject Image_Log;
    public Animator Anim;
    private PlayerController playerController;
    private bool StartConversation = false;
    private int currentTextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Script();
        if (!Level1_1_01.LevelAlreadyTold && StartConversation)
        {
            if (currentTextIndex == 0)
            {
                playerController.IsConversation();
                DialogBoxText.text = Level1_1_01.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level1_1_01.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = Level1_1_01.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= Level1_1_01.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                Level1_1_01.LevelAlreadyTold = true;
            }
        }
        if (Level1_1_01.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
        }
    }
    void GenerateImage()
    {
        if (Level1_1_01.avatar[currentTextIndex] != null)
        {
            if (Level1_1_01.avatar[currentTextIndex].name == "PlayerAvatar")
            { Image_Log = Instantiate(Level1_1_01.avatar[currentTextIndex], PlayerAvatar); }
            else
            { Image_Log = Instantiate(Level1_1_01.avatar[currentTextIndex], OtherAvatar); }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartConversation = true;
        }
    }
    void Script()
    {
        if (DialogBoxText.text == "�@�r~���L�ӤF�C")
        {
            Anim.SetBool("StandUp", true);
        }
        if (DialogBoxText.text == "���K���ӡA���٦��ƥ����F�C")
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (DialogBoxText.text == "�B�K�~�M�����ǯ��C")
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
