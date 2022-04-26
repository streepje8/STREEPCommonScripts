# Singleton
Just a generic singleton class you can extend.

## How to use
When making a singleton script, add these two things
```
	public class Name : Singleton<Name> 
	{
	
	}
```
and
```
	public class Name : Singleton<Name> 
	{
		void Awake() 
		{
			Instance = this;
		}
	}
```

This code can be very powerful if you also add a
```
DontDestroyOnLoad(gameObject);
```
at the beginning of the script.