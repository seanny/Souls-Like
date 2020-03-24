using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace SoulsLike
{
    public class Console : MonoBehaviour
    {
        public GameObject commandConsole;
        public TMP_InputField commandField;
        string command;
        public List<string> paramaters = new List<string>();
        public List<string> commands = new List<string>();

        // Start is called before the first frame update
        void Start()
        {
            commandConsole.SetActive(false);
            commands.Add("quit");
            commands.Add("loadlevel");
            commands.Add("mainmenu");
            commands.Add("debugstats");
            commands.Add("setpos");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                commandConsole.SetActive(!commandConsole.activeSelf);
                if(commandConsole.activeSelf == true)
                {
                    commandField.ActivateInputField();
                    commandField.text = string.Empty;
                }
            }

            if(commandConsole.activeSelf == true)
            {
                bool found = false;
                if(Input.GetKeyUp(KeyCode.Return))
                {
                    paramaters.Clear();
                    Debug.Log($"Command: {commandField.text}");

                    string[] commandParts = commandField.text.Split('(', ',', ')');
                    commandField.text = string.Empty;

                    for(int i = 0; i < commandParts.Length; i++)
                    {
                        if(i == 0)
                        {
                            command = commandParts[i];
                        }
                        else
                        {
                            paramaters.Add(commandParts[i]);
                        }
                    }

                    foreach(var item in commands)
                    {
                        var cmd = item.ToLower();
                        if (cmd == command.ToLower())
                        {
                            found = true;
                            switch(cmd)
                            {
                                case "quit":
                                    QuitApp();
                                    break;
                                case "mainmenu":
                                    MainMenu();
                                    break;
                                case "loadlevel":
                                    if (paramaters.Count > 0)
                                    {
                                        Debug.Log($"Paramater: {paramaters[0]}");
                                        LoadLevel(paramaters[0]);
                                    }
                                    break;
                                case "setpos":
                                    Debug.Log($"SetPos param count == {paramaters.Count}");
                                    if (paramaters.Count >= 3)
                                    {
                                        if(float.TryParse(paramaters[0], out float x)
                                            && float.TryParse(paramaters[1], out float y)
                                            && float.TryParse(paramaters[2], out float z))
                                        {
                                            Debug.Log($"SetPos({x}, {y}, {z})");
                                            PlayerActor.instance.transform.position = new Vector3(x, y, z);
                                        }

                                    }
                                    break;
                                case "freecam":
                                    throw new NotImplementedException("Free Camera command not implemented");
                                case "freecamspeed":
                                    throw new NotImplementedException("Free Camera Speed command not implemented");
                                case "debugstats":
                                    DebugStats.ToggleDebugStats();
                                    break;
                            }
                        }
                    }

                    if (!found)
                    {
                        Debug.LogError($"Command {command} does not exist.");
                    }
                }
            }
        }

        private void MainMenu()
        {
            LoadScene.instance.ReturnToMainMenu();
        }

        private void LoadLevel(string levelName)
        {
            LoadScene.instance.LoadLevel(levelName);
        }

        private void QuitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}