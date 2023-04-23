using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLIMITER : MonoBehaviour
{
  // Limit the FPS of the game so no
  // funny business happens
  private void Start() {
    Application.targetFrameRate = 60;
  }
}
