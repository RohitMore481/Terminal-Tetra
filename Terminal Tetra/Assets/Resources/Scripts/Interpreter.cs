using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class Interpreter : MonoBehaviour
{
    List<string> response = new List<string>();

    public List<string> Interpret(string userInput){

        response.Clear();

        string[] args = userInput.Split();

        if (args[0] == "help")
        {
            response.Add("If you want to use the terminal, type 'Play' ");
            response.Add("This is Line Two ");
            return response;
        }
        else
        {
            response.Add("Command not recognized. Type help for a list of commands.");
            return response;
        }
    }
}
