using MelonLoader;
using UnityEngine;
using Il2CppScheduleOne;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.PlayerScripts;
using System.Collections;
using FactoryTest;
using Il2CppInterop.Runtime.Injection;
using CustomItemCreator;
using Il2CppScheduleOne.ObjectScripts;
using Il2CppScheduleOne.Management;
using Il2CppScheduleOne.EntityFramework;
using Il2CppScheduleOne.UI.Shop;
using static Il2CppScheduleOne.UI.Phone.PhoneShopInterface;
using AssetLoader;
using Il2CppScheduleOne.Storage;

public class Main : MelonMod
{
    GameObject autoGrowTent;
    GameObject autoMixingStation;
    GameObject conveyorBeltStraight;
    GameObject conveyorBeltLeft;
    GameObject conveyorBeltRight;
    GameObject largeStorageRack;
    GameObject mediumStorageRack;
    GameObject smallStorageRack;
    GameObject itemOutput;
    GameObject transferPointObject;

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (sceneName == "Main")
        {
            largeStorageRack = Resources.Load<GameObject>("storage/storagerack_large/StorageRack_Large");

            itemOutput = new GameObject("Item Output front_right");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(-0.5f, 0.5f, -0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            itemOutput.transform.parent = largeStorageRack.transform;

            itemOutput = new GameObject("Item Output front_left");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(0.5f, 0.5f, -0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            itemOutput.transform.parent = largeStorageRack.transform;

            itemOutput = new GameObject("Item Output back_right");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(-0.5f, 0.5f, 0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            itemOutput.transform.parent = largeStorageRack.transform;

            itemOutput = new GameObject("Item Output back_left");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(0.5f, 0.5f, 0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            itemOutput.transform.parent = largeStorageRack.transform;

            transferPointObject = new GameObject("Transfer Point back_right");
            transferPointObject.transform.parent = largeStorageRack.transform;
            transferPointObject.transform.localPosition = new Vector3(0.5f, -0.37f, 0.75f);
            transferPointObject.transform.localRotation = Quaternion.identity;

            transferPointObject = new GameObject("Transfer Point back_left");
            transferPointObject.transform.parent = largeStorageRack.transform;
            transferPointObject.transform.localPosition = new Vector3(-0.5f, -0.37f, 0.75f);
            transferPointObject.transform.localRotation = Quaternion.identity;

            transferPointObject = new GameObject("Transfer Point front_right");
            transferPointObject.transform.parent = largeStorageRack.transform;
            transferPointObject.transform.localPosition = new Vector3(0.5f, -0.37f, -0.75f);
            transferPointObject.transform.localRotation = Quaternion.identity;

            transferPointObject = new GameObject("Transfer Point front_left");
            transferPointObject.transform.parent = largeStorageRack.transform;
            transferPointObject.transform.localPosition = new Vector3(-0.5f, -0.37f, -0.75f);
            transferPointObject.transform.localRotation = Quaternion.identity;

            largeStorageRack.AddComponent<LargeStorageRack>();

            mediumStorageRack = Resources.Load<GameObject>("storage/storagerack_medium/StorageRack_Medium");

            itemOutput = new GameObject("Item Output");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(0f, 0.5f, -0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            itemOutput.transform.parent = mediumStorageRack.transform;

            itemOutput = new GameObject("Item Output");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(0f, 0.5f, 0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            itemOutput.transform.parent = mediumStorageRack.transform;

            smallStorageRack = Resources.Load<GameObject>("storage/storagerack_small/StorageRack_Small");

            itemOutput = new GameObject("Item Output");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(0f, 0.5f, -0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            itemOutput.transform.parent = smallStorageRack.transform;

            itemOutput = new GameObject("Item Output");
            itemOutput.AddComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("ItemOutput");
            itemOutput.AddComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("ItemOutput");
            itemOutput.transform.localPosition = new Vector3(0f, 0.5f, 0.25f);
            itemOutput.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            itemOutput.transform.parent = smallStorageRack.transform;


            autoMixingStation = GameObject.Instantiate(Resources.Load<GameObject>("stations/mixingstation/MixingStation_Built"), new Vector3(0, 0, 0), Quaternion.identity, null);
            autoMixingStation.name = "AutoMixingStation_Built";
            autoMixingStation.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("auto_mixer");
            autoMixingStation.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<MeshCollider>().sharedMesh = AssetLoading.GetEmbeddedMesh("auto_mixer");
            autoMixingStation.transform.GetChild(1).GetChild(0).GetChild(2).transform.localRotation = Quaternion.Euler(0, 90, 0);
            autoMixingStation.transform.GetChild(1).GetChild(0).GetChild(2).transform.localPosition = new Vector3(0, 0, 0);
            autoMixingStation.transform.position = new Vector3(0, -10000, 0);
            GameObject.Destroy(autoMixingStation.transform.GetChild(1).GetChild(0).GetChild(0).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(1).GetChild(0).GetChild(1).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(0).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(7).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(11).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(12).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(13).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(16).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(14).gameObject);
            GameObject.Destroy(autoMixingStation.transform.GetChild(15).gameObject);

            autoGrowTent = GameObject.Instantiate(Resources.Load<GameObject>("pots/growtent/GrowTent_Built"), new Vector3(0, 0, 0), Quaternion.identity, null);
            autoGrowTent.name = "AutoGrowTent_Built";
            autoGrowTent.transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("growtent");
            autoGrowTent.transform.position = new Vector3(0, -10000, 0);
            // transfer point
            GameObject transferPoint = new GameObject("Transfer Point");
            transferPoint.transform.parent = autoGrowTent.transform;
            transferPoint.transform.localPosition = new Vector3(0f, 0.2432f, -1f);
            transferPoint.transform.localRotation = Quaternion.identity;
            autoGrowTent.AddComponent<AutoGrowTent>();

            conveyorBeltStraight = GameObject.Instantiate(Resources.Load<GameObject>("furniture/woodsquaretable/WoodSquareTable"), new Vector3(0, 0, 0), Quaternion.identity, null);
            conveyorBeltStraight.name = "ConveyorStraight_Built";
            conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(4).localPosition = new Vector3(0, 0, 0);
            conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(4).localRotation = Quaternion.identity;
            conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("conv_straight");
            conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("Conveyor");
            GameObject.Destroy(conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<BoxCollider>());
            MeshCollider collider = conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.AddComponent<MeshCollider>();
            collider.convex = true;
            collider.sharedMesh = AssetLoading.GetEmbeddedMesh("conv_straight");
            GameObject.Destroy(conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
            GameObject.Destroy(conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
            GameObject.Destroy(conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(2).gameObject);
            GameObject.Destroy(conveyorBeltStraight.transform.GetChild(0).GetChild(0).GetChild(3).gameObject);
            conveyorBeltStraight.transform.position = new Vector3(0, -10000, 0);
            GameObject.Destroy(conveyorBeltStraight.GetComponent<StorageEntityInteractable>());
            GameObject.Instantiate(autoGrowTent.transform.GetChild(15).gameObject, new Vector3(0, 0, 0), Quaternion.identity, conveyorBeltStraight.transform);
            conveyorBeltStraight.transform.GetChild(9).localPosition = new Vector3(0f, -0.25f, 0.7f);
            conveyorBeltStraight.transform.GetChild(9).localRotation = Quaternion.Euler(90, 270, 0);
            conveyorBeltStraight.transform.GetChild(9).gameObject.SetActive(true);
            conveyorBeltStraight.transform.GetChild(0).name = "Conveyor";
            conveyorBeltStraight.transform.GetChild(0).GetChild(0).name = "conveyor";
            GameObject transferBox = new GameObject("Transfer Box");
            transferBox.transform.parent = conveyorBeltStraight.transform;
            transferBox.transform.localPosition = Vector3.zero;
            transferBox.transform.localRotation = Quaternion.identity;
            transferBox.layer = 17;
            transferBox.AddComponent<BoxCollider>();
            transferBox.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
            // transfer point
            transferPoint = new GameObject("Transfer Point");
            transferPoint.transform.parent = conveyorBeltStraight.transform;
            transferPoint.transform.localPosition = new Vector3(0, 0, 1);
            transferPoint.transform.localRotation = Quaternion.identity;
            transferBox.AddComponent<conveyor>();
            transferPoint.AddComponent<TransferPoint>();

            conveyorBeltLeft = GameObject.Instantiate(Resources.Load<GameObject>("furniture/woodsquaretable/WoodSquareTable"), new Vector3(0, 0, 0), Quaternion.identity, null);
            conveyorBeltLeft.name = "ConveyorLeft_Built";
            conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(4).localPosition = new Vector3(0, 0, 0);
            conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(4).localRotation = Quaternion.identity;
            conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("conv_left");
            conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("Conveyor");
            GameObject.Destroy(conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<BoxCollider>());
            collider = conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.AddComponent<MeshCollider>();
            collider.convex = true;
            collider.sharedMesh = AssetLoading.GetEmbeddedMesh("conv_left");
            GameObject.Destroy(conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
            GameObject.Destroy(conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
            GameObject.Destroy(conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(2).gameObject);
            GameObject.Destroy(conveyorBeltLeft.transform.GetChild(0).GetChild(0).GetChild(3).gameObject);
            conveyorBeltLeft.transform.position = new Vector3(0, -10000, 0);
            GameObject.Destroy(conveyorBeltLeft.GetComponent<StorageEntityInteractable>());
            GameObject.Instantiate(autoGrowTent.transform.GetChild(15).gameObject, new Vector3(0, 0, 0), Quaternion.identity, conveyorBeltLeft.transform);
            conveyorBeltLeft.transform.GetChild(9).localPosition = new Vector3(0f, -0.25f, 0.7f);
            conveyorBeltLeft.transform.GetChild(9).localRotation = Quaternion.Euler(90, 270, 0);
            conveyorBeltLeft.transform.GetChild(9).gameObject.SetActive(true);
            conveyorBeltLeft.transform.GetChild(0).name = "Conveyor";
            conveyorBeltLeft.transform.GetChild(0).GetChild(0).name = "conveyor";
            transferBox = new GameObject("Transfer Box");
            transferBox.transform.parent = conveyorBeltLeft.transform;
            transferBox.transform.localPosition = Vector3.zero;
            transferBox.transform.localRotation = Quaternion.identity;
            transferBox.layer = 17;
            transferBox.AddComponent<BoxCollider>();
            transferBox.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
            // transfer point
            transferPoint = new GameObject("Transfer Point");
            transferPoint.transform.parent = conveyorBeltLeft.transform;
            transferPoint.transform.localPosition = new Vector3(-1, 0, 0);
            transferPoint.transform.localRotation = Quaternion.identity;
            transferBox.AddComponent<conveyor>();
            transferPoint.AddComponent<TransferPoint>();

            conveyorBeltRight = GameObject.Instantiate(Resources.Load<GameObject>("furniture/woodsquaretable/WoodSquareTable"), new Vector3(0, 0, 0), Quaternion.identity, null);
            conveyorBeltRight.name = "ConveyorRight_Built";
            conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(4).localPosition = new Vector3(0, 0, 0);
            conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(4).localRotation = Quaternion.identity;
            conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<MeshFilter>().mesh = AssetLoading.GetEmbeddedMesh("conv_right");
            conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<MeshRenderer>().material = AssetLoading.GetCustomTexture("Conveyor");
            GameObject.Destroy(conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<BoxCollider>());
            collider = conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.AddComponent<MeshCollider>();
            collider.convex = true;
            collider.sharedMesh = AssetLoading.GetEmbeddedMesh("conv_right");
            GameObject.Destroy(conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
            GameObject.Destroy(conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
            GameObject.Destroy(conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(2).gameObject);
            GameObject.Destroy(conveyorBeltRight.transform.GetChild(0).GetChild(0).GetChild(3).gameObject);
            conveyorBeltRight.transform.position = new Vector3(0, -10000, 0);
            GameObject.Destroy(conveyorBeltRight.GetComponent<StorageEntityInteractable>());
            GameObject.Instantiate(autoGrowTent.transform.GetChild(15).gameObject, new Vector3(0, 0, 0), Quaternion.identity, conveyorBeltRight.transform);
            conveyorBeltRight.transform.GetChild(9).localPosition = new Vector3(0f, -0.25f, 0.7f);
            conveyorBeltRight.transform.GetChild(9).localRotation = Quaternion.Euler(90, 270, 0);
            conveyorBeltRight.transform.GetChild(9).gameObject.SetActive(true);
            conveyorBeltRight.transform.GetChild(0).name = "Conveyor";
            conveyorBeltRight.transform.GetChild(0).GetChild(0).name = "conveyor";
            transferBox = new GameObject("Transfer Box");
            transferBox.transform.parent = conveyorBeltRight.transform;
            transferBox.transform.localPosition = Vector3.zero;
            transferBox.transform.localRotation = Quaternion.identity;
            transferBox.layer = 17;
            transferBox.AddComponent<BoxCollider>();
            transferBox.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
            // transfer point
            transferPoint = new GameObject("Transfer Point");
            transferPoint.transform.parent = conveyorBeltRight.transform;
            transferPoint.transform.localPosition = new Vector3(1, 0, 0);
            transferPoint.transform.localRotation = Quaternion.identity;
            transferBox.AddComponent<conveyor>();
            transferPoint.AddComponent<TransferPoint>();

            ShopListing listing = CustomItems.AddCustomItem
            (
                name: "Automatic Grow Tent",
                id: "auto_growtent",
                description: "Outputs Buds Once Fully Grown Automaticly",
                category: Il2CppScheduleOne.ItemFramework.EItemCategory.Equipment,
                stackLimit: 10,
                basePurchasePrice: 500,
                resellMultiplier: 0.6f,
                legalStatus: Il2CppScheduleOne.ItemFramework.ELegalStatus.Legal,
                textureName: "GrowTent_Built_Icon",
                buildItem: autoGrowTent.GetComponent<Pot>()
            );

            foreach (var shopInterface in UnityEngine.Object.FindObjectsOfType<ShopInterface>())
            {
                shopInterface.CreateListingUI(listing);
            }

            listing = CustomItems.AddCustomItem
            (
                name: "Belt",
                id: "conv_straight",
                description: "Moves Items Across The Belt",
                category: Il2CppScheduleOne.ItemFramework.EItemCategory.Equipment,
                stackLimit: 64,
                basePurchasePrice: 125,
                resellMultiplier: 0.8f,
                legalStatus: Il2CppScheduleOne.ItemFramework.ELegalStatus.Legal,
                textureName: "GrowTent_Built_Icon",
                buildItem: conveyorBeltStraight.GetComponent<PlaceableStorageEntity>()
            );

            foreach (var shopInterface in UnityEngine.Object.FindObjectsOfType<ShopInterface>())
            {
                shopInterface.CreateListingUI(listing);
            }

            listing = CustomItems.AddCustomItem
            (
                name: "Belt Left",
                id: "conv_left",
                description: "Moves Items Left Across The Belt",
                category: Il2CppScheduleOne.ItemFramework.EItemCategory.Equipment,
                stackLimit: 64,
                basePurchasePrice: 125,
                resellMultiplier: 0.8f,
                legalStatus: Il2CppScheduleOne.ItemFramework.ELegalStatus.Legal,
                textureName: "GrowTent_Built_Icon",
                buildItem: conveyorBeltLeft.GetComponent<PlaceableStorageEntity>()
            );

            foreach (var shopInterface in UnityEngine.Object.FindObjectsOfType<ShopInterface>())
            {
                shopInterface.CreateListingUI(listing);
            }

            listing = CustomItems.AddCustomItem
            (
                name: "Belt Right",
                id: "conv_right",
                description: "Moves Items Right Across The Belt",
                category: Il2CppScheduleOne.ItemFramework.EItemCategory.Equipment,
                stackLimit: 64,
                basePurchasePrice: 125,
                resellMultiplier: 0.8f,
                legalStatus: Il2CppScheduleOne.ItemFramework.ELegalStatus.Legal,
                textureName: "GrowTent_Built_Icon",
                buildItem: conveyorBeltRight.GetComponent<PlaceableStorageEntity>()
            );

            foreach (var shopInterface in UnityEngine.Object.FindObjectsOfType<ShopInterface>())
            {
                shopInterface.CreateListingUI(listing);
            }

            listing = CustomItems.AddCustomItem
            (
                name: "Auto Mixer",
                id: "auto_mixer",
                description: "Works With A Belt To Automaticly Mix Product",
                category: Il2CppScheduleOne.ItemFramework.EItemCategory.Equipment,
                stackLimit: 10,
                basePurchasePrice: 300,
                resellMultiplier: 0.75f,
                legalStatus: Il2CppScheduleOne.ItemFramework.ELegalStatus.Legal,
                textureName: "GrowTent_Built_Icon",
                buildItem: autoMixingStation.GetComponent<MixingStation>()
            );

            foreach (var shopInterface in UnityEngine.Object.FindObjectsOfType<ShopInterface>())
            {
                shopInterface.CreateListingUI(listing);
            }

            MelonCoroutines.Start(Main_Func());
        }
    }

    public IEnumerator Main_Func()
    {
        yield return new WaitForSeconds(0.5f);
        autoGrowTent.SetActive(true);
        conveyorBeltStraight.SetActive(true);
        conveyorBeltLeft.SetActive(true);
        conveyorBeltRight.SetActive(true);
        autoMixingStation.SetActive(true);
    }
}
