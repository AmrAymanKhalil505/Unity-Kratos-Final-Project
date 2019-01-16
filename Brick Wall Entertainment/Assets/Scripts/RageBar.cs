using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour {

	private GameObject Kratos;
	public GameObject Rimage;

	private bool ScaleIncreasing = true;

	// Use this for initialization
	void Start () {
		Kratos = GameObject.FindGameObjectWithTag("Kratos");
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image>().fillAmount = (float)Kratos.GetComponent<PlayerController>().currentRage/(float)Kratos.GetComponent<PlayerController>().RageMeter;
		if(GetComponent<Image>().fillAmount == 1){
			Rimage.SetActive(true);
			if(Rimage.transform.localScale.x > 1.3){
				ScaleIncreasing = false;
			}
			if(Rimage.transform.localScale.x < 0.9){
				ScaleIncreasing = true;
			}
			if(ScaleIncreasing){
				Rimage.transform.localScale = Rimage.transform.localScale + new Vector3(0.01f,0.01f,0.01f);
			}
			else{
				Rimage.transform.localScale = Rimage.transform.localScale - new Vector3(0.01f,0.01f,0.01f);
			}
		}
		else{
			Rimage.SetActive(false);
			Rimage.transform.localScale = new Vector3(1,1,1);
		}
	}
}
