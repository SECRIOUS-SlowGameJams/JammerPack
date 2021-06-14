using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController2D : MonoBehaviour, IMovementListener
{
    

    public AnimationClip[] animationClips;

    public AnimatorClipInfo[] animInfo;
    
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void MoveLeft()
    {
        animator.SetBool("isMoving", true);
    }

    public void MoveRight()
    {
        animator.SetBool("isMoving", true);
        animInfo = animator.GetCurrentAnimatorClipInfo(0);
        //for (int i = 0; i < animInfo.Length; i++)
        //{
        //    animationClips[i] = animInfo[i].clip;
        //}
    }

    public void StoppedMoving()
    {
        animator.SetBool("isMoving", false);
    }
}
