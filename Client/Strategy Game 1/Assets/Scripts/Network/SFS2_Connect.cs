using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public class SFS2_Connect : MonoBehaviour {
	
	public string configFile = "Scripts/Network/server_config.xml";
	public bool useConfigFile = false;
	public string serverIp = "127.0.0.1";
	public int serverPort = 9933;
	public string zoneName = "BasicExamples";
	public string userName = "";
	public string roomName = "The Lobby";
	public int sumA = 2;
	public int sumB = 3;
	
	SmartFox _sfs;
	
	// Use this for initialization
	void Start () {
		
		_sfs = new SmartFox();
		_sfs.ThreadSafeMode = true;
		_sfs.AddEventListener(SFSEvent.CONNECTION , OnConnection);	
		_sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
		_sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
		_sfs.AddEventListener(SFSEvent.CONFIG_LOAD_SUCCESS, OnConfigLoad);
		_sfs.AddEventListener(SFSEvent.CONFIG_LOAD_FAILURE, OnConfigFail);
		_sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
		_sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
		_sfs.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
		_sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnSumResponse);
		Security.PrefetchSocketPolicy(serverIp,serverPort);
		if(useConfigFile)
			_sfs.LoadConfig(Application.dataPath + "/" + configFile);
		else
			_sfs.Connect(serverIp,serverPort);
	
	}
	
	void OnSumResponse(BaseEvent e){
		string cmd = (string)e.Params["cmd"];
		ISFSObject objIn = (SFSObject)e.Params["params"];		
		if(cmd == "SUM_INTS")
			Debug.Log("Sum: " + objIn.GetInt("c"));
	}
	
	void OnJoinRoom(BaseEvent e){
		Debug.Log("Joined room " + e.Params["room"]);	
		_sfs.Send(new PublicMessageRequest("Hello everyone"));
	}
	
	void OnPublicMessage(BaseEvent e){
		Room room = (Room)e.Params["room"];
		User sender = (User)e.Params["sender"];
		Debug.Log("(" + room.Name + ") " + sender.Name + ":" + e.Params["message"]);
	}
	
	void OnJoinRoomError(BaseEvent e){
		Debug.Log("Failed to join the room");	
		Debug.Log("Error )" + e.Params["errorCode"] + ": " + e.Params["errorMessage"] );
	}
	
	void OnConfigLoad(BaseEvent e){
		Debug.Log("Config load successfully");
		_sfs.Connect(_sfs.Config.Host, _sfs.Config.Port);
	}
	
	void OnConfigFail(BaseEvent e){
		Debug.Log("Config couldn't be loaded");	
	}
	
	void OnLogin(BaseEvent e){
		Debug.Log("Welcome "+ e.Params["user"]);
		ISFSObject objOut = new SFSObject();
		objOut.PutInt("a",sumA);
		objOut.PutInt("b",sumB);
		_sfs.Send(new ExtensionRequest("SUM_INTS",objOut));
		_sfs.Send(new JoinRoomRequest(roomName));
	}
	
	void OnLoginError(BaseEvent e){
		Debug.Log("Login failed");
		Debug.Log("Error " + e.Params["errorCode"] + ": " + e.Params["errorMessage"] );
	}
	
	void OnConnection(BaseEvent e){
		if((bool)e.Params["success"])
			Debug.Log("Successfully connected");
		else
			Debug.Log("Connection to server failed");
		
		if(useConfigFile)
			zoneName = _sfs.Config.Zone;
		_sfs.Send(new LoginRequest(userName,"",zoneName));
	}
	
	
	// Update is called once per frame
	void Update () {
		_sfs.ProcessEvents();
	}
	
	void OnApplicationQuit(){
	
		if(_sfs.IsConnected)
			_sfs.Disconnect();
	}
	
}
