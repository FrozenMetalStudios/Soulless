using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Menu
{
    public class SceneSelector : MonoBehaviour
    {
        //------------------------------------------------------------
        enum EMainMenuState
        {
            Startup,
            DisplayLogo,
            Menu,
        };

        //------------------------------------------------------------
        enum EMenuButtonId
        {
            None,

            StartGame,
            Quit,
        };

        //------------------------------------------------------------
        public GameObject AppPrefab;

        //------------------------------------------------------------
        private EMainMenuState StateId;
        private EMenuButtonId ButtonId;
        private float LogoDisplayTimer;

        //------------------------------------------------------------
        void Start()
        {
            // Is the controlling App already in existence?
            if (GameObject.Find("Managers") == null)                  
            {
                Instantiate(AppPrefab);
            }

            Logger.LogMessage(eLogCategory.Control,
                              eLogLevel.Trace, 
                              "MainMenu: Starting.");

            StateId = EMainMenuState.Startup;
            ButtonId = EMenuButtonId.None;
        }

        //------------------------------------------------------------
        void Update()
        {
            switch (StateId)
            {
                case EMainMenuState.Startup:
                    Logger.LogMessage(eLogCategory.Control,
                                      eLogLevel.Trace, 
                                      "MainMenu: State: Startup.");

                    // Here we would do any menu preparation work.

                    // Has the logo been displayed already?
                    if (SceneLoader.Singleton.DisplayedLogo)
                        // Move to the next state.
                        StateId = EMainMenuState.Menu;
                    else
                    {
                        SceneLoader.Singleton.DisplayedLogo = true;
                        StateId = EMainMenuState.DisplayLogo;
                        LogoDisplayTimer = 1.0f;
                    }
                    break;

                case EMainMenuState.DisplayLogo:
                    LogoDisplayTimer -= Time.deltaTime;

                    if (LogoDisplayTimer < 0.0f)
                        StateId = EMainMenuState.Menu;
                    break;

                case EMainMenuState.Menu:
                    if (ButtonId == EMenuButtonId.StartGame)
                    {
                        Logger.LogMessage(eLogCategory.Control,
                                          eLogLevel.Trace,                                                                                    
                                          "MainMenu: Prototype Scene Selected.");
                        SceneLoader.Singleton.LoadLevel("Prototype Scene");
                    }
                    else if (ButtonId == EMenuButtonId.Quit)
                    {
                        Logger.LogMessage(eLogCategory.Control,
                                          eLogLevel.Trace,
                                          "MainMenu: Quit Selected.");
                        SceneLoader.Singleton.Quit();
                    }

                    ButtonId = EMenuButtonId.None;
                    break;

                default:
                    Logger.LogMessage(eLogCategory.Control,
                                      eLogLevel.Error,
                                      "Really shouldn't be here... illegal state id set.");

                    // Auto recover.
                    StateId = EMainMenuState.Startup;
                    break;
            }
        }

        //-----------------------------------------------------------------
        void OnGUI()                                                                                                 
        {
            if (StateId == EMainMenuState.DisplayLogo)
            {
                GUI.Button(new Rect(100.0f, 100.0f, 500.0f, 500.0f), "BIG LOGO DISPLAYED HERE!");
            }
            else
            {
                if (GUI.Button(new Rect(200.0f, 100.0f, 300.0f, 100.0f), "Start Game"))
                {
                    ButtonId = EMenuButtonId.StartGame;
                }

                if (GUI.Button(new Rect(200.0f, 350.0f, 300.0f, 100.0f), "Quit"))
                {
                    ButtonId = EMenuButtonId.Quit;                                                  
                }
            }
        }
    }
}