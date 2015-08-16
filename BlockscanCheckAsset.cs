/**
 * This class allow to call blockscan API to find assets
 * depending to a public key bitcoin address
 * You can add asset name to be discover and perform action if found
 * 
 * @author     Julien Burn 
 * @version    1.0
 * @date       14.08.2015
 **/

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON; //use SimpleJSON custom class to parse json from blockscan
using System.Reflection;


public class BlockscanCheckAsset : MonoBehaviour {

	//url of blockscan.com API to check assets balance according to an bitcoin address
	public string blockScanURL = "http://xcp.blockscan.com/api2?module=address&action=balance";
	//publicKey from the player
	public string publicKey;
	//Array of name's assets you want to check
	public string[] assetName;

	//Allow to call an object contening all player informations (mostly public key)
	public GameObject playerInfo;
	private PlayerInfo m_playerInfo;

	//This objects are uses to download image from an url according to the assets found
	public GameObject rawImage;
	public Canvas canvas;
	public GameObject[] frame;


	public void Start(){
		//initialiazed player informations object
		m_playerInfo = playerInfo.GetComponent<PlayerInfo> ();
	}

	/**
	 * Main function to call at first from your game
	 * call blockchain.com API to get an json contening all assets and amount
	 * according to player's public key
	 **/
	public void ScanBlock ()
	{
		publicKey = m_playerInfo.getpublicKey();
		string url = blockScanURL+"&btc_address="+publicKey;
		
		WWW www = new WWW (url);
		StartCoroutine (WaitForRequestBlockscan (www));
	}

	/**
	 * Call blockscan API
	 * */
	IEnumerator WaitForRequestBlockscan (WWW www)
	{
		yield return www;
		JSONNode jsonResponse = null;

		//if no www error call parse the response and call ParseJson function
		if (www.error == null) {			
			Debug.Log ("WWW Ok!: " + www.text);
			jsonResponse = JSON.Parse (www.text);
			ParseJson(jsonResponse);
		//else debug error response (blockscan server down? no internet connection?)
		} else {
			Debug.Log ("WWW Error!: " + www.text);
			jsonResponse = null;
		} 

	}

	/**
	 * Parse json response from blockscan
	 * call asset's function if same name asset are found in json response
	 * */
	public void ParseJson(JSONNode jsonResponse){
		//check if asset has been found at this address (bad format address?)
		if (jsonResponse == null) {
			Debug.Log ("no asset found at this address");
			return;
		} 

		Debug.Log ("jsonReponse text: " + jsonResponse.ToString ());
		Debug.Log ("jsonResponse data count: " + jsonResponse ["data"].Count);

		//store number of asset found in json response
		int nbrOfAsset = jsonResponse ["data"].Count;

		//iterate to check if asset name are the same in assetName array and asset from json response
		for (int i = 0; i < nbrOfAsset; i++) {
			for (int y = 0; y < assetName.Length; y++){
				if (jsonResponse ["data"] [i] ["asset"].Value == assetName[y]){
					string methodName = "Asset"+i;
					
					//Get the method information using the method info class
					MethodInfo mi = this.GetType().GetMethod(methodName);
					
					//Invoke the method Asset1, Asset2, etc..
					var arguments = new object[] { y };
					mi.Invoke(this, arguments);
				}
			}
			Debug.Log ("asset name: " + jsonResponse ["data"] [i] ["asset"].Value);
			Debug.Log ("asset balance: " + jsonResponse ["data"] [i] ["balance"].Value);
		}

	}

	/**
	 * Perform action if asset name from array has been found
	**/
	public void Asset0(int imgPos){
		string url = "http://api.moonga.com/RCT/cp/cards/view/retina/regular/en/1700.png";
		WWW wwwImg = new WWW (url);
		Debug.Log ("Asset0 has been call");
		StartCoroutine (DownloadCardImg (wwwImg, imgPos));
	}

	/**
	 * Perform action if asset name from array has been found
	**/
	public void Asset1(int imgPos){
		//do action
		string url = "http://api.moonga.com/RCT/cp/cards/view/retina/regular/en/1710.png";
		WWW wwwImg = new WWW (url);
		Debug.Log ("Asset1 has been call");
		StartCoroutine (DownloadCardImg (wwwImg, imgPos));
	}

	/**
	 * for purpose only, download img from an URL according to asset found
	**/
	IEnumerator DownloadCardImg (WWW www, int imgPos)
	{
		// Wait for download to complete
		yield return www;
		Vector3 newPos = new Vector3 (0, 0, 0);
		// assign texture
		GameObject newRaw = Instantiate (rawImage) as GameObject;
		newRaw.transform.SetParent (frame [imgPos].transform, false);
		newRaw.GetComponent<RawImage> ().texture = www.texture;
		newRaw.transform.position += newPos;
		
	}

}
