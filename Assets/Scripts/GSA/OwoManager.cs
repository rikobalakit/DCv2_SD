using GSA;
using OWOGame;
using System.Collections;
using UnityEngine;

public class OwoManager : BaseManager
{
    private bool isConnected = false;

    public override void Initialize(NewGameManager gameManager)
    {
        base.Initialize(gameManager);
        StartCoroutine(Connect());
    }
    // Start is called before the first frame update
    
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

    public void SendDagger()
    {
        // Send the Dagger dynamic sensation
        OWO.Send(Sensation.Dagger);
        Debug.Log("Dynamic sensation (Dagger) sent!");
    }
    
    public void SendBall()
    {
        StartCoroutine(SendBakedSensation());
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
