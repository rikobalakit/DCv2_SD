using OWOGame;
using System.Collections;
using UnityEngine;

public class OWOController : MonoBehaviour
{
    private bool isConnected = false;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        // Connect to the OWO device during initialization
        yield return StartCoroutine(Connect());
    }

    private void Update()
    {
        // Check for key presses to send sensations
        if (isConnected)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCoroutine(SendBakedSensation());
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SendDynamicSensation();
            }
        }
    }

    private IEnumerator Connect()
    {
        // Declare two baked sensations, Ball(0) and Dart(1)
        var auth = GameAuth.Parse("0~Ball~100,1,100,0,0,0,Impact|0%100,1%100,2%100,3%100,4%100,5%100~impact-0~#1~Dart~12,1,100,0,0,0,Impact|5%100~impact-1~");
        OWO.Configure(auth);

        // Use a Task to connect and wait until it's done
        var connectTask = OWO.AutoConnect();
        while (!connectTask.IsCompleted)
        {
            yield return null;
        }

        if (connectTask.IsFaulted)
        {
            Debug.LogError("Failed to connect to OWO device!");
        }
        else
        {
            Debug.Log("Connected to OWO device successfully!");
            isConnected = true;
        }
    }

    private void SendDynamicSensation()
    {
        // Send the Dagger dynamic sensation
        OWO.Send(Sensation.Dagger);
        Debug.Log("Dynamic sensation (Dagger) sent!");
    }

    private IEnumerator SendBakedSensation()
    {
        // Send the Ball baked sensation
        OWO.Send("0");

        // Wait for the baked sensation to finish
        yield return new WaitForSeconds(0.1f);

        Debug.Log("Baked sensation (Ball) sent!");
    }
}
