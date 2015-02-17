using UnityEngine;
using System.Collections;

public class CreditsMenu : MonoBehaviour 
{
	private float speed = 0.05f;

	void Start()
	{
		guiText.text = "Credits\n" + " \n" +
			"Made by \n" +
			"\t-Dori Chan\n" +
			"\t-for VG4430 Game Engine Scripting\n" +
			"Big Furniture Pack by\n" + "\t-Vertex Studios\n" +
			"8 Bit Sounds\n" + "\t-by Electrodynamics\n\n" +
			"Press Escape to return to the Main Menu";
	}

	void Update()
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime);

		if(Input.GetKey(KeyCode.Escape)) {
			Application.LoadLevel("main_menu");
		}
	}
}
