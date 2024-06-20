using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow3DObject : MonoBehaviour
{
    public Transform target; // 3D �l���� Transform
    public RectTransform uiElement; // 2D UI ������ RectTransform
    public Camera mainCamera; // �D�ṳ��

    void Update()
    {
        // �N 3D �l���󪺥@�ɧ����ഫ���̹�����
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        // �]�m 2D UI ��������m
        uiElement.position = screenPos;
    }
}
