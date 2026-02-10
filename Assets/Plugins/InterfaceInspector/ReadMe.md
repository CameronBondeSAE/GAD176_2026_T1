# Serialize Interface Generator

This repository contains the source code for generating the Unity Source Generator DLL which allows us to use the [SerializeInterface] attribute.

## [SerializeInterface] Attribute

The Serialize Interface Attribute allows us to serialize interfaces in Unity the same way we would use [SerializeField]

```csharp

public partial class InterfaceConsumer : MonoBehaviour
{
    [SerializeInterface] private IFoo foo;

    private void Start()
    {
        foo.Test();
    }
}

```

Where the instance of 'IFoo' can be derived from a MonoBehaviour or ScriptableObject. For example, the following would be valid classes:

```csharp
public interface IFoo
{
    public void Test();
}

public class Foo1 : MonoBehaviour, IFoo
{
    public void Test() => Debug.Log("Hello from Foo1");
}

public class Foo2 : MonoBehaviour, IFoo
{
    public void Test() => Debug.Log("Hello from Foo2");
}

public class Foo3 : ScriptableObject, IFoo
{
    public void Test() => Debug.Log("Hello from Foo3 (I'm a Scriptable Object btw)");
}
```

This attribute allows us to follow the SOLID principles in Unity, by allowing us to more widely use Interfaces alongside traditional unity workflows.

This is a powerful feature as it allows us to serialize in ways not previously possible, for example, serializing a list of generic classes.

```csharp
public partial class GenericInterfaceConsumer : MonoBehaviour
{
    [SerializeInterface]
    public readonly List<IGeneric<int>> MyGenericClasses = new();
}

public interface IGeneric<T>
{
    public T Value {get;set;}
}

public class Example1 : MonoBehaviour, IGeneric<int>
{
    public int Value {get;set;}
}
```

For more use cases, it's worth checking out the Samples at Assets->Samples->SerializeInterface Sample.unity

## How to use this repository

Source Generators are created using a .net Standard 2.0 Class Library Project which is then built to a DLL. It's this DLL which is imported into Unity.

This Project can be found in the 'SerializeInterfaceGenerator' Directory.

1. Load the project ```\serialize-interface-generator\SerializeInterfaceGenerator\SerializeInterfaceGenerator.sln```
2. Ensure nuget packages ```Microsoft.CodeAnalysis.CSharp 4.1.0``` and ```Microsoft.CodeAnalysis.Common 4.1.0``` 
    - IMPORTANT: The version MUST be 4.1.0 and not the most recent version.
3. Build the project (The DLL file should automatically be moved into the Unity Assets Folder)

## How it Works

The Source Generator searches for any files containting the ```[SerializeInterface]``` attribute and extends the class to support serialiation on interfaces. For example:

```csharp 
public partial class MyClass : MonoBehaviour
{
    [SerializeInterface]
    public IFoo foo;
}
```

Will source generate the partial class:

```csharp
public partial class MyClass : MonoBehaviour, ISerializationCallbackReceiver
{
    void OnBeforeSerialize()
    {

    }

    void OnAfterDeserialize()
    {
        
    }
}
