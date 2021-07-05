# Pottery

This project is similar to creating shapes in lathe.
Shape (pottery) is simply array of radiuses (rings).
- Each ring is placed on top of another by Y axis (vertically).
- Each ring has radius horizontally (X and Z axis).
- Then according to this values, mesh is constructed.
- Mesh generation has edges count modifier, which sets count of vertices for each ring.
- Each point in ring is calculated using simple trigonometric formula where:
<br>**x** = cos(π / faces * index)
<br>**y** = sin(π / faces * index)

For more information check `PotteryGenerator.cs.cs` file

Source files contians XML documentation if you want to get better understanding of process.

### Using
To use this project as Unity Package, get git link of this repository from Clone section and add `#upm` postfix in Package Manager window.
https://github.com/vmp1r3/Pottery.git#upm

### Examples
<p>
<img src="https://user-images.githubusercontent.com/14846427/124443152-1bff3b80-dd86-11eb-95b3-d9c81fc71a26.png" height=300>
<img src="https://user-images.githubusercontent.com/14846427/124443204-27526700-dd86-11eb-920a-2bc6ed9f2142.png" height=300>
<br><img src="https://user-images.githubusercontent.com/14846427/124443437-59fc5f80-dd86-11eb-9ffb-8ed52747843b.png" height=300>
<img src="https://user-images.githubusercontent.com/14846427/124443493-65e82180-dd86-11eb-8a85-478b21055ca3.gif" height=300>
</p>

### License
Licensed under GPLv3 license or under special license.
See the LICENSE file in the project root for further information.
