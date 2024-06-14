using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Merchant : MonoBehaviour
{
    public GameObject Mall;
    public GameObject DialogBox;
    public Text DialogBoxText;
    private GameObject Image_Log;
    public ConversationFunction MerchantAngry;
    public Transform PlayerAvatar;
    public Transform OtherAvatar;
    private PlayerController playerController;
    private int currentTextIndex = 0;

    private int OpenQuantity;
    private Animator Anim;
    private bool IsOpenMall;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimContorller();
        if (OpenQuantity > 5)
        {
            Conversation();
        }
    }
    void AnimContorller()
    {
        if (!IsOpenMall)
        {
            Anim.SetTrigger("ReturnToIdle");
        }
    }
    public void OpenMall()
    {
        IsOpenMall = true;
        Anim.SetTrigger("OpenWindbreaker");
        if (Mall != null)
        {
            Mall.SetActive(!Mall.activeSelf);
            OpenQuantity++;
        }
    }
    public void CloseMall()
    {
        if (IsOpenMall)
        {
            Anim.SetTrigger("CloseWindbreaker");
            IsOpenMall = false;
            if (Mall != null)
            {
                Mall.SetActive(!Mall.activeSelf);
            }
        }
    }
    void Conversation()
    {
        if (!MerchantAngry.LevelAlreadyTold)
        {
            playerController.IsConversation();
            if (currentTextIndex == 0)
            {
                DialogBoxText.text = MerchantAngry.LevelTextMesh[0];
                DialogBox.SetActive(true);
                GenerateImage();
                currentTextIndex++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && currentTextIndex < MerchantAngry.LevelTextMesh.Count)
            {
                Destroy(Image_Log);
                DialogBoxText.text = MerchantAngry.LevelTextMesh[currentTextIndex];
                GenerateImage();
                currentTextIndex++;
            }
            if (currentTextIndex >= MerchantAngry.LevelTextMesh.Count)
            {
                DialogBox.SetActive(false);
                OpenQuantity = 0;
                currentTextIndex = 0;
                playerController.OnlockPlayer = false;
            }
        }
    }
    void GenerateImage()
    {
        if (MerchantAngry.avatar[currentTextIndex] != null)
        {
            if (MerchantAngry.avatar[currentTextIndex].name == "PlayerAvatar")
            { Image_Log = Instantiate(MerchantAngry.avatar[currentTextIndex], PlayerAvatar); }
            else
            { Image_Log = Instantiate(MerchantAngry.avatar[currentTextIndex], OtherAvatar); }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("In");
            other.GetComponent<PlayerController>().canOpenMall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().canOpenMall = false;
            CloseMall();
            OpenQuantity = 0;
        }
    }
}
