using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{

    // End Scene
    public static bool Win { get; set; } = true;
    public static int Time { get; set; } = 300; // time
    public static int RoomCode { get; set; }
    public static int HintsUsed { get; set; }

    // Email Validation
    public static string ActivationMessage;

}
