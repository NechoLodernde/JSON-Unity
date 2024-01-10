using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Linq;

public class ClientScript : MonoBehaviour
{
    private const string serverAddress = "127.0.0.1"; // Replace with your actual public IP
    private const int serverPort = 5000; // Use the desired port

    IEnumerator Start()
    {
        //Debug.unityLogger.logEnabled = true;

        // Construct the URL for the server
        string serverURL = $"http://{serverAddress}:{serverPort}";

        string requestData = "Hello from Unity Client!";

        // Use UnityWebRequest to connect to the server asynchronously
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(serverURL, requestData))
        {
            yield return request.SendWebRequest();
            Debug.Log($"Sending web request to server");
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Request Success");
                // Process the response data
                string responseData = request.downloadHandler.text;
                Debug.Log($"Unjoined and Unsplitted data: {responseData}");
                char[] separators = new char[] { '%', '2', '0', '1' };
                string fixedData = string.Join(" ", responseData.Split(separators, System.StringSplitOptions.RemoveEmptyEntries)
                    .ToArray());
                Debug.Log($"Received from server: {fixedData}");
            }
            else
            {
                Debug.Log($"Request Failed");
                Debug.LogError($"Error: {request.error}");
            }
        }
    }
}
