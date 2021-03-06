# Poolboy

Poolboy is a simple object pooling solution for unity.
My goal was to have the easiest way to get the pooling functionality for my games without thinking always about the same thing over and over again. So here it is the Poolboy :)

## What you get?
You get a pool to reduce lags during runtime if you want to instantiate `GameObject`'s.

## Example?

Following examples shows a simple shooting script.

I just drag and drop the `Bullet` prefab to the reference slot in the `PlayerShooting` script. And press space.

<img src="https://i.imgur.com/doB3gUX.gif" alt="Add Prefab" width="500" height="250">

```cs
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField]
        private Bullet bulletPrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                bulletPrefab.GetPooledInstance();
            }
        }
    }
    
    public class Bullet : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            
            gameObject.SetActive(false);
        }
    }

```

Everytime space get pressed you get a `Bullet` from the pool. 
The `Bullet` will be disabled after 2 seconds so its ready again for the pool.

<img src="https://i.imgur.com/njborfz.gif" alt="Shooting Bullets" width="300" height="400">

## How it works?

If you call `GetPooledInstance()` you get a `GameObject` out of a list of disabled GameObjects depends on the prefabs instance id. If no object is available a new one will be added.

The typical reseting part takes place in unity's `OnDisable()` method, you dont have to call reset or something to get a valid state of the object. You just have to clean it up in `OnDisable()`. Easy right? :)

Additionally every pooled object gets a `PoolEntity` Component, the only two reasons for this are to reparenting a pooled object to the pool if you changed his parent after calling `GetPooledInstance()` and to remove the object from the pool if you really want to destroy it.

The last important part is that you have to call `Pool.Initialize()`; Somewhere to populate the pool at the start, that reduces lag spikes during the gameplay.

### PoolSettings

To populate the pool at the start of your game with some preconfigured prefabs you can use the `PoolSettings` to do so. You can create them with [Tools->Pool->Create Pool Settings] If you don't have `PoolSettings` you get a Warning, but the pool does work anyway.

## Conclusion

The pool is really solid if you take care that `OnDisable()` is your reset method.

The bad thing about unity and pooling is that you can't get disabled instances of GameObjects from a prefab, except you save a disabled prefab.

Anyway I had no problems with my `OnEnable()` and `OnDisable()` strategy but you should keep in mind that if you instantiate the pool the first time every object calls `OnEnable()` and `OnDisable()`.

## Tips

If you use `ParticleSystems` disable them after playing, otherwise you don't have the pooling advantage.

## Made with
<a href="https://wildwoods.itch.io/wildwoods">
<img src="https://img.itch.zone/aW1nLzIyNzAzMjUucG5n/315x250%23c/j71zvH.png" alt="Wild Woods" width="200" height="150">
</a>
<a href="https://ruhken.itch.io/follovers">
<img src="https://img.itch.zone/aW1nLzE4ODY3NzMucG5n/315x250%23c/Xx1SH9.png" alt="Follovers Game" width="200" height="150">
</a>
<a href="https://ruhken.itch.io/tangle-toys">
<img src="https://img.itch.zone/aW1nLzIxNjczMzYucG5n/315x250%23c/YmTuQn.png" alt="Tangle Toys" width="200" height="150">
</a>
<a href="https://1-jar.itch.io/cuddle-waddle">
<img src="https://img.itch.zone/aW1nLzMxOTk1MTgucG5n/315x250%23c/8bgbl7.png" alt="Cuddle Waddle" width="200" height="150">
</a>




