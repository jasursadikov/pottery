# Pottery

Pottery is a runtime lathe 3D shapes generator made on Unity.

<img src="img/demo.gif" height=360>

## How it works
A shape is created as an array of rings placed on top of each other along the Y-axis (vertically). All rings have a radius, and they share the same number of edges set in the shape. The mesh is created based on the shape's height, ring radius, and edge count. All vertices are calculated using a simple trigonometric formula where:
- x = cos(π / faces * index)
- y = sin(π / faces * index)
- z = height / ringsCount * ringIndex
<p>
<img src="img/mesh.gif" height=360>
<img src="img/pot.gif" height=360>
</p>