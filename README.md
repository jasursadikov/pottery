<h1 align="center">🍸 Pottery 🍷</h1>

Pottery is runtime 3D shapes generator similar to lathe.  

### How it works
Shape is created which simply array of rings placed on top of each another by Y axis (vertically).  
All rings have radius. Rings share the same amount of edges set in shape.  
Mesh is created by shape height, ring radius and edges count.  
All vertices are calculated using simple trigonometric formula where:  
**x** = cos(π / faces * index)  
**y** = sin(π / faces * index)  
**z** = height / rignsCount * ringIndex

<p>
<img src="https://user-images.githubusercontent.com/14846427/124443152-1bff3b80-dd86-11eb-95b3-d9c81fc71a26.png" height=240>
<img src="https://user-images.githubusercontent.com/14846427/124443204-27526700-dd86-11eb-920a-2bc6ed9f2142.png" height=240>
<br><img src="https://user-images.githubusercontent.com/14846427/124443437-59fc5f80-dd86-11eb-9ffb-8ed52747843b.png" height=240>
<img src="https://user-images.githubusercontent.com/14846427/124443493-65e82180-dd86-11eb-8a85-478b21055ca3.gif" height=240>
</p>