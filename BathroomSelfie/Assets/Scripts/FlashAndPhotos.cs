using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FlashAndPhotos : MonoBehaviour
{
    [HideInInspector] public List<GameObject> currentPhotos;

    [Header("Photos")]
    [SerializeField] List<GameObject> photoPrefabs;
    [SerializeField] Transform photoSpawn;

    private int sortingCounter = 4;

    [SerializeField] Image flashImage;

    // Start is called before the first frame update
    void Start()
    {
        flashImage.gameObject.SetActive(false);
    }
    //Used with AnimationEvent--StandingPose1234
    public void FlashStart()
    {
        flashImage.gameObject.SetActive(true);
        flashImage.DOFade(0.45f, 0.1f);

    }
    //Used with AnimationEvent--StandingPose1234
    public void FlashEnd()
    {
        flashImage.DOFade(0f, 0.1f);
        flashImage.gameObject.SetActive(false);
    }

    //Used with AnimationEvent--StandingPose1234
    public void CreatePhotos(int photoNumber)
    {
        GameObject newPhoto = Instantiate(photoPrefabs[photoNumber], photoSpawn.position +
            new Vector3(Random.Range(0.5f, 1), Random.Range(0.1f, 0.4f), 0),
                Quaternion.Euler(transform.rotation.x,
                transform.rotation.y, transform.rotation.z + Random.Range(-15, 15)),
                transform);
        newPhoto.GetComponentInChildren<SpriteRenderer>().sortingOrder = sortingCounter;

        sortingCounter++;
        currentPhotos.Add(newPhoto);
        GetActivePhoto(newPhoto);
    }
    private void GetActivePhoto(GameObject photo)
    {
        photo.gameObject.SetActive(true);
        photo.transform.DOScale(1, 0.2f);

    }
    
}
