using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{

    public Image LoadingImg;
    bool Once;
    // Start is called before the first frame update
    void Start()
    {
        LoadingImg.fillAmount = 0;
        Once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Once)
        {
            if (LoadingImg.fillAmount < 1)
            {
                LoadingImg.fillAmount += Time.deltaTime/4;
            }
            else
            {
                Once = false;
                SceneManager.LoadScene(1);
            }
        }
    }
}
