using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic; // Added for List<>
using System.Globalization; // Added for CultureInfo
using UnityEngine;
using Il2CppScheduleOne;
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.Storage;
using Il2CppScheduleOne.Product;
using Il2CppScheduleOne.Levelling;
using Il2CppScheduleOne.UI.Shop;
using AssetLoader;
using Il2CppScheduleOne.Equipping;
using Il2CppScheduleOne.EntityFramework;
using MelonLoader;

namespace CustomItemCreator
{
    public static class CustomItems
    {
        public static ShopListing AddCustomItem(string name, string id, string description, EItemCategory category, int stackLimit, float basePurchasePrice, float resellMultiplier, ELegalStatus legalStatus, string textureName, BuildableItem buildItem)
        {
            GameObject customItemObject = new GameObject(name + "_Stored");
            GameObject buildItemObject = new GameObject(name + "_Buildable");
            StoredItem storedItem = customItemObject.AddComponent<StoredItem>();
            Equippable_BuildableItem equippableBuildItem = buildItemObject.AddComponent<Equippable_BuildableItem>();

            BuildableItemDefinition customItemDefinition = new BuildableItemDefinition();
            ShopListing shopListing = new ShopListing();
            ItemInstance customItem = new ItemInstance();
            FullRank rankNeeded = new FullRank(ERank.Hoodlum, 5);

            customItem.definition = customItemDefinition;
            customItemDefinition.BuiltItem = buildItem;
            customItemDefinition.Equippable = equippableBuildItem;

            customItemDefinition.Name = name;
            customItemDefinition.ID = id;
            customItemDefinition.Description = description;
            customItemDefinition.Category = category;
            customItemDefinition.StackLimit = stackLimit;
            customItemDefinition.BasePurchasePrice = basePurchasePrice;
            customItemDefinition.RequiredRank = rankNeeded;
            customItemDefinition.RequiresLevelToPurchase = false;
            customItemDefinition.ResellMultiplier = resellMultiplier;
            customItemDefinition.legalStatus = legalStatus;
            customItemDefinition.StoredItem = storedItem;

            storedItem.canBePickedUp = true;
            storedItem._canBePickedUp_k__BackingField = true;

            shopListing.Item = customItemDefinition;
            Sprite iconSprite = AssetLoading.GetCustomSpriteEmbedded("Factory_Test.Textures." + textureName + ".png");
            customItemDefinition.Icon = iconSprite;

            Registry.instance.AddToRegistry(customItemDefinition);
            Registry.Instance.AddToItemDictionary(new Registry.ItemRegister { ID = customItemDefinition.ID, Definition = customItemDefinition });

            return shopListing;
        }
    }
}

namespace AssetLoader
{
    public static class AssetLoading
    {
        public static Mesh GetEmbeddedMesh(string resourceName)
        {
            try
            {
                MelonLogger.Msg($"Attempting to load embedded mesh: {"Factory_Test.Models."+resourceName+".obj"}");
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("Factory_Test.Models." + resourceName + ".obj"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string objData = reader.ReadToEnd();
                    MelonLogger.Msg($"OBJ Data Length: {objData.Length}");
                    var (vertices, uvs, faces) = ParseOBJ(objData);

                    MelonLogger.Msg($"Parsed {vertices.Length} vertices, {uvs.Length} UVs, {faces.Length} triangles");

                    Mesh mesh = new Mesh
                    {
                        vertices = vertices,
                        uv = uvs,
                        triangles = faces
                    };
                    mesh.RecalculateNormals();
                    mesh.RecalculateBounds();
                    return mesh;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading embedded mesh: {ex.Message}");
                return null;
            }
        }

        public static Sprite GetCustomSpriteEmbedded(string resourceName)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        byte[] imageBytes = new byte[stream.Length];
                        stream.Read(imageBytes, 0, imageBytes.Length);
                        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                        if (texture.LoadImage(imageBytes))
                        {
                            Sprite sprite = Sprite.Create(
                                texture,
                                new Rect(0, 0, texture.width, texture.height),
                                new Vector2(0.5f, 0.5f),
                                100.0f
                            );
                            return sprite;
                        }
                        else
                        {
                            Debug.LogError("Failed to load texture from embedded resource: " + resourceName);
                            return null;
                        }
                    }
                    else
                    {
                        Debug.LogError("Embedded resource not found: " + resourceName);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading sprite from embedded resource: " + ex.Message);
                return null;
            }
        }

