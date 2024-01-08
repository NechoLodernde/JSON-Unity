using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestServerScript : MonoBehaviour
{
    private const string serverAddress = "127.0.0.1"; // Replace with your public IP address
    private const int serverPort = 5000; // Use the desired port

    IEnumerator Start()
    {
        string serverURL = $"http://{serverAddress}:{serverPort}";
        // Use UnityWebRequest to connect to the server asynchronously
        using (UnityWebRequest request = UnityWebRequest.Get(serverURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Process the response data
                string responseData = request.downloadHandler.text;
                Debug.Log($"Received from server: {responseData}");
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }
}
