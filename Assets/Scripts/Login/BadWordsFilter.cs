using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadWordsFilter : MonoBehaviour
{
    InputField inFieldText;
    string myString;
    [SerializeField]
    string[] badWords;

    public TMP_Text Feedback;
    // Start is called before the first frame update
    void Start()
    {
        inFieldText = GetComponent<InputField>();
    }

    public void ChangeString(string stringin)
    {
        Feedback.text = "";
        myString = stringin;
        BadWordParser();
    }

    private void BadWordParser()
    {
        for (int i = 0; i < badWords.Length; i++)
        {
            if (myString.ToLower().Contains(badWords[i])) 
            {
                for (int j = 0; j < myString.Length; j++) 
                { 
                    if (myString.ToLower()[j] == badWords[i][0])
                    {
                        string temp = myString.Substring(j, badWords[i].Length);
                        if (temp.ToLower() == badWords[i])
                        {
                            myString = myString.Remove(j, badWords[i].Length);
                            if (myString != null)
                            {
                                inFieldText.text = myString.ToString();
                            }
                            else 
                            {
                                inFieldText.text = "";
                            }
                            //BadWordParser();
                            return;
                        }
                    }
                
                
                }
            
            }
        }
    }

    
}
