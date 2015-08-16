/**
 * This class initialized a playerInfo to store informations as bitcoin public key
 * or whathever you want
 * 
 * @author     Julien Burn 
 * @version    1.0
 * @date       14.08.2015
 **/

using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	[SerializeField]
	private string phpSSID;
	[SerializeField]
	private string UDID;
	[SerializeField]
	private string publicKey;

	public string getPhpSsid(){
		return this.phpSSID;
	}

	public string getUDID(){
		return this.UDID;
	}

	public string getpublicKey(){
		return this.publicKey;
	}

	public void setPhpSsid(string phpSsid){
		this.phpSSID = phpSsid;
	}

	public void setUDID(string UDID){
		this.UDID = UDID;
	}

	public void setPublicKey(string publicKey){
		this.publicKey = publicKey;
	}
}
