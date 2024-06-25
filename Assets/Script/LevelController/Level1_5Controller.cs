using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1_5Controller : MonoBehaviour
{
    public float FadeInTime;
    private float FadeInTime_Log;
    public float DisplayTime;
    public float FadeoutTime;
    private float FadeoutTime_Log;
    public CameraFollow FollowTarget;
    public GameObject DialogBox;
    public GameObject SwordEnergy;
    public GameObject SwordStartTarget;
    public Text DialogBoxText;
    public GameObject ShadyUI;
    public Image Shady;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;
    public Transform PlayerTransform;
    public Transform TarGet;
    public ConversationFunction Level1_5;
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
        SR = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        if (Level1_5.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartConversation = true;
            playerController.IsConversation();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (StartConversation)
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
            if (FadeoutTime <= 0)
            {
                ShadyUI.SetActive(false);
            }
        }
        Script();
        if (!Level1_5.LevelAlreadyTold && StartConversation)
        {
            if (currentTextIndex == 0)
            {
                playerController.IsConversation();
                DialogBoxText.text = Level1_5.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < Level1_5.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = Level1_5.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= Level1_5.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                Debug.Log(currentTextIndex);
                Debug.Log(Level1_5.LevelTextMesh.Count);
                Level1_5.LevelAlreadyTold = true;
            }
        }
        if (Level1_5.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            Destroy(gameObject);
        }
        void GenerateImage()
        {
            if (Level1_5.avatar[currentTextIndex] != null)
            {
                if (Level1_5.avatar[currentTextIndex].name == "PlayerAvatar")
                { Image_Log = Instantiate(Level1_5.avatar[currentTextIndex], PlayerAvatar); }
                else
                { Image_Log = Instantiate(Level1_5.avatar[currentTextIndex], OtherAvatar); }
            }
        }
        void Script()
        {
            if (DialogBoxText.text == "沒事，這裡交給我。")
            {
                playerController.IsConversation();
                StartConversation = false;
                StartCoroutine(Energy());
            }
        }
        IEnumerator Energy()
        {
            if (!Lock)
            {
                Lock = true;
                StartConversation = false;
                yield return new WaitForSeconds(2);
                DialogBox.SetActive(false);
                GameManager.instance.audioManager.Play(2, "DrawKnife", false);
                Anim.SetTrigger("Raise");
                yield return new WaitForSeconds(3);
                GameManager.instance.audioManager.Play(2, "SwingKnife", false);
                Anim.SetTrigger("Swing");
                yield return new WaitForSeconds(1);
                GameObject swordEnergy = Instantiate(SwordEnergy, SwordStartTarget.transform.position, SwordStartTarget.transform.rotation);
                FollowTarget.smoothing = 2;
                FollowTarget.target = swordEnergy.transform;
                yield return new WaitForSeconds(5);
                FollowTarget.smoothing = 0.1f;
                FollowTarget.target = PlayerTransform;
                DialogBoxText.text = Level1_5.LevelTextMesh[currentTextIndex];
                DialogBox.SetActive(true);
                StartConversation = true;
            }
        }
        IEnumerator SetScene()
        {
            if (!SetTheScene)
            {
                SetTheScene = true;
                playerController.IsConversation();
                Color color_2 = SR.color;
                color_2.a = 1;
                SR.color = color_2;
                yield return new WaitForSeconds(FadeInTime + DisplayTime + FadeoutTime);
                FollowTarget.smoothing = 5;
                FollowTarget.target = TarGet.transform;
                yield return new WaitForSeconds(2);
                FollowTarget.target = PlayerTransform;
                FollowTarget.smoothing = 0.1f;
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
