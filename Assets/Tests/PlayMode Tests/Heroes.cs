using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class Heroes
{
    GameObject player;

    string playerString = "Assets/Prefabs/Hero.prefab";

    [SetUp]
    public void SetUp()
    {
        player = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(playerString));
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(player);
    }

    [Test]
    public void HeroHasPlayerInput()
    {
        Assert.IsNotNull(player.GetComponent<PlayerInput>());
    }

    [Test]
    public void HeroHasRigidbody()
    {
        Assert.IsNotNull(player.GetComponent<Rigidbody>());
    }

    [Test]
    public void HeroHasFreezeRotation()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Assert.IsTrue(rb.freezeRotation);
    }

    [Test]
    public void HeroHasHeroBehaviourScript()
    {
        Assert.IsNotNull(player.GetComponent<HeroBehaviour3D>());
    }

    [Test]
    public void HeroHasNavMeshAgent()
    {
        Assert.IsNotNull(player.GetComponent<NavMeshAgent>());
    }

    [Test]
    [TestCase("Body")]
    [TestCase("Gun")]
    [TestCase("SphereSpawn")]
    public void HeroHasChild(string stringChild)
    {
        var obj = player.transform.Find(stringChild);
        Assert.IsNotNull(obj);
    }

    [Test]
    public void HeroSphereSpawnHasSphereCollider()
    {
        var obj = player.transform.Find("SphereSpawn");
        Assert.IsNotNull(obj.GetComponent<SphereCollider>());
    }

    [Test]
    public void HeroBodyHasSpriteRenderer()
    {
        var obj = player.transform.Find("Body");
        Assert.IsNotNull(obj.GetComponent<SpriteRenderer>());
    }

    [Test]
    public void HeroBodyHasAntiBodyRotate2DScript()
    {
        var obj = player.transform.Find("Body");
        Assert.IsNotNull(obj.GetComponent<AntiBodyRotate2DScript>());
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HeroesWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

}
