# Pottery

Pottery is a runtime lathe 3D shapes generator.

## How it works
A shape is created as an array of rings placed on top of each other along the Y-axis (vertically). All rings have a radius, and they share the same number of edges set in the shape. The mesh is created based on the shape's height, ring radius, and edge count. All vertices are calculated using a simple trigonometric formula where:
- x = cos(π / faces * index)
- y = sin(π / faces * index)
- z = height / ringsCount * ringIndex

<p>
<img src="https://user-images.githubusercontent.com/14846427/124443152-1bff3b80-dd86-11eb-95b3-d9c81fc71a26.png" height=240>
<img src="https://user-images.githubusercontent.com/14846427/124443204-27526700-dd86-11eb-920a-2bc6ed9f2142.png" height=240>
<br><img src="https://user-images.githubusercontent.com/14846427/124443437-59fc5f80-dd86-11eb-9ffb-8ed52747843b.png" height=240>
<img src="https://user-images.githubusercontent.com/14846427/124443493-65e82180-dd86-11eb-8a85-478b21055ca3.gif" height=240>
</p>
