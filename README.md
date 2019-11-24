# Pottery

Pottery is runtime pottery editing engine, developed using C# and Unity

#### Description

Pottery object is array of horizontal circle's radius. Each circle has preset common vertical distance from one to another. Each circle is linked one by one into one solid mesh.

Each dot in one line is simply calculated by:

*x = cos(π / faces * index)
y = sin(π / faces * index)*

For futher information about generation please check `PotteryGenerator.cs` file

Read XML documentation included in source code files, for further information.

#### Example
Result
<p>
<img src="http://plasticblock.xyz/projects/pottery/example.png" height=300>
<img src="http://plasticblock.xyz/projects/pottery/objectsExample.png" height=300>
</p>

Mesh structure
<p>
<img src="http://plasticblock.xyz/projects/pottery/meshStructure.png" height=300>
</p>

Pottery Demo editing example
<p>
<img src="http://plasticblock.xyz/projects/pottery/editingSample.gif" height=300>
</p>

#### Contacts
Website **<a href="http://plasticblock.xyz/">plasticblock.xyz</a>**
Skype **plasticblock**
E-mail **<a href="mailto: contact@plasticblock.xyz">contact@plasticblock.xyz</a>**

#### License
Licensed under GPLv3 license or under special license.
See the LICENSE file in the project root for further information.