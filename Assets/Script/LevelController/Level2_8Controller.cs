using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2_8Controller : MonoBehaviour
{
    public GameObject RiruAIOR;
    public RiruAI riruAI;
    public Riru3DController riru3D;
    public Animator RiruAnim;
    public PlayerController playerController;
    public CastSpell cast;
    public Rigidbody2D riruRb;

    public ConversationFunction Level2_8;
    public ConversationFunction Level2_8_2;
    public GameObject DialogBox;
    public Text DialogBoxText;
    public Transform PlayerTreanform;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;
    public GameObject Stop;

    private GameObject Image_Log;
    private bool StartConversation = false;
    private int currentTextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Stop.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        riruRb.bodyType = RigidbodyType2D.Static;
        riruAI.OnLock = true;
        if (Level2_8.LevelAlreadyTold && Level2_8_2.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Stop.SetActive(false);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Level2_8.LevelAlreadyTold && StartConversation)
        {
            if (currentTextIndex == 0)
            {
                playerController.IsConversation();
                DialogBoxText.text = Level2_8.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level2_8.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = Level2_8.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= Level2_8.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                cast.OnLock = false;
                Level2_8.LevelAlreadyTold = true;
            }
        }
        if (Level2_8.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            riruAI.OnLock = false;
            Stop.SetActive(true);
            Destroy(gameObject);
        }
    }
    void GenerateImage()
    {
        if (Level2_8.avatar[currentTextIndex] != null)
        {
            if (Level2_8.avatar[currentTextIndex].name == "PlayerAvatar")
            { Image_Log = Instantiate(Level2_8.avatar[currentTextIndex], PlayerAvatar); }
            else
            { Image_Log = Instantiate(Level2_8.avatar[currentTextIndex], OtherAvatar); }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Stop.SetActive(true);
            riruRb.bodyType = RigidbodyType2D.Dynamic;
            RiruAnim.SetTrigger("Start");
            playerController.IsConversation();
            cast.OnLock = true;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        StartConversation = true;
    }
}
