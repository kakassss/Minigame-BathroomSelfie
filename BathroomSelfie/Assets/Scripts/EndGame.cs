using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EndGame : MonoBehaviour
{
    public static EndGame current;

    [Header("Reference")]
    [SerializeField] FlashAndPhotos photos;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI levelCompletedText;

    private bool canDistribute = true;
    private IEnumerator distribute;
    private void Start()
    {
        distribute = DistributePhotos();
    }
    public void CheckIfTapGameEnded()
    {
        StartCoroutine(distribute);
    }
    private IEnumerator DistributePhotos()
    {
        if (canDistribute)
        {
            for (int i = 0; i < photos.currentPhotos.Count; i++)
            {
                photos.currentPhotos[i].transform.DOMove(new Vector3(Random.Range(-1, 2), Random.Range(9, 12), transform.position.z), 0.2f);
                yield return new WaitForSeconds(0.2f);
            }
            canDistribute = false;
        }
        
        GameEvents.current.OnEndGame -= CheckIfTapGameEnded;
        StartCoroutine(LevelComplete());
    }
    private IEnumerator LevelComplete()
    {       
        yield return new WaitForSeconds(1f);
        foreach (GameObject photo in photos.currentPhotos)
        {
            photo.gameObject.SetActive(false);
        }
        levelCompletedText.gameObject.SetActive(true);
        currentLevelText.gameObject.SetActive(false);
    }

}
