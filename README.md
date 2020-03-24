[![License: LGPL v3](https://img.shields.io/badge/License-LGPL%20v3-blue.svg)](https://www.gnu.org/licenses/lgpl-3.0)
## SpeckleBHoMRobot

A Speckle Client for Robot that deals in BHoMObjects.

### Milestones:

- [ ] ~~Create a Winforms Chromium browser to expose Speckle client into, using [CEFSharp](http://cefsharp.github.io/) as done in [SpeckleRhino](https://github.com/BHoM/SpeckleRhinoFork)~~
- [ ] (Currently WIP) Implement SpeckleUi as done in [SpeckleRevitReboot](https://github.com/speckleworks/SpeckleRevitReboot)
- [x] Receive data from Speckle 
- [ ] Send data to Speckle
- [ ] Convert any BHoMObject into RobotOM by using the existing Robot_Engine converts of [`Robot_Toolkit`](https://github.com/BHoM/Robot_Toolkit)
- [ ] Plan out how to get similar behaviour of Robot_Toolkit in terms of Push and Pull. Implementation of such a functionality would be well into the future. 
- [ ] A simple Import is what we can target initially (like BHoM Adapter's `CreateOnly`).
- [ ] (Well into the future) allow for export.

