using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassGeneration : MonoBehaviour {

    private List<string> words = new List<string> {"seal","mouse" ,"password", "gravy", "julia"};
    private string passEnc;
    public string pass;
    public GameObject Loginbutton;

    // Use this for initialization
    void Start () {
        pass = words[Random.Range(0,4)];
        char[] passarr= pass.ToCharArray();
        passEnc = GenerateEncriptedPass(passarr);
        this.GetComponent<TextAppear>().desiredText = passEnc;
        Loginbutton.GetComponent<LogIn>().desiredPass = pass;

    }

    public string GenerateEncriptedPass(char[] pass)
    {
        for(int i = 0; i<pass.Length; i++)
        {
            char tmp = pass[i];
            int r = Random.Range(i,pass.Length);
            pass[i] = pass[r];
            pass[r] = tmp;
        }
        return new string(pass);
    }

    private bool IsInArray(int numb, int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (numb == array[i])
                return true;
        }
        return false;
    }

    // Update is called once per frame
	void Update () {
		
	}
}
