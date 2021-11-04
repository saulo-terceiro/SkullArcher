using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelletonAnimator : MonoBehaviour
{

    public Animator skelletonAnimator;

    public AudioClip stretchSound;
    public AudioClip lounchSound;
    private static SkelletonAnimator _instance;

    public static SkelletonAnimator Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void push()
    {        
        this.skelletonAnimator.SetBool("firstClick",true);
        this.skelletonAnimator.SetBool("pushed",false);
        this.skelletonAnimator.SetBool("release",false);
        this.skelletonAnimator.SetBool("finish",true);

    }

    public void pushed()
    {
        this.skelletonAnimator.SetBool("pushed",true);
        this.skelletonAnimator.SetBool("finish",false);



    }
    public void release()
    {
        this.skelletonAnimator.SetBool("release",true);

    }

    public void finish()
    {
        this.skelletonAnimator.SetBool("finish",true);
        this.skelletonAnimator.SetBool("firstClick",false);
    }

    public void stretch()
    {
        Controller.Instance.playSound(stretchSound);
    }

    public void releaseSound()
    {
        Controller.Instance.playSound(lounchSound);

    }
    

    
}
