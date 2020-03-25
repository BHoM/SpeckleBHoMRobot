[![License: LGPL v3](https://img.shields.io/badge/License-LGPL%20v3-blue.svg)](https://www.gnu.org/licenses/lgpl-3.0)
# SpeckleBHoMRobot

A Speckle Client for Robot that deals in BHoMObjects.

## Milestones:

- [ ] ~~Create a Winforms Chromium browser to expose Speckle client into, using [CEFSharp](http://cefsharp.github.io/) as done in [SpeckleRhino](https://github.com/BHoM/SpeckleRhinoFork)~~
- [ ] (Currently WIP) Implement SpeckleUi as done in [SpeckleRevitReboot](https://github.com/speckleworks/SpeckleRevitReboot)
- [x] Receive data from Speckle 
- [ ] Send data to Speckle
- [ ] Convert any BHoMObject into RobotOM by using the existing Robot_Engine converts of [`Robot_Toolkit`](https://github.com/BHoM/Robot_Toolkit)
- [ ] Plan out how to get similar behaviour of Robot_Toolkit in terms of Push and Pull. Implementation of such a functionality would be well into the future. 
- [ ] A simple Import is what we can target initially (like BHoM Adapter's `CreateOnly`).
- [ ] (Well into the future) allow for export.


## Registering the Add-in in Robot
((adapted from the Autodesk Instructions))

### Create a `.tlb` file
1. Go to the folder where the add-in `.dll` is generated (e.g. …\SpeckleBHoMRobot\bin\x64\Debug) and run the following command:
```shell
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /tlb /codebase SpeckleRobotClient.dll
```
2. Add the created `.tlb` library to the add-in `.dll` file:
   * From the File menu, \Open\File -> open created add-in .dll file
   * Add created `.tlb` library to `.dll` file (right hand mouse click menu). Resource type should be named as TYPELIB.

![adding the .tlb to the .dll](https://user-images.githubusercontent.com/7717434/77532477-d4081e00-6e8c-11ea-91af-2be84d28db84.png)

3. Change TYPELIB sumber to e.g. 1.0 using Properties
4. Save and close

### Register created add-in `.dll` in RSA
1. Open Command Prompt window as Admin
2. Go to the folder where the add-in .dll file is located and register it by commands:
```shell
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /tlb /codebase SpeckleRobotClient.dll
```

### Make it available in the RSA pull down menu
1. Start RSA and select any structure type
2. From the Add-ins Menu, start the Add-ins Manager
3. Locate the path for your `.dll` file using the "..." button then press the Add button
![adding the add-in to the pulldown menu](https://user-images.githubusercontent.com/7717434/77532496-dbc7c280-6e8c-11ea-990b-dd308905dbac.png)


## Trying it Out!
1. Clone the [SpeckleUiApp Repo](https://github.com/speckleworks/SpeckleUiApp)
2. Navitage into the repo and run `npm run serve`
3. Profit!