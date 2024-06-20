using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow3DObject : MonoBehaviour
{
    public Transform target; // 3D 子物件的 Transform
    public RectTransform uiElement; // 2D UI 元素的 RectTransform
    public Camera mainCamera; // 主攝像機

    void Update()
    {
        // 將 3D 子物件的世界坐標轉換為屏幕坐標
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        // 設置 2D UI 元素的位置
        uiElement.position = screenPos;
    }
}
