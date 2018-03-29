## Structure Wrapper
Structure Wrapper is a C# Xamarin Wrapper for Occipital's Structure Sensor SDK.

### Disclaimer
This binding was developed for SDK __0.7.1__ is still very much a work in progress. I have been able to get a sample functioning (sans camera) but have not had the time to finish porting their sample scanner.

### Getting Started
1. Clone the repository
2. Download the latest Structure SDK [here](https://developer.structure.io/portal)
3. Unzip the .zip file, and navigate to ../[unzipped_folder]/Structure SDK/Frameworks/Structure.framework/Versions/A/
4. Rename _Structure_ to _Structure.a_
5. Copy _Structure.a_ to the base directory of your cloned repo
6. Open _StructureWrapper.sln_ and build!

### Things that aren't working
I haven't been able to get the key strings working yet. In the meantime, you can use the key name as a string by removing the 'k' from the front of the key name and the "Key" from the end of the key name (_kSTDepthToRgbaStrategyKey_ becomes _"STDepthToRgbaStrategy"_).

Here's an example from my test app:
```
var options = new NSDictionary(
    "STStreamConfig", (NSNumber)(int)STStreamConfig.STStreamConfigDepth320x240, 
    "STFrameSyncConfig", (NSNumber)(int)STFrameSyncConfig.STFrameSyncOff, 
    "STHoleFilterEnabled", NSObject.FromObject(true)
);
```

### Contributing
Because this project is still very much a work in progress, I haven't been able to test the functionality of all methods and some may not function/function as expected/contain all methods/return the proper type. If you come across some _unexpected_ functionality, please raise an issue or - better yet - tackle it yourself in a Pull Request.
