using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextScript : MonoBehaviour{
    
    private string[] textLines;
    private string filePath = "Assets\\Resources\\wordList.txt";
    private Text uiText;
    private int originalTextLength;
    private string originalText;
    private string baseColor = "grey", specialColor = "black";
    private int currentWordIndex = 0;
    private bool wordCompleted = false;
    void Start(){
        loadTextFile(filePath);
        uiText = GetComponent<Text>();
        setUiText();
        formatTextWithColors(baseColor, specialColor);
    }

    void Update(){
        if(Input.anyKeyDown){
            if(wordCompleted) return;
            string key = Input.inputString;
            foreach (char c in key){
                if(currentWordIndex < originalTextLength && c == originalText[currentWordIndex]){
                    currentWordIndex++;
                    formatTextWithColors(baseColor, specialColor);
                } if(currentWordIndex >= originalTextLength){
                    wordCompleted = true;
                    StartCoroutine(wordComplete(0.5f));
                }
            }
        }    
    }

    IEnumerator wordComplete(float duration){
        formatTextWithColors("grey", "green");
        if(wordCompleted){
            Gun.Instance.addBullets(originalTextLength);
            GameController.instance.addWordTyped();
        }
        yield return new WaitForSeconds(duration);
        setUiText();
        formatTextWithColors(baseColor, specialColor);
        wordCompleted = false;
    }

    string getRandomText(){
        int randomIndex = Random.Range(0, textLines.Length);
        return textLines[randomIndex];
    }

    void setUiText(){
        currentWordIndex = 0;
        uiText.text = getRandomText();
        originalTextLength = uiText.text.Length;
        originalText = uiText.text;
    }

    void loadTextFile(string filePath){
        if (File.Exists(filePath)) {
            textLines = File.ReadAllLines(filePath);  
        } else {
            Debug.Log("File not found");
        }
    }

    void formatTextWithColors(string baseColor, string specialColor){
        if(originalTextLength > 0){
            string plainPart = $"<color={baseColor}>";
            string colorPart = $"<color={specialColor}>";

            for(int i = 0; i < currentWordIndex; i++){
                colorPart += originalText[i];
            }
            for(int j = currentWordIndex; j < originalTextLength; j++){
                plainPart += originalText[j];
            }

            colorPart += "</color>";
            plainPart += "</color>";

            uiText.text = colorPart + plainPart;
        }
    }
}