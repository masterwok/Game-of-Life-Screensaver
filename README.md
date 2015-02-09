# GameOfLife
Conways game of life screensaver. 

I got bored one day and decided to implement Conway's Game of Life as a windows screensaver. I used the optimized algorithm from Michael Abrash's Graphics Programming Black Book (http://www.amazon.com/Michael-Abrashs-Graphics-Programming-Special/dp/1576101746). I keep the screen saver going by dropping multiple circles of differnet radii of alive cells around a point once the population density reaches a certain threshhold this way the life doesn't fizz out.

TODO:

o Implement configuration form.

o Fix out-of-bounds exception from circle wrapping and uncomment the code that drops the circles.
