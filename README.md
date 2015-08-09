# UK_Spatial_Search_Data_Cleaner

This software was implemented to clean and organise spatial data for the United Kingdom. Has been developed to read CSV files available from the ordinance survey and other sources and then build a SQL Server database using Geographical types that can be used for geo-spatial search within the UK.

The data that has been processed can be found here: 

https://www.ordnancesurvey.co.uk/opendatadownload/products.html
http://download.geofabrik.de/europe/great-britain.html

I have published this software here because it contains:

- An algorithm that maps the convex hull of a set of points. 
- Conversion between easting northings and latitudes and longtitudes. 
- The migrations to build your own database for this kind of search in SQL server. 
- A couple of classes that you can use in other projects for example a simple CSV reader

I hope this is of some use to someone. Please feel free to message me at matthew.folker@gmail.com if you have any questions. 

If you would like to work on another project with this data, myself and a colleague have one in mind. Again, please feel free to get in contact. 

Useful tools:
- http://sourceforge.net/projects/splitcsv
