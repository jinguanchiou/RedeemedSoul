using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1_1Controller02 : MonoBehaviour
{
    public float FadeInTime;
    private float FadeInTime_Log;
    public float DisplayTime;
    public float FadeoutTime;
    private float FadeoutTime_Log;
    public GameObject DialogBox;
    public Text DialogBoxText;
    public GameObject ShadyUI;
    public Image Shady;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;
    public Transform PlayerTransform;
    public Transform TarGet;
    public Rigidbody2D Rb;
    public Rigidbody2D PlayerRb;
    public ConversationFunction Level1_1_01;
    public ConversationFunction Level1_1_02;
    public GameObject Stop;
    public GameObject Pig;
    public GameObject Corpse;
    public SpriteRenderer CorpseSR;
    public PlayerController playerController;
    private SpriteRenderer SR;
    private bool StartConversation;
    private bool SetTheScene;
    private GameObject Image_Log;
    private bool Lock;
    public Animator Anim;
    private int currentTextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        FadeoutTime_Log = FadeoutTime;
        FadeInTime_Log = FadeInTime;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Rb = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        if (!Level1_1_01.LevelAlreadyTold)
        {
            StartConversation = false;
        }
        if (Level1_1_02.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(Corpse);
            Destroy(Stop);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Pig == null)
        {
            ShadyUI.SetActive(true);
            StartCoroutine(SetScene());
        }
        if (SetTheScene)
        {
            if (FadeInTime > 0)
            {
                FadeIn();
            }
            if (FadeInTime <= 0)
            {
                DisplayTime -= Time.deltaTime;
            }
            if (DisplayTime <= 0)
            {
                FadeOut();
            }
            if(FadeoutTime <= 0)
            {
                ShadyUI.SetActive(false);
            }
        }
        Script();
        if (!Level1_1_02.LevelAlreadyTold && StartConversation)
        {
            
            if (currentTextIndex == 0)
            {
                playerController.IsConversation();
                DialogBoxText.text = Level1_1_02.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level1_1_02.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = Level1_1_02.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= Level1_1_02.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                Debug.Log(currentTextIndex);
                Debug.Log(Level1_1_02.LevelTextMesh.Count);
                Level1_1_02.LevelAlreadyTold = true;
            }
        }
        if (Level1_1_02.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(Corpse);
            Destroy(Stop);
            Destroy(gameObject);
        }
        void GenerateImage()
        {
            if (Level1_1_02.avatar[currentTextIndex] != null)
            {
                if (Level1_1_02.avatar[currentTextIndex].name == "PlayerAvatar")
                { Image_Log = Instantiate(Level1_1_02.avatar[currentTextIndex], PlayerAvatar); }
                else
                { Image_Log = Instantiate(Level1_1_02.avatar[currentTextIndex], OtherAvatar); }
            }
        }
        void Script()
        {
            if (DialogBoxText.text == "呀…我就知道你行的。")
            {
                playerController.IsConversation();
                StartCoroutine(JUMP());
            }
            if (DialogBoxText.text == "怎麼這麼說，我還是有良心的，最起碼我還是會幫你立個石碑的。")
            {
                Lock = false;
            }
            if(DialogBoxText.text == "你根本不是人!!")
            {
                StartCoroutine(Transitions());
            }
        }
        IEnumerator JUMP()
        {
            if (!Lock)
            {
                Lock = true;
                StartConversation = false;
                yield return new WaitForSeconds(0.3f);
                Vector2 jumpDirection = transform.right;
                Anim.SetBool("Jump", true);
                Rb.AddForce(jumpDirection * 8, ForceMode2D.Impulse);
                yield return new WaitForSeconds(3);
                StartConversation = true;
            }
        }
        IEnumerator Transitions()
        {
            if (!Lock)
            {
                Lock = true;
                StartConversation = false;
                PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, 6);
                yield return new WaitForSeconds(2);
                Rb.velocity = new Vector2(0f, 0f);
                StartConversation = true;
            }
        }
        IEnumerator SetScene()
        {
            if (!SetTheScene)
            {
                SetTheScene = true;
                playerController.IsConversation();
                Color color_1 = CorpseSR.color;
                color_1.a = 1;
                CorpseSR.color = color_1;
                Color color_2 = SR.color;
                color_2.a = 1;
                SR.color = color_2;
                PlayerTransform.position = TarGet.position;
                PlayerTransform.rotation = TarGet.rotation;
                yield return new WaitForSeconds(FadeInTime+ DisplayTime+ FadeoutTime);
                StartConversation = true;
            }
        }
        void FadeIn()
        {
            FadeInTime -= Time.deltaTime;
            Color color_1 = Shady.color;
            color_1.a = 1;
            Shady.color = color_1;
        }
        void FadeOut()
        {
            FadeoutTime -= Time.deltaTime;
            Color color_1 = Shady.color;
            color_1.a = FadeoutTime / FadeoutTime_Log;
            Shady.color = color_1;
        }
    }
}
