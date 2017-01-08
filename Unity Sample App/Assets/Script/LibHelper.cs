using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibHelper : MonoBehaviour {

	private Text mText;

	private string log;

	// Use this for initialization
	void Start () {
		mText = GetComponent<Text>();

		log = "";
		
		// Access public static variable
		double pi = new AndroidJavaObject("com.wm4n.android_sample_lib.Calc").GetStatic<double>("PI");
		log += string.Format("public static variable: {0}\n", pi);

		// Access public variable
		int prime = new AndroidJavaObject("com.wm4n.android_sample_lib.Calc").Get<int>("SMALLEST_PRIME");
		log += string.Format("public variable: {0}\n", prime);
	}
	
	// Update is called once per frame
	void Update () {
		mText.text = log;
	}
}
