# LionSky-Project

This is fitness site . In the site you can find (Shop,Recipes,Exercises,Calculator for control weight,Trainers and more.

## :hammer_and_pick: Used Technologies

- ASP.NET Core
- Entity Framework Core
- MS SQL Server
- jQuery


## :hammer_and_pick: Tests

- Unit testing

- Component testing

- Integration testing

--screenshot


## Functionality of Roles

Guest Visitors:

    - Can check recipes,shop (without buying),classes (only categories of classes)
   
Logged Users:

    - Can add to wish list some products from shop and buy it
    
    - Can check all classes in some categorie and join in the class
    
    - Can calculate how much protein,Fat,Calories,Carbohydrates need to eat per day
    
    - Can candidate for trainer with candidature in main menu (Become Trainer)
    
Moderator User

    - Have access to Admin Panel , but only for adding products,recipes,classes,exercises
    
Admin User
    Have full access in Admin Panel 
    
    -Adding: classes,recipes,trainers,exercises,products
    
    -Editting: classes,recipes,exercises,products
    
    -Deleting: classes,recipes,exercises,products,trainers
    
     

## :gear: Seeds

### 1. Categories of classes
It will seed authomatically categories for classes (Box,Mma,Wrestling...)

### 2. Product Brands
Seeding brands of products (Universal,MyProtein,Amix....)

### 3. Product Types
Seeding product types (Protein,Bcaa,L-Carnitine...)

### 4. Types of exercises
Seeding type of exercises (Biceps,Chest,Back..)
 
### 5. Users
Seeding 20 users with 
       Account Emaila/Username : user0@lionsky.net , user1@lionsky.net, user2@lionsky.net, user3@lionsky.net and like this too user19@lionsky.net
       Account Password: user12 (For every users between user0@lionsky.net and user19@lionsky.net is same password)
       
### 5. UserRoles
Seeding 2 Roles:

Admin Role : 

  Account Email/Username : admin@lionsky.net   
  Password: admin12

Moderator Role : 

  Account Email/Username : moderator@lionsky.net   
  Password: moderator12


###  IMPORTANT
If you want to add some exercise,products,recipes,classes or trainers , you need to log in with Admin account ( because Moderator  don't have Full permission to all functionality of Admin Panel) and from Admin Panel add what you wish . 

To add class you need first to have some trainer add ( trainer is add only on real user that he will became trainer) , and after when you have trainer you can add some class with that trainer. Classes without trainer can't be added!

## :framed_picture: Screenshot - Database Diagram

![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/Database.jpg)

## :framed_picture: Screenshot - Home page

![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/homePage1.jpg)
![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/homePage3.jpg)
![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/HomePage2.jpg)
![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/HomePage5.jpg)
![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/HomePage6.jpg)
![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/HomePagePhone.jpg)

![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/HomePagePhone2.jpg)

![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/HomePagePhone3.jpg)

![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/MenuPhone.jpg)

## :framed_picture: Screenshot - Classes


## :framed_picture: Screenshot - Exercise
![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/ExercisePage.jpg)
![alt text](https://github.com/rbbozhilov/LionSky-Project/blob/main/LionSky-Images/ExercisePage.jpg)



## :framed_picture: Screenshot - Shop


## :framed_picture: Screenshot - Trainers


## :framed_picture: Screenshot - Admin Panel


## :framed_picture: Screenshot - Recipes


## License

This project is licensed under the [MIT License](LICENSE).
