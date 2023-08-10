using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fps_text;

    private void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fps_text.text = fps.ToString();
    }
}
