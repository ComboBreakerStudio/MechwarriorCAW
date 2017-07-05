using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadoutInfo : NetworkBehaviour {
	[SyncVar]
	public bool partEnabled;
}
