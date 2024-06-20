using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureToSprite : MonoBehaviour
{
    public RenderTexture renderTexture; // 將 Render Texture 拖曳到這個變數中
    public SpriteRenderer spriteRenderer; // 將 SpriteRenderer 拖曳到這個變數中
    public Transform RiruTransform;
    public Camera PixelCamera;

    public bool RiruIsMove;

    private Quaternion RiruRotation;
    private Quaternion RiruLastRotation;

    void Start()
    {
        PixelCamera.clearFlags = CameraClearFlags.SolidColor;
        UpdateTexture();
        RiruRotation = RiruTransform.rotation;
        RiruLastRotation = RiruRotation;
    }
    void Update()
    {
        RiruRotation = RiruTransform.rotation;
        if (RiruRotation != RiruLastRotation)
        {
            RiruIsMove = true;
        }
        else
        {
            RiruIsMove = false;
        }
        if (RiruIsMove)
        {

            RiruLastRotation = RiruRotation;
        }
        UpdateTexture();
    }
    void UpdateTexture()
    {
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        spriteRenderer.sprite = newSprite;
    }
}
