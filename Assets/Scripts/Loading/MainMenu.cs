using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public class MainMenu : MonoBehaviour
    {
        public void OnContinue()
        {
            LoadScene.instance.LoadLevel("SeanTest");
        }
    }
}