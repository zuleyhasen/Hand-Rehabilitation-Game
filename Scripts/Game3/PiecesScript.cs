using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PiecesScript : MonoBehaviour
{

    private Vector3 RightPosition;
    public bool InRightPosition;

    // Start is called before the first frame update
    void Start()
    {
        RightPosition = transform.position;
        
    }
    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, RightPosition)< 0.3)
        {
            transform.position = RightPosition;
            InRightPosition = true;
        }
        else
        {
            InRightPosition = false;
        }
    }
}
