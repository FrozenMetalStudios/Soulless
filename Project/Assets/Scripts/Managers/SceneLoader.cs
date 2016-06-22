/*******************************************************************
 * 
 * Copyright (C) 2015 Frozen Metal Studios - All Rights Reserved
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Frozen Metal Studios. The intellectual and 
 * technical concepts contained herein are proprietary to 
 * Frozen Metal Studios are protected by copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from Frozen Metal Studios.
 * 
 * *****************************************************************
 * 
 * Filename: SceneLoader.cs
 * 
 * Description: Controls Initial Game Entry and Scene Management.
 * 
 *******************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Diagnostics;
using Assets.Scripts.Utility;
using Assets.Scripts.CustomEditor;


namespace Assets.Scripts
{
    class SceneLoader : MonoBehaviour
    {
        // --------------------------------------------------------------------
        public string GameTitle = "Default Game Title";
        [ReadOnly]
        public bool DisplayedLogo = false;

        // --------------------------------------------------------------------
        private string _CurrentScene;

        // --------------------------------------------------------------------
#if !FINAL
        private float _FPS;
        private float _FPSTime;
        private int _FPSFrames;
#endif

        //---------------------------------------------------------------------
        public ARKLogger logger = new ARKLogger();

        // --------------------------------------------------------------------
        static SceneLoader _Singleton = null;

        // --------------------------------------------------------------------
        public static SceneLoader Singleton
        {
            get { return _Singleton; }
        }

        // --------------------------------------------------------------------
        void Awake()
        {
            // Ensure only 1 singleton
            if (null != _Singleton)
            {
                UnityEngine.Debug.LogError("SceneLoader: Multiple SceneLoaders violate Singleton pattern.");
            }
            _Singleton = this;

            // Initialize the Logger
            logger.Initialize();

            // Trace Startup
            ARKLogger.LogMessage(eLogCategory.Control,
                                 eLogLevel.Info,
                                 "SceneLoader: Awake.");

            // Init the FPS Tracker
            InitFPS();

            // Are we in the Application Scene?
            if (SceneManager.GetActiveScene().name == "Entry")
            {
#if !FINAL
                // Load the Debug Scene Selector
                ARKLogger.LogMessage(eLogCategory.Control,
                                     eLogLevel.Info,
                                     "SceneLoader: Loading SceneSelector");
                _CurrentScene = "Launcher";
                SceneManager.LoadScene(_CurrentScene);
#endif
            }

            // Make sure this object persists between scene loads.
            DontDestroyOnLoad(gameObject);
        }

        //-------------------------------------------------------------------------------------------------------------------------
        void Start()
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public void LoadLevelSync(string level)
        {
            ARKLogger.LogMessage(eLogCategory.Control,
                                 eLogLevel.Info,
                                 "SceneLoader: Loading Level, " + level);

            _CurrentScene = level;
            SceneManager.LoadScene(_CurrentScene);
        }
        
        //-------------------------------------------------------------------------------------------------------------------------
        public void ResetCurrentLevel()
        {
            ARKLogger.LogMessage(eLogCategory.Control,
                                 eLogLevel.Info,
                                 "SceneLoader: Restarting Level, " + _CurrentScene);

            SceneManager.LoadScene(_CurrentScene);
        }

        //-------------------------------------------------------------------------------------------------------------------------
#if !FINAL
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            }

            UpdateFPS();
        }
#endif

        //-------------------------------------------------------------------------------------------------------------------------
        public void Quit()
        {
            ARKLogger.LogMessage(eLogCategory.Control,
                                 eLogLevel.Info,
                                 "SceneLoader: Terminating.");
            Application.Quit();
        }

        //-------------------------------------------------------------------------------------------------------------------------
#if !FINAL
        void OnGUI()
        {
            RenderFPS();
        }
#endif

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        void InitFPS()
        {
#if !FINAL
            // Clear the fps counters.
            _FPS = 0.0f;
            _FPSTime = 0.0f;
            _FPSFrames = 0;
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        void UpdateFPS()
        {
#if !FINAL
            // Increase the number of frames.
            _FPSFrames++;

            // Accumulate time.
            _FPSTime += Time.deltaTime;

            // Have we reached 1 second.
            if (_FPSTime > 1.0f)
            {
                // Store number of frames per second.
                _FPS = _FPSFrames;

                // Reset for the next frame.
                _FPSTime -= 1.0f;
                _FPSFrames = 0;
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        void RenderFPS()
        {
#if !FINAL
            // Display on screen the current Frames Per Second.
            string fps_string = String.Format("FPS: {0:d2}", (int) _FPS);
            GUI.Label(new Rect(10, 10, 600, 30), fps_string);
#endif
        }

        public void OnDestroy()
        {
            // Cleanup the logger
            logger.Cleanup();
        }
    }
}
