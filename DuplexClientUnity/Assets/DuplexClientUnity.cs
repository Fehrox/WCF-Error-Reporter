using UnityEngine;
using System.Collections;
using DuplexClient;
using DuplexClient.ServiceReference;

public class DuplexClientUnity : MonoBehaviour, IServiceCallback {

	void Start () {

		Debug.Log("Connecting....");
		var callback = new Client();
		callback.Connect("net.tcp://localhost:8001/DuplexService/", this);
		Debug.Log("Connected!\nRecieving:\n");

	}

	public void RecieveMessage(string message)
	{
		Debug.Log(message);
	}
}
