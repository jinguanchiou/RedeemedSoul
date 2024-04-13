using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUpdateToPixelCamera : MonoBehaviour
{
    public RenderTexture PixelRenderTexture; // 將 Render Texture 拖曳到這個變數中
    public SpriteRenderer spriteRenderer; // 將 SpriteRenderer 拖曳到這個變數中
    public Transform RiruTransform;
    public Camera PixelCamera;
    public bool RiruIsMove;
    private Quaternion RiruRotation;
    private Quaternion RiruLastRotation;

    void Start()
    {
        UpdateToSprite();
        RiruRotation = RiruTransform.rotation;
        RiruLastRotation = RiruRotation;
    }
    void Update()
    {
        RiruRotation = RiruTransform.rotation;
        if (RiruRotation != RiruLastRotation)
        {
            RiruIsMove = true;
            Debug.Log("RiruIsMove");
        }
        else
        {
            RiruIsMove = false;
        }
        if (RiruIsMove)
        {
            UpdateRenderTexture();
            RiruLastRotation = RiruRotation;
        }
        UpdateToSprite();
    }
    void UpdateRenderTexture()
    {
        PixelRenderTexture = new RenderTexture(PixelRenderTexture.width, PixelRenderTexture.height, 0, RenderTextureFormat.ARGB32);
        PixelRenderTexture.filterMode = FilterMode.Point;
    }
    void UpdateToSprite()
    {
        Texture2D texture = new Texture2D(PixelRenderTexture.width, PixelRenderTexture.height, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;

        RenderTexture.active = PixelRenderTexture;
        texture.ReadPixels(new Rect(0, 0, PixelRenderTexture.width, PixelRenderTexture.height), 0, 0);
        texture.Apply();

        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        spriteRenderer.sprite = newSprite;
    }
}
