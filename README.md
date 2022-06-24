# Jewellery
It's my project I’ve created as a part of an online course where I learned about ASP.NET MVC.

This application is an idea for a simple online store. Functionality covers listing of all products with filters, adding to cart, checkout and placing an inquiry followed by sending an email with summary. Products in cart are stored in session, you can also register as a new customer. Account panel for now gives only ability to change password (but there is a room for further expansion like inquiry history). More complete is administration panel where are all tools to maintain content of the store. You can add/edit/delete products, categories, manage users and more.

From more technical side whole project is based on ASP.NET 5.0 MVC approach with use of Entity Framework to establish connection with MS SQL database. To fulfil CRUD operations I implemented a repository pattern. On frontend layer I used existing template which I slightly adjusted using Bootstrap and CSS and modifying HTML structure. User administration panel has been scaffolded using build in ASP.NET Identity. To send emails I used Mailjet package.

To run this project locally you have to have sql database on which you have to run migrations to create and initialise all tables (don’t forget to change your connection string in appsettings.json!).

![MVC1](https://user-images.githubusercontent.com/97447088/175542440-ff1581e2-1af3-4432-8e44-d197dd3b1720.png)
![MVC2](https://user-images.githubusercontent.com/97447088/175542452-6e29c840-412f-41ca-ac32-92232ae75c23.png)
![MVC3](https://user-images.githubusercontent.com/97447088/175542455-c16b4fc0-57be-41ba-8bfb-47ca415ca939.png)
![MVC4](https://user-images.githubusercontent.com/97447088/175542457-a4641a2f-6829-456d-bc63-ab50768d3e9c.png)
