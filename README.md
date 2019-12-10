# Space Invaders Recreation

This is a WIP recreation of Space Invaders in Unity. It was originally a project for the class "Introduction to Game Development,"
but I have been reworking it since. Originally, it was a pair project, but I have removed my partner's name for the sake of her privacy. <br/>
<br/>
Here are the aspects of the game I was responsible for or have reworked since the class: <br/>
-EnemyManager script (movement, attacking, instantiation) (the new version of TayghEnemyManager only has movement working right now) <br/>
-Custom editor code <br/>
-Player script <br/>
-GameManager script <br/>
-Setting up the game scene <br/>
<br/>
I had one goal when programming the TayghEnemyManager script: to build it in a way that is structurally scalable (just in case anyone ever
wants to create new game modes that do not follow the original rules of the game) and usable by a game designer (referred to as a user 
in code comments) without them having to ever look at the code. For example, designers define a total number of rows and the enemy prefabs
they want to use in the game. Then, the custom editor takes this information and creates a list of dropdown lists per row for the designer
to define what enemy type they want per row. This makes it easy for the designer, cleaner to look at in the Inspector, and allows for 
anyone to iterate on the original game with new types of enemies or rows with different attributes (speed at which they move, different
spacing between enemies, etc.). I will update this page as changes are made. I simply have not had time to finish it due to school and
other projects.
