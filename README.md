# Pool Attendant

Pool Attendant is a simple object pooling solution for unity.
My goal was to have the easiest way to get the pooling functionality for my game jams and new project without thinking always about the same thing over and over again. So here it is the Pool Attendant :)

## What you get?
You get a pool to reduce lags during runtime if you want to instantiate GameObjects.

## How to use?

Pooled GameObject from prefab
```cs
public class GameObjectExample
{
   [SerializedField] private GameObject prefab;

   private void InstantiateSomething()
   {
       GameObject obj = prefab.GetPooledInstance();
   }
}

```

Pooled ParticleSystem from prefab
```cs
public class ExampleClass
{
   [SerializedField] private ParticleSystem systemPrefab;

   private void InstantiateSomething()
   {
       ParticleSystem obj = system.GetPooledInstance<ParticleSystem>();
   }
}

```

## How it works

If you call GetPooledInstance() you get a GameObject out of a list of disabled GameObjects depends on the prefabs instance id. If no object is available a new one will be added. 

The typical reseting part takes place in unity's OnDisable() Method so you dont have to call reset or something to the a valid state of the object. You just have to clean it up in OnDisable(). Easy right? :)

Additionally every pooled object gets a PoolEntity Component, the only reason for this is to reparenting a pooled object to the pool if you changed his parent after calling GetPooledInstance();

The last important part is that you have to call Pool.Initialize(); Somewhere to populate the pool at the start, that reduces lag spikes during the gameplay.

### PoolSettings

To populate the Pool at the start of your game with some preconfigured prefabs you can use the PoolSettings to do so. You can create them with [Tools->Pool->Create Pool Settings] If you dont have Pool Settings you get a Warning, but the pool does work anyway.

## Conclusion

The pool is really solid if you take care that OnDisable() is your reset method.
The bad thing about unity and pooling is always that you can't get disabled instances of GameObjects from a prefab, except you disable the prefab in scene view and save the result, so that your prefab assets are disabled.

Anyway i had no problems with my OnEnable and OnDisable strategy but you should keep in mind that if you instantiate the pool the first time every objects calls OnEnable and OnDisable.
