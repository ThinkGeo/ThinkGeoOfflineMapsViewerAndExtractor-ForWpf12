# MBTiles Extractor Sample for WPF

### Description

The MBTiles Extractor allows you to create new smaller subsets from the MBTiles database. You simply specify the bounding box by tracking a rectangle shape on the map for the new area, then it will create a new SQLite database for that regions. 

*.MBTile format can be supported in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

Please refer to [Wiki](https://wiki.thinkgeo.com/wiki/thinkgeo_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/MBTilesExtractorSample-ForWpf.NETCore/blob/master/Screenshot.gif)

### About the Code
The sample can extract MBTiles from source database to target database. One thing needs to be aware of: there are three tables ("map","images","metadata") need to be copied from source database to target database.
```csharp
ThinkGeoMBTilesLayer.CreateDatabase(targetFilePath);
var targetDBConnection = new SqliteConnection($"Data Source={targetFilePath}");
var targetMap = new Map(targetDBConnection);
var targetImages = new Images(targetDBConnection);
var targetMetadata = new Metadata(targetDBConnection);

var sourceDBConnection = new SqliteConnection("Data Source=Data/tiles_Frisco.mbtiles");
var sourceMap = new Map(sourceDBConnection);
var sourceImages = new Images(sourceDBConnection);
var sourceMetadata = new Metadata(sourceDBConnection);

sourceMetadata.NextPage();
foreach (MetadataEntry entry in sourceMetadata.Entries)
{
    if (entry.Name.Equals("center"))
    {
        PointShape centerPoint = projection.ConvertToExternalProjection(bbox).GetCenterPoint();
        entry.Value = $"{centerPoint.X},{centerPoint.Y},{maxZoom}";
    }
}
targetMetadata.Insert(sourceMetadata.Entries);

int recordLimit = 1000;
foreach (var tileRange in tileRanges)
{
    long offset = 0;
    bool isEnd = false;
    while (!isEnd)
    {
        string querySql = $"SELECT * FROM {sourceMap.TableName} WHERE " + ConvetToSqlString(tileRange) 
                            + $" LIMIT {offset},{recordLimit}";
        var entries = sourceMap.Query(querySql);
        targetMap.Insert(entries);

        if (entries.Count < recordLimit)
            isEnd = true;

        querySql = $"SELECT images.tile_data as tile_data, images.tile_id as tile_id FROM {sourceImages.TableName} "
                    + $WHERE images.tile_id IN ( SELECT {Map.TileIdColumnName} FROM {sourceMap.TableName} WHERE "
                    + ConvetToSqlString(tileRange) + $" LIMIT {offset},{recordLimit} )";
        entries = sourceImages.Query(querySql);
        targetImages.Insert(entries);

        offset = offset + recordLimit;
    }
}
```
### Getting Help

[Map Suite UI Control for WPF Wiki Resources](https://wiki.thinkgeo.com/wiki/thinkgeo_desktop_for_wpf)

[Map Suite UI Control for WPF Product Description](https://thinkgeo.com/gis-ui-desktop#platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
