# GameOfLife

Conways game of life screensaver. 

I got bored one day and decided to implement Conway's Game of Life as a windows screensaver. I used the optimized algorithm from Michael Abrash's Graphics Programming Black Book (http://www.amazon.com/Michael-Abrashs-Graphics-Programming-Special/dp/1576101746). I keep the screen saver going by dropping multiple circles of differnet radii of alive cells around a point once the population density reaches a certain threshhold this way the life doesn't fizz out.

The screen saver is almost completely configurable. Configurable items include:
  o Cell Size
  o Seed population density (number of alive cells in the seed generation)
  o Max circle radius (maximum radius of the circle that is dropped to keep life going)
  o Circle drop threshold (drop circle when population density is x percent below seed cell count)
  o Cell color can be set to a sepcific RGB using color dialog or set to cycle through colors
  o Max FPS
  
To get started, pull down the code and open the solution file in Visual Studio. Run time parameters include:
  o /c - Open configuration form
  o /s - Start screensaver 
  o /p:0000 - Start screen saver using window pointer "0000"

To install the screen saver, rename the complied binary "GameOfLife.exe" to "GameOfLife.scr" then right-click and choose install. This wil bring up the windows screensaver settings. To configure the screensaver click the "Settings" button. 
