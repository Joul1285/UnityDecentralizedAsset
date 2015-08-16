/**
 * This class allow create menu in Unity editor
 * Add a Blockscan.com Manager as Gameobject in your scene
 * Add a PlayerInfo Manager as Gameobject in your scene
 * 
 * @author     Julien Burn 
 * @version    1.0
 * @date       14.08.2015
 **/

using UnityEngine;
using UnityEditor;

public class MenuItems : MonoBehaviour
{
	[MenuItem("BlockchainAssets/add blockscan.com manager")]
	private static void AddBlockscanManager ()
	{
		string objectName = "BlockscanManager";
		string fileLocation = "Assets/BlockchainAsset/" + objectName + ".prefab";
		GameObject BlockscanManagerPrefab = AssetDatabase.LoadAssetAtPath (fileLocation, typeof(GameObject)) as GameObject;
		if (BlockscanManagerPrefab == null) {
			Debug.Log ("Unable to create prefab.");
		}
		if (BlockscanManagerPrefab != null) {
			PrefabUtility.InstantiatePrefab (BlockscanManagerPrefab);
			Debug.Log ("Prefab created!");
		}      
	}

	[MenuItem("BlockchainAssets/add PlayerInfo manager")]
	private static void AddPlayerInfoManager ()
	{
		string objectName = "PlayerInfo";
		string fileLocation = "Assets/BlockchainAsset/" + objectName + ".prefab";
		GameObject PlayerInfoPrefab = AssetDatabase.LoadAssetAtPath (fileLocation, typeof(GameObject)) as GameObject;
		if (PlayerInfoPrefab == null) {
			Debug.Log ("Unable to create prefab.");
		}
		if (PlayerInfoPrefab != null) {
			PrefabUtility.InstantiatePrefab (PlayerInfoPrefab);
			Debug.Log ("Prefab created!");
		}      
	}
}