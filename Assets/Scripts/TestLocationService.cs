﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLocationService : MonoBehaviour
{

    public Text StatusText;
    public Text LatText;
    public Text LongText;

    private void Start()
    {
        StartCoroutine(GetLocation());
    }

    IEnumerator GetLocation()
    {

        StatusText.text = "Beginning GPS initialisation...";

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            StatusText.text = "Location not enabled!";
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            StatusText.text = "Initialisation timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            StatusText.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            //print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            StatusText.text = "Success";
            LatText.text = Input.location.lastData.latitude.ToString();
            LongText.text = Input.location.lastData.longitude.ToString();

        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
