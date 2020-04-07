# ThinkGeo Offline Maps Viewer & Extractor

### Description

This sample shows you how to display the ThinkGeo Cloud offline maps in a desktop application. All the data it is using is in a 16.3M mbtiles database along with the sample. Check out [this sample](https://github.com/ThinkGeo/ThinkGeoCloudVectorMapsSample-ForWpf12) to see how to get the ThinkGeo Cloud online map in a desktop application. 

This sample also allows you to create a new smaller subsets data from an existing MBTiles database by simply specify the extent of the new area on the map.  

This sample happens to use .NET Core, you can also create a .NET Framework application accomplishing the same thing. [Here](https://github.com/ThinkGeo/ThinkGeoGoogleMapsOverlaySample-ForWpf12) is a sample using ThinkGeo 12 on .NET Framework 4.6.1.  

*.MBTile format is supported in both Desktop and Mobile.

![Screenshot](https://github.com/ThinkGeo/ThinkGeoOfflineMapsViewerAndExtractor-ForWpf12/blob/master/Screenshot.gif)

### About the Code
```csharp
string mbtilesPathFilename = "Data/NewYorkCity.mbtiles";
string defaultJsonFilePath = "Data/thinkgeo-world-streets-light.json";

this.wpfMap.MapUnit = GeographyUnit.Meter;
ThinkGeoMBTilesLayer thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesLayer(mbtilesPathFilename, new Uri(defaultJsonFilePath, UriKind.Relative));
            
layerOverlay = new LayerOverlay();
layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);

this.wpfMap.Overlays.Add(layerOverlay);
```
### Getting Help

[Map Suite UI Control for WPF Wiki Resources](https://wiki.thinkgeo.com/wiki/thinkgeo_desktop_for_wpf)

[Map Suite UI Control for WPF Product Description](https://thinkgeo.com/gis-ui-desktop#platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
