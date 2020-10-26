# Very small library to deal with User Agents

This library is not interested in telling you what type of device this User Agent belongs to.

It's simply looking for name/version groups to derive "identities".

In the event that no identity can be discerned it will return an identity of `garbage` with a Major Version of `1`

This is best illustrated with an example : 

## Usage

```csharp
using com.janoserdelyi.UAB;
...
Browser b = Browser.Parse ("Mozilla/5.0 (Windows NT 10.0; WOW64; rv:77.0) Gecko/20100101 Firefox/77.0");
```

This will present multiple identities - 
```
Name - firefox, Major Version - 77, Minor Version - 0
Name - mozilla, Major Version - 5, Minor Version - 0
Name - rv, Major Version - 77, Minor Version - 0
```

Typical usage to filter out older versions of Chrome might look like this : 

```csharp
Browser b = Browser.Parse ("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36");

if (b.Is("chrome") && b.Get("chrome").MajorVersion < 60) {
	Console.WriteLine("whoa now gramps! time to update!");
}
```
