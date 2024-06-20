using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1_1Controller01 : MonoBehaviour
{
    public ConversationFunction Level1_1_01;
    public GameObject DialogBox;
    public GameObject Lily;
    public Text DialogBoxText;
    public Transform LilyTreansform;
    public Transform PlayerTreanform;
    public Transform TarGet;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;
    public Rigidbody2D EnemyMontherRb;
    public Rigidbody2D Rb;
    public MonsterPig1_1 monsterPig;

    private GameObject Image_Log;
    private bool Lock;
    public Animator Anim;
    private PlayerController playerController;
    private bool StartConversation = false;
    private int currentTextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        EnemyMontherRb.bodyType = RigidbodyType2D.Static;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (Level1_1_01.LevelAlreadyTold)
        {
            playerController.OnlockPlayer = false;
            monsterPig.OnLock = false;
            Destroy(gameObject);
        }
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
            monsterPig.OnLock = false;
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
        if (DialogBoxText.text == "哦呀~醒過來了。")
        {
            playerController.IsConversation();
            Anim.SetBool("StandUp", true);
        }
        if (DialogBoxText.text == "阿…那個，我還有事先走了。")
        {
            LilyTreansform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (DialogBoxText.text == "咕…居然情緒勒索。")
        {
            LilyTreansform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (DialogBoxText.text == "還能是什麼研究… ")
        {
            EnemyMontherRb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (DialogBoxText.text == "啊!!")
        {
            LilyTreansform.rotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(Transitions01());
        }
        if (DialogBoxText.text == "嗚哇哇!! ")
        {
            if (Lock)
            {
                LilyTreansform.rotation = Quaternion.Euler(0, 180, 0);
                Anim.SetBool("StandUp", false);
                Anim.SetBool("Run", true);
            }
            if ((LilyTreansform.position - TarGet.position).sqrMagnitude > 0.1f)
            {
                StartConversation = false;
                LilyTreansform.position = Vector2.MoveTowards(LilyTreansform.position, TarGet.position, 6 * Time.deltaTime);
            }
            if ((LilyTreansform.position - TarGet.position).sqrMagnitude <= 0.1f)
            {
                Anim.SetBool("Run", false);
                Anim.SetBool("StandUp", true);
                LilyTreansform.rotation = Quaternion.Euler(0, 0, 0);
                StartConversation = true;
            }
        }
        if (DialogBoxText.text == "蛤?")
        {
            PlayerTreanform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    IEnumerator Transitions01()
    {
        if (!Lock)
        {
            Lock = true;
            StartConversation = false;
            Rb.velocity = new Vector2(Rb.velocity.x, 6);
            yield return new WaitForSeconds(3);
            Rb.velocity = new Vector2(0f, 0f);
            StartConversation = true;
        }
    }
}
