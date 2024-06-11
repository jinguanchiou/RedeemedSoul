using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContorller : MonoBehaviour
{
    public ScenseContorller scenseContorller;
    public GameObject BackToThisLevel;
    public Transform PlayerTransform;
    public int I_log;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < scenseContorller.ScenseList.Count; i++)
        {
            if(scenseContorller.ScenseList[i])
            {
                I_log = i;
            }
        }
        if(I_log < scenseContorller.i)
        {
            PlayerTransform.position = BackToThisLevel.transform.position;
            PlayerTransform.rotation = BackToThisLevel.transform.rotation;
            scenseContorller.i = I_log;
        }
        else
            scenseContorller.i = I_log;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
