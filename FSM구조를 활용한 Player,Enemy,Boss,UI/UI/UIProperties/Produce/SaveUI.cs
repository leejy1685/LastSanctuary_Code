using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private Sprite[] saveSprites;
    [SerializeField] private Image saveImage;
    [SerializeField] private Image saveTextImage;

    [SerializeField] private float frameDelay = 0.4f;

    private Coroutine _animCoroutine;

    public void SaveAnima()
    {
        if (_animCoroutine != null)
            StopCoroutine(_animCoroutine);
        _animCoroutine = StartCoroutine(SaveAnimation_Coroutine());
    }
    public void StopSaveAnimation()
    {
        if (_animCoroutine != null)
        {
            StopCoroutine(_animCoroutine);
            _animCoroutine = null;
        }

        if (saveSprites.Length > 0)
            saveImage.sprite = saveSprites[0];
    }

    private IEnumerator SaveAnimation_Coroutine()
    {
        Color color1 = saveImage.color;
        Color color2 = saveTextImage.color;
        color1.a = 1f;
        color2.a = 2f;
        saveImage.color = color1;
        saveTextImage.color = color2;

        int repeatCount = 2;
        for (int loop = 0; loop < repeatCount; loop++)
        {
            for (int idx = 0; idx < saveSprites.Length; idx++)
            {
                saveImage.sprite = saveSprites[idx];
                yield return new WaitForSeconds(frameDelay);
            }
        }

        yield return new WaitForSeconds(0.5f);
        color1.a = 0f;
        color2.a = 0f;
        saveImage.color = color1;
        saveTextImage.color = color2;

        this.gameObject.SetActive(false);
    }
}