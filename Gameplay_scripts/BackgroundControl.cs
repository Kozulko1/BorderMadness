using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public GameObject BackgroundHP3;
    public GameObject BackgroundHP2;
    public GameObject BackgroundHP1;
    public GameObject BackgroundHP0;

    public void SetHP3()
    {
        this.BackgroundHP3.SetActive(true);
        this.BackgroundHP2.SetActive(false);
        this.BackgroundHP1.SetActive(false);
        this.BackgroundHP0.SetActive(false);
    }
    public void SetHP2()
    {
        this.BackgroundHP3.SetActive(false);
        this.BackgroundHP2.SetActive(true);
        this.BackgroundHP1.SetActive(false);
        this.BackgroundHP0.SetActive(false);
    }
    public void SetHP1()
    {
        this.BackgroundHP3.SetActive(false);
        this.BackgroundHP2.SetActive(false);
        this.BackgroundHP1.SetActive(true);
        this.BackgroundHP0.SetActive(false);
    }
    public void SetHP0()
    {
        this.BackgroundHP3.SetActive(false);
        this.BackgroundHP2.SetActive(false);
        this.BackgroundHP1.SetActive(false);
        this.BackgroundHP0.SetActive(true);
    }
}
