using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TerminalManager : MonoBehaviour {

    public GameObject directoryLine;
    public GameObject responseLine;

    public TMP_InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect sr;
    public GameObject msgList;

    Interpreter interpreter;

    private void Start() {
        interpreter = GetComponent<Interpreter>();
        if (sr != null && sr.content == null && msgList != null)
        {
            sr.content = msgList.GetComponent<RectTransform>();
        }
    }

    private void OnGUI() {
        
        if (terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            string userInput = terminalInput.text;

            ClearInputField();

            AddDirectoryLine(userInput);

            int lines = AddInterpreterLines(interpreter.Interpret(userInput));

            ScrollToBottom(lines);

            userInputLine.transform.SetAsLastSibling();

            terminalInput.ActivateInputField();

            terminalInput.Select();
        }
    }

    void ClearInputField(){
        terminalInput.text = "";
    }

    void AddDirectoryLine(string userInput){

        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35.0f);

        GameObject msg = Instantiate(directoryLine, msgList.transform);

        msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);
        
        // Use GetComponentsInChildren<TextMeshProUGUI>() since you are using TMPro
        TextMeshProUGUI[] texts = msg.GetComponentsInChildren<TextMeshProUGUI>();

        // Check if there's at least one component before trying to access it
        if (texts.Length > 0)
        {
            // If you have two components (e.g., one for the prompt, one for the user input)
            // You can check the length and assign to the correct index.
            // This is safer than hardcoding [1].
            if (texts.Length > 1)
            {
                texts[1].text = userInput;
            }
            else
            {
                // Fallback to the first component if there is only one
                texts[0].text = userInput;
            }
        }
    }

    int AddInterpreterLines(List<string> interpretation)
    {
        for (int i = 0; i < interpretation.Count; i++)
        {
            GameObject res = Instantiate(responseLine,msgList.transform);

            res.transform.SetAsLastSibling();

            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 35.0f);

            res.GetComponentInChildren<TextMeshProUGUI>().text = interpretation[i];
        }

        return interpretation.Count;
    }

    void ScrollToBottom(int lines){
        if (lines > 4)
        {
            sr.velocity = new Vector2(0, 450);
        }
        else
        {
            sr.verticalNormalizedPosition = 0;
        }
    }
}