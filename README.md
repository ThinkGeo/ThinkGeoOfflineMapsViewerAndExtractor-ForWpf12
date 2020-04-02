# MBTiles Extractor Sample for WPF

### Description

This sample shows you how to display the ThinkGeo Cloud Maps (mbtiles file) in an offline desktop application. This sample happens to use .NET Core, you can also create a .NET Framework application accomplishing the same thing. 

This sample also allows you to create a new smaller subsets data from an existing MBTiles database by simply specify the extent of the new area on the map.  

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
