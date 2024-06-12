using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kareFadeEfekt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoFadeRoutine());
    }


    IEnumerator DoFadeRoutine()
    {
        while (gameObject.GetComponent<CanvasGroup>().alpha!=0)
        {
            gameObject.GetComponent<CanvasGroup>().alpha -= 0.4f;
        }
        yield return new WaitForSeconds(0.07f);

    }
}