        public static Material GetCustomTexture(string resourcePath)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("Factory_Test.Textures." + resourcePath + ".png"))
                {
                    if (stream != null)
                    {
                        byte[] imageBytes = new byte[stream.Length];
                        stream.Read(imageBytes, 0, imageBytes.Length);
                        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                        if (texture.LoadImage(imageBytes))
                        {
                            texture.name = "CustomTexture";
                            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
                            Material material;
                            if (shader != null)
                            {
                                material = new Material(shader);
                                material.SetTexture("_BaseMap", texture);
                                material.SetColor("_BaseColor", Color.white);
                            }
                            else
                            {
                                Debug.LogWarning("URP/Lit shader not found, falling back to Standard");
                                shader = Shader.Find("Standard");
                                material = new Material(shader);
                                material.SetTexture("_MainTex", texture);
                                material.SetColor("_Color", Color.white);
                            }
                            return material;
                        }
                        else
                        {
                            Debug.LogError("Failed to load texture from embedded resource: " + "Factory_Test.Textures." + resourcePath + ".png");
                            return null;
                        }
                    }
                    else
                    {
                        Debug.LogError("Embedded resource not found: " + "Factory_Test.Textures." + resourcePath + ".png");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading custom texture from embedded resource: " + ex.Message);
                return null;
            }
        }

        private static (Vector3[], Vector2[], int[]) ParseOBJ(string obj)
        {
            var vertices = new List<Vector3>();
            var uvs = new List<Vector2>();
            var faces = new List<List<(int vertexIndex, int uvIndex)>>();

            foreach (var line in obj.Split('\n'))
            {
                var elements = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length == 0) continue;

                switch (elements[0])
                {
                    case "v":
                        var vertex = new Vector3(
                            float.Parse(elements[1], CultureInfo.InvariantCulture),
                            float.Parse(elements[2], CultureInfo.InvariantCulture),
                            -float.Parse(elements[3], CultureInfo.InvariantCulture));
                        vertices.Add(vertex);
                        MelonLogger.Msg($"Added Vertex: {vertex}");
                        break;
                    case "vt":
                        var uv = new Vector2(
                            float.Parse(elements[1], CultureInfo.InvariantCulture),
                            float.Parse(elements[2], CultureInfo.InvariantCulture));
                        uvs.Add(uv);
                        MelonLogger.Msg($"Added UV: {uv}");
                        break;
                    case "f":
                        var face = new List<(int, int)>();
                        for (int i = 1; i < elements.Length; i++)
                        {
                            var indices = elements[i].Split('/');
                            int vertexIndex = int.Parse(indices[0]) - 1;
                            int uvIndex = indices.Length > 1 && !string.IsNullOrEmpty(indices[1]) ? int.Parse(indices[1]) - 1 : -1;

                            face.Add((vertexIndex, uvIndex));
                        }
                        faces.Add(face);
                        MelonLogger.Msg($"Added Face: {string.Join(", ", face)}");
                        break;
                }
            }

            var uniqueVertices = new Dictionary<string, int>();
            var finalVertices = new List<Vector3>();
            var finalUVs = new List<Vector2>();
            var triangles = new List<int>();

            foreach (var face in faces)
            {
                if (face.Count < 3) continue;
                for (int i = 0; i < face.Count - 2; i++)
                {
                    var v0 = face[0];
                    var v1 = face[i + 1];
                    var v2 = face[i + 2];

                    string key0 = v0.vertexIndex + "_" + v0.uvIndex;
                    string key1 = v1.vertexIndex + "_" + v1.uvIndex;
                    string key2 = v2.vertexIndex + "_" + v2.uvIndex;

                    if (!uniqueVertices.ContainsKey(key0))
                    {
                        uniqueVertices[key0] = finalVertices.Count;
                        finalVertices.Add(vertices[v0.vertexIndex]);
                        finalUVs.Add(v0.uvIndex == -1 ? Vector2.zero : uvs[v0.uvIndex]);
                        MelonLogger.Msg($"Added Unique Vertex: {vertices[v0.vertexIndex]}, UV: {finalUVs[finalUVs.Count - 1]}");
                    }
                    if (!uniqueVertices.ContainsKey(key1))
                    {
                        uniqueVertices[key1] = finalVertices.Count;
                        finalVertices.Add(vertices[v1.vertexIndex]);
                        finalUVs.Add(v1.uvIndex == -1 ? Vector2.zero : uvs[v1.uvIndex]);
                        MelonLogger.Msg($"Added Unique Vertex: {vertices[v1.vertexIndex]}, UV: {finalUVs[finalUVs.Count - 1]}");
                    }
                    if (!uniqueVertices.ContainsKey(key2))
                    {
                        uniqueVertices[key2] = finalVertices.Count;
                        finalVertices.Add(vertices[v2.vertexIndex]);
                        finalUVs.Add(v2.uvIndex == -1 ? Vector2.zero : uvs[v2.uvIndex]);
                        MelonLogger.Msg($"Added Unique Vertex: {vertices[v2.vertexIndex]}, UV: {finalUVs[finalUVs.Count - 1]}");
                    }

                    triangles.Add(uniqueVertices[key0]);
                    triangles.Add(uniqueVertices[key2]);
                    triangles.Add(uniqueVertices[key1]);
                    MelonLogger.Msg($"Added Triangle: {uniqueVertices[key0]}, {uniqueVertices[key2]}, {uniqueVertices[key1]}");
                }
            }

            return (finalVertices.ToArray(), finalUVs.ToArray(), triangles.ToArray());
        }
    }
}
