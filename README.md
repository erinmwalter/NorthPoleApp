# NorthPoleApp

Christmas-themed MVC app I made to practice MVC as well as connecting C# and Html/Razor pages in Views to back-end SQL using dapper.

Allows a child to write a letter to santa and send it in (only Create functionality, and no access to other pages).
Children can choose which gift they would like (MTM relationship between letters <-> gifts chosen).

Allows the elves (employees) CRU functionality to login and see their task list, update the status on tasks, and 
reassign tasks to other elves (Elves -> tasks is OTM relationship). 

Allows Santa (admin) full CRUD functionality both with employees and being able to edit/add/delete employees profiles,
but also to read letters, assign elves tasks, update the letter to determine whether the child was going to get the
requested gift or a lump of coal, as well as delete the task once it was delivered to the child. 
