using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TapCollider : MonoBehaviour
{
    public static TapCollider current;

    [Header("TagNames")]
    [SerializeField] string arrowUpTagName;
    [SerializeField] string arrowDownTagName;
    [SerializeField] string arrowLeftTagName;
    [SerializeField] string arrowRightTagName;

    [Header("AnimationNames")]
    [SerializeField] string standingPose1;
    [SerializeField] string standingPose2;
    [SerializeField] string standingPose3;
    [SerializeField] string standingPose4;

    [Header("Reference")]
    [SerializeField] ArrowTapMovement arrowTapMovement;
    [SerializeField] Animator animWoman;

    [Header("UI")]
    [SerializeField] Image tapBarImage;
    [SerializeField] GameObject tapBarUI;

    private Vector3 firstPosImageBar;
    private bool isTriggerFull = false;

    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        TapBarDeactive();
        firstPosImageBar = tapBarImage.transform.position; //back to where it started after shake
    }
    private void OnTriggerEnter(Collider other)
    {
        //GameEvents.current.onWrongTapAttempt += CheckWrongTapAttempt;
        isTriggerFull = true;
    }
    private void OnTriggerStay(Collider other)
    {       
        //ArrowUpChecks
        if (other.tag == arrowUpTagName
             && arrowTapMovement.difference.y <= -0.1f)
        {
            StartCoroutine(CorrectTapAnimations(other));
            StandingAnimations(standingPose1);
        }
        else if(other.tag == arrowUpTagName && arrowTapMovement.difference != Vector3.zero)
        {
            StartCoroutine(WrongTapAnimations());
        }

        //ArrowDownChecks
        if (other.tag == arrowDownTagName
            && arrowTapMovement.difference.y >= 0.1f)
        {
            StartCoroutine(CorrectTapAnimations(other));
            StandingAnimations(standingPose2);
        }
        else if (other.tag == arrowDownTagName && arrowTapMovement.difference != Vector3.zero)
        {
            StartCoroutine(WrongTapAnimations());
        }


        //ArrowRightChecks
        if (other.tag == arrowRightTagName
            && arrowTapMovement.difference.x >= 0.1f)
        {
            StartCoroutine(CorrectTapAnimations(other));
            StandingAnimations(standingPose3);
        }
        else if (other.tag == arrowRightTagName && arrowTapMovement.difference != Vector3.zero)
        {
            StartCoroutine(WrongTapAnimations());
        }

        //ArrowLeftChecks
        if (other.tag == arrowLeftTagName
            && arrowTapMovement.difference.x <= -0.1f)
        {
            StartCoroutine(CorrectTapAnimations(other));
            StandingAnimations(standingPose4);
        }
        else if (other.tag == arrowLeftTagName && arrowTapMovement.difference != Vector3.zero)
        {
            StartCoroutine(WrongTapAnimations());
        }
    }
    private void OnTriggerExit(Collider other)
    {       
        isTriggerFull = false;
    }
    //Woman standingPose1234
    public void StandingAnimations(string standingAnim)
    {
        animWoman.SetTrigger(standingAnim);
    }
    private IEnumerator CorrectTapAnimations(Collider other)
    {
        other.GetComponent<Arrow>().canMove = false;
        other.GetComponentInChildren<SpriteRenderer>().DOFade(0f, 1f);
        other.transform.DOMoveY(9f, 1f);
        tapBarImage.DOColor(Color.green, 0.1f);

        yield return new WaitForSeconds(0.1f);
        WhiteColor();
    }
    private IEnumerator WrongTapAnimations()
    {
        tapBarImage.DOColor(Color.red, 0.1f);
        Tweener shakeTweener =  tapBarImage.transform.DOShakePosition(0.1f,0.1f,fadeOut:true);
        shakeTweener.onComplete = () =>
        {
            tapBarImage.transform.DOMove(firstPosImageBar, 0.1f);
            shakeTweener.Kill(true);
        };
        yield return new WaitForSeconds(0.1f);
        WhiteColor();
    }
    public void CheckWrongTapAttempt()
    {
        if (!isTriggerFull)
        {
            if (arrowTapMovement.difference.x != 0f || arrowTapMovement.difference.y != 0f)
            {
                StartCoroutine(WrongTapAnimations());
            }
        }
    }
    private void WhiteColor()
    {
        tapBarImage.DOColor(Color.white, 1f);
    }
    private void TapBarDeactive()
    {
        tapBarUI.gameObject.SetActive(false);
        tapBarUI.transform.DOScale(0, 0.8f);
    }
    public void TapBarActive()
    {
        tapBarUI.gameObject.SetActive(true);
        tapBarUI.transform.DOScale(1, 0.2f);
    }
    
    
}
