using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    CameraFollow cameraFollow;
    [SerializeField]
    PlayerController playerController;
    float animdur = 48f;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(!PlayerPrefs.HasKey("FirstRun"))
        {
            Debug.Log("Im here! means no FirstRun Key");
            anim.enabled = false;
            cameraFollow.enabled = true;
            playerController.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.normalizedTime >= 1)
        {
            anim.enabled=false;
        }
    }
}
