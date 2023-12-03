
---
[![Release_Work_Flow](https://github.com/NolanMM/Cloud_Database_Management_System/actions/workflows/master.yml/badge.svg)](https://github.com/NolanMM/Cloud_Database_Management_System/actions/workflows/master.yml)

---

# Cloud_Database_Management_System

---

## Table of Contents

### I. Cloud_Database_Management_System

> <details><summary><strong> 1. Cloud_Database_Management_System </strong></summary>
>
> The Cloud Database Management System plays a pivotal role in the effective execution of the CRUD (Create, Read, Update, Delete) process within our Analysis and Reporting System

</details>

> <details><summary><strong> 2. ClientGetHttp </strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 3. OTP_Centre</strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 4. Security_Services_Dev_Env </strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 5. Databbase_Tests </strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 6. PageViewTable_Services_Tests </strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 7. SaleTransactionTable_Services_Tests </strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 8. FeedbackTable_Services_Tests </strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 9. TestRouteToDatabase </strong></summary>
>
> Add Later

</details>

> <details><summary><strong> 10. UserviewTable_Services_Tests </strong></summary>
>
> Add Later

</details>

---

### II. Table of Contents

- [Cloud\_Database\_Management\_System](#cloud_database_management_system)
  - [Table of Contents](#table-of-contents)
    - [I. Cloud\_Database\_Management\_System](#i-cloud_database_management_system)
    - [II. Table of Contents](#ii-table-of-contents)
      - [2. Tech Stack](#2-tech-stack)
      - [3. Endpoints](#3-endpoints)
      - [4. Cloud\_Database\_Management\_System Data Structures](#4-cloud_database_management_system-data-structures)
        - [4.1 Group\_1\_Record\_Interface](#41-group_1_record_interface)
        - [4.2 Valid User Views Data Structure](#42-valid-user-views-data-structure)
        - [4.3 Website logs Data Structure](#43-website-logs-data-structure)
        - [4.4 Sales Transactions Data Structure](#44-sales-transactions-data-structure)
        - [4.5 Feedback Data Structure](#45-feedback-data-structure)
      - [5. High Level Module Design](#5-high-level-module-design)
      - [6. MVP Designs](#6-mvp-designs)
        - [6.1 EndPoint 1: .root/Help](#61-endpoint-1-roothelp)
        - [6.2 EndPoint 2: .root/Login](#62-endpoint-2-rootlogin)
        - [6.3 EndPoint 3: .root/SignUp](#63-endpoint-3-rootsignup)
        - [6.4 Email MVP](#64-email-mvp)
      - [7. Nuget Packages Install](#7-nuget-packages-install)

---

#### 2. Tech Stack

- `Server:` [ASP.NET]() 6.0 Server API
- `Front-end:` JavaScript, HTML(string), CSS
- `Database:` Azure MySQL

[Back To Top](#cloud_database_management_system)

---

#### 3. Endpoints

<strong>.root:<https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController></strong>

<strong>Help Page:<https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/Help></strong>
<details>
  <summary><strong>Login Process Requests Options</strong></summary>
  <ul>
    <li><strong>GET</strong> -> root/Login </li>
    <li><strong>POST</strong> -> root/CheckAcount/ {username} / {password} </li>
  </ul>
</details>

<details>
  <summary><strong>Sign Up Process Requests Options</strong></summary>
  <ul>
    <li><strong>1. GET</strong> -> .root/SignUp</li>
    <li>
        <strong>2. POST</strong>
        <ul>
            <li>2.1 -> .root/Register/ {registerUsername} / {registerEmail} / {registerPassword}</li>
            <li>2.2 -> .root/VerifyOTP/ {OTP_CODE_ID (From 2.1 Response)} / {otpCode(Email)}</li>
        </ul>
    </li>
  </ul>
</details>

<details>
  <summary><strong>Get Data from Table By Table Number Related Requests</strong></summary>
  <ul>
    <li><strong>1. GET</strong> -> .root/ {username} / {password} /group {groupId} / {tableNumber}</li>
  </ul>
</details>

<details>
  <summary><strong>Get All Tables From Database Request</strong></summary>
  <ul>
    <li>1. <strong>GET</strong> -> .root/ {username} / {password} /group {groupId} /GetAllData</li>
  </ul>
</details>

<details>
  <summary><strong>Post Data to Table By DataObject Related Requests Options</strong></summary>
  <ul>
    <li><strong>1. POST -> .root/ {username} / {password} /group {groupId}/ {tableNumber}</strong>
        <ul>
            <li>1.2 Body: Data Struct</li>
        </ul>
     </li>
    <li>
        <strong>1. Sample POST Corresponding to Data</strong>
        <ul>
            <li><strong>UserView</strong> -> .root/{username}/{password}/group1/0
                <ul>
                    <li>Body: -H "Content-Type: application/json" -d '{"UserView_ID": 1, "User_ID": "user123", "Product_ID": "product123", "Time_Count": 2.5, "Date_Access": "2023-11-20T12:00:00"}'</li>
                </ul>
            </li>
            <li><strong>PageView</strong> -> .root/{username}/{password}/group1/0
                <ul>
                    <li>Body: -H "Content-Type: application/json" -d '{"PageView_ID": 1, "Page_Name": "Page1", "Page_Info": "Info1", "Product_ID": "product123", "Start_Time": "2023-11-20T12:00:00", "UserID": "user123"}'</li>
                </ul>
            </li>
            <li><strong>Transaction</strong> -> .root/{username}/{password}/group1/0
                <ul>
                    <li>Body: -H "Content-Type: application/json" -d '{"Transaction_ID": "trans123", "User_ID": "user123", "Order_Value": 100.5, "date": "2023-11-20T12:00:00", "Details_Products": "[{\"Product_ID\": \"P0006\", \"Product_Price\": 120.25, \"Product_Quantity\": 10},{\"Product_ID\": \"P884723\", \"Product_Price\": 120.25, \"Product_Quantity\": 18},{\"Product_ID\": \"P92713\", \"Product_Price\": 221.25, \"Product_Quantity\": 12}]"
}'</li>
                </ul>
            </li>
            <li><strong>Feedback</strong> -> .root/{username}/{password}/group1/0
                <ul>
                    <li>Body:-H "Content-Type: application/json" -d '{"Feedback_ID": 1, "User_ID": "user123", "Product_ID": "product123", "Stars_Rating": 4.5, "Date_Updated": "2023-11-20T12:00:00"}'
                    </li>
                </ul>
            </li>
        </ul>
    </li>

  </ul>
</details>

---

#### 4. Cloud_Database_Management_System Data Structures

##### 4.1 Group_1_Record_Interface

```CSharp
    public abstract class Group_1_Record_Interface : Group_Data_Model
    {
        public override string ToString()
    }
```

##### 4.2 Valid User Views Data Structure

```CSharp
    public class UserView : Group_1_Record_Interface
    {
        public int UserView_ID { get; set; }
        [Required]
        public string User_ID { get; set; }
        [Required]
        public string Product_ID { get; set; }
        [Required]
        public decimal Time_Count { get; set; }
        [Required]
        public DateTime Date_Access { get; set; }
        
        public override string ToString()

    }
```

##### 4.3 Website logs Data Structure

```CSharp
    public class PageView : Group_1_Record_Interface
    {
        public int PageView_ID { get; set; }
        [Required]
        [MaxLength(45)]
        public string Page_Name { get; set; }
        [Required]
        [MaxLength(45)]
        public string Page_Info { get; set; }
        [Required]
        [MaxLength(45)]
        public string Product_ID { get; set; }
        [Required]
        public DateTime Start_Time { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserID { get; set; }

        public override string ToString()
    }
```

##### 4.4 Sales Transactions Data Structure

```CSharp
    public class SaleTransaction : Group_1_Record_Interface
    {
        [Required]
        public string Transaction_ID { get; set; }
        [Required]
        public string User_ID { get; set; }
        [Required(ErrorMessage = "Order Value is required")]
        [Range(0.00001, double.MaxValue, ErrorMessage = "Required Order value > 0")]
        public decimal Order_Value { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DateNotDefault(ErrorMessage = "Date must be filled")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "The item list cannot be empty")]
        [MaxLength(10000)]
        public string Details_Products { get; set; }

        public override string ToString()
    }
```

##### 4.5 Feedback Data Structure

```CSharp
    public class Feedback : Group_1_Record_Interface
    {
        [Required]
        public int Feedback_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string User_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Product_ID { get; set; }
        [Required(ErrorMessage = "Stars Rating is required")]
        public decimal Stars_Rating { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DateNotDefault(ErrorMessage = "Date must be filled")]
        public DateTime Date_Updated { get; set; }

        public override string ToString()
    }
```

[Back To Top](#cloud_database_management_system)

#### 5. High Level Module Design

<p align="center">
  <img src="Documents\HighLevelDesign.png" alt="High Level Module Design Image" width="100%">
</p>

[Back To Top](#cloud_database_management_system)

---

#### 6. MVP Designs

##### 6.1 <strong>EndPoint 1: .root/Help</strong>

<div style="display: flex; flex-direction: column;">
  <div style="padding: 10px;">
    <strong>Design UI Help Page</strong>
    <img src="Documents\HelpPageMVD1.png" alt="MVP Designs 2" style="width: 100%;">
  </div>
  <div style="display: flex;">
    <div style="flex: 50%; padding: 10px;">
      <strong>Preview UI Help Page</strong>
      <img src="Documents\HelpPage.png" alt="MVP Preview UI 2 1" style="width: 100%;">
    </div>
  </div>
</div>

##### 6.2 <strong>EndPoint 2: .root/Login</strong>

<div style="display: flex; flex-direction: column;">
  <div style="padding: 10px;">
    <strong>Design</strong>
    <img src="Documents\LoginMVD1.png" alt="MVP Designs Login" style="width: 100%;">
  </div>
  <div style="display: flex;">
    <div style="flex: 50%; padding: 10px;">
      <strong>Preview UI Login</strong>
      <img src="Documents\Login.png" alt="MVP Preview UI Login" style="width: 100%;">
    </div>
  </div>
</div>
<div style="display: flex; flex-direction: column;">
  <div style="padding: 10px;">
    <strong>Design</strong>
    <img src="Documents\LoginMVD2.png" alt="MVP Designs Login Success" style="width: 100%;">
  </div>
  <div style="display: flex;">
    <div style="flex: 50%; padding: 10px;">
      <strong>Login Success</strong>
      <img src="Documents\Login_2.png" alt="MVP Preview UI Login Success" style="width: 100%;">
    </div>
  </div>
</div>

##### 6.3 <strong>EndPoint 3: .root/SignUp</strong>

<div style="display: flex; flex-direction: column;">
  <div style="padding: 10px;">
    <strong>Design Sign Up</strong>
    <img src="Documents\SignUpMVD1.png" alt="MVP Designs 2" style="width: 100%;">
  </div>
  <div style="display: flex;">
    <div style="flex: 50%; padding: 10px;">
      <strong>Preview UI Sign Up</strong>
      <img src="Documents\SignUp.png" alt="MVP Preview UI 2 1" style="width: 100%;">
    </div>
  </div>
</div>
<div style="display: flex; flex-direction: column;">
  <div style="padding: 10px;">
    <strong>Sign Up Success</strong>
    <img src="Documents\SignUpMVD2.png" alt="MVP Designs Sign Up Success" style="width: 100%;">
  </div>
  <div style="display: flex;">
    <div style="flex: 50%; padding: 10px;">
      <strong>Preview UI Sign Up Success</strong>
      <img src="Documents\SignUp_2.png" alt="MVP Preview UI Sign Up Success 1" style="width: 100%;">
    </div>
  </div>
</div>

##### 6.4 <strong>Email MVP</strong>

<div style="display: flex; flex-direction: column;">
  <div style="padding: 10px;">
    <strong>Design Email Template</strong>
    <img src="Documents\EmailTemplate.png" alt="MVP Designs 2" style="width: 100%;">
  </div>
  <div style="display: flex;">
    <div style="flex: 50%; padding: 10px;">
      <strong>Preview UI Email</strong>
      <img src="Documents\EmailTemplate_2.png" alt="MVP Preview UI 2 1" style="width: 100%;">
    </div>
  </div>
</div>

---

#### 7. Nuget Packages Install

- `Newtonsoft.Json: 13.0.3`
- `MySqlConnector: 2.2.7`
- `Swashbuckle.AspNetCore: 6.5.0`
- `Swashbuckle.AspNetCore.Swagger: 6.5.0`
- `Swashbuckle.AspNetCore.SwaggerGen: 6.5.0`
- `Swashbuckle.AspNetCore.SwaggerUI: 6.5.0`

[Back To Top](#cloud_database_management_system)

---
