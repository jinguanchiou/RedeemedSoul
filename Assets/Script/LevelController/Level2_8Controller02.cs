using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2_8Controller02 : MonoBehaviour
{
    public GameObject RiruAIOR;
    public RiruAI riruAI;
    public Riru3DController riru3D;
    public Animator RiruAnim;
    public PlayerController playerController;
    public bool Lock;

    public GameObject ShadyUI;
    public Image Shady;
    public GameObject DialogBox;
    public Text DialogBoxText;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;

    public ConversationFunction Level2_8_02;
    public ConversationFunction Level2_8_01;

    public GameObject Stop;
    public GameObject Stop2;
    private GameObject Image_Log;
    private bool StartConversation = false;
    private int currentTextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(!Level2_8_02.LevelAlreadyTold)
        {
            Level2_8_01.LevelAlreadyTold = false;
        }
        if(Level2_8_02.LevelAlreadyTold)
        {
            Destroy(Stop);
            Destroy(Stop2);
            Destroy(RiruAIOR);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (riruAI.health <= 0 && !Lock)
        {
            Lock = true;
            StartCoroutine(RiruDead());
        }
        if (!Level2_8_02.LevelAlreadyTold && StartConversation)
        {
            if (currentTextIndex == 0)
            {
                playerController.IsConversation();
                DialogBoxText.text = Level2_8_02.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level2_8_02.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = Level2_8_02.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= Level2_8_02.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                Level2_8_02.LevelAlreadyTold = true;
                Destroy(Stop);
                Destroy(Stop2);
                Destroy(gameObject);
            }
        }
        void GenerateImage()
        {
            if (Level2_8_02.avatar[currentTextIndex] != null)
            {
                if (Level2_8_02.avatar[currentTextIndex].name == "PlayerAvatar")
                { Image_Log = Instantiate(Level2_8_02.avatar[currentTextIndex], PlayerAvatar); }
                else
                { Image_Log = Instantiate(Level2_8_02.avatar[currentTextIndex], OtherAvatar); }
            }
        }
        IEnumerator RiruDead()
        {
            for (int i = 0; i < 3; i++)
            {
                ShadyUI.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                ShadyUI.SetActive(false);
                yield return new WaitForSeconds(0.1f);
            }
            RiruAnim.SetTrigger("Dead");
            yield return new WaitForSeconds(1);
            Destroy(RiruAIOR);
            playerController.IsConversation();
            StartConversation = true;
        }
    }
}
