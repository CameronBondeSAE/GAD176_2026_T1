using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

public interface IIsCamable
{
	public bool TestIsCamable();
}

[Serializable]
public class TestTriInspector : MonoBehaviour
{
	[SerializeReference]
	public IIsCamable IsCamable;
	
	[SerializeReference]
	public List<IIsCamable> IsCamables;
	

	[Button]
	void DoThing()
    {
        Debug.Log("TestIsCamable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
