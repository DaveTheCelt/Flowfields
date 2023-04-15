# 2D Flowfield Navigation

This repository is a demonstration of a flowfield. Each grid tile points to the direction of the target. Flowfields are a great optimisation when you have many agents that wish to navigate towards a singular target. This is a common technique used in RTS games.

This demonstration is not made with optimisation in mind but there are many ways to optimise it such as the use of threading or even GPU processing.

## **NOTE**
<br>
This repository requires my <a href="https://github.com/TheCelticGuy/AStar-Pathfinding-Algorithm">A* Pathfinding Algorithm Repository</a> to work since it uses the A* pathfinding algorithm to calculate the flowfields. So make sure you import that repository aswell.

# Preview

<img src="https://user-images.githubusercontent.com/33559521/230799543-bd8356e8-c4d3-4046-ac40-2c71f3ca0d23.gif" width="50%" height="50%"/>

# Source

Helpful sources to learn the algorithm:

<a href="https://github.com/TheCelticGuy/Flowfields/files/11186436/GameAIPro_Chapter23_Crowd_Pathfinding_and_Steering_Using_Flow_Field_Tiles.pdf"> PDF :: Crowd Pathfinding and Steering Using Flow Field Tiles</a>
