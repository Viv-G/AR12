namespace GoogleARCore.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class EntryScript : MonoBehaviour
    {
        public static string HostSet = "172.20.10.2";
        public Text Text_Status;
        private string content;

        public void SetIP(string HostField)
        {
            Debug.Log("Set IP \n");
            HostSet = HostField;
         //   Connection.Connect(HostSet);
        }

        public void LoadScene()
        {
            HelloAR.Connection.Connect(HostSet);

            if (HelloAR.Connection.s == null)
            {
                content = "Unable To Connect... Try again";
                return;
            }
            else
            {
                content = "Connected To: " + HostSet;
                SceneManager.LoadScene("HelloAR");
            }
        }

        public void LoadHome()
        {
            Debug.Log("Set IP \n");
            HostSet = "192.168.8.100";
            HelloAR.Connection.Connect(HostSet);
            if (HelloAR.Connection.s == null)
            {
                content = "Unable To Connect... Try again";
                return;
            }
            else
            {
                content = "Connected To: " + HostSet;
                SceneManager.LoadScene("HelloAR");
            }
        }

        public void LoadHotSpot()
        {
            Debug.Log("Set IP \n");
            HostSet = "172.20.10.2";
            HelloAR.Connection.Connect(HostSet);
            if (HelloAR.Connection.s == null)
            {
                content = "Unable To Connect... Try again";
                return;
            }
            else
            {
                content = "Connected To: " + HostSet;
                SceneManager.LoadScene("HelloAR");
            }
        }

        public void SetConf(string c_val)
        {
            float cfloat_val = float.Parse(c_val);
            Common.PointcloudVisualizer.setConf = cfloat_val;
            content = "Set Confidence To: " + c_val;
            
        }


        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Text_Status.text = content;

            _UpdateApplicationLifecycle();

            // If the player has not touched the screen, we are done with this update.
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

        }
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }


            if (m_IsQuitting)
            {
                return;
            }
        }


        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>(
                            "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }

    }
}