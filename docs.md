# Course Management API Documentation

## Product Background

### 1. Project Overview

The Course Management API is a comprehensive learning management system designed to facilitate online education and course delivery. This system enables educational institutions and instructors to create, manage, and deliver courses while providing students with a seamless learning experience.

### 2. Problem Statement

The traditional education system faces several challenges:

- Limited accessibility to quality education
- Geographical constraints for both students and instructors
- Difficulty in managing course content and student progress
- Lack of real-time interaction between instructors and students
- Complex payment and enrollment processes
- Inefficient content delivery and management

### 3. Solution

Our Course Management API addresses these challenges by providing:

- A robust platform for course creation and management
- Real-time communication between instructors and students
- Secure payment processing and enrollment management
- Efficient content delivery through modular course structure
- Progress tracking and assessment tools
- Flexible learning environment accessible from anywhere

### 4. Target Market

The system is designed for:

- Educational institutions
- Corporate training departments
- Individual instructors
- Online learning platforms
- Professional development organizations

### 5. Key Benefits

1. For Educational Institutions:

   - Streamlined course management
   - Reduced administrative overhead
   - Enhanced student engagement
   - Scalable learning solutions

2. For Instructors:

   - Easy content creation and management
   - Real-time student interaction
   - Progress monitoring tools
   - Flexible teaching methods

3. For Students:
   - Accessible learning platform
   - Self-paced learning options
   - Interactive learning experience
   - Progress tracking
   - Secure payment options

### 6. Market Opportunity

The online education market is experiencing significant growth:

- Increasing demand for flexible learning options
- Growing adoption of digital learning platforms
- Rising need for professional development
- Expanding remote learning requirements
- Growing investment in educational technology

### 7. Competitive Advantage

Our system differentiates itself through:

- Comprehensive course management features
- Real-time communication capabilities
- Secure payment integration
- Scalable architecture
- User-friendly interface
- Robust security measures
- Extensive API documentation

### 8. Future Growth

The platform is designed for future expansion:

- Integration with additional payment gateways
- Enhanced analytics and reporting
- Mobile application development
- Advanced assessment tools
- Gamification features
- AI-powered learning recommendations

## Table of Contents

1. [Project Overview](#project-overview)

   - 1.1. Introduction
   - 1.2. Project Objectives
   - 1.3. Target Users
   - 1.4. Key Features

2. [System Architecture](#system-architecture)

   - 2.1. Technology Stack
   - 2.2. System Components
   - 2.3. Database Design
   - 2.4. API Structure

3. [Core Features](#core-features)

   - 3.1. User Management
     - Authentication & Authorization
     - Role-based Access Control
     - User Profiles
   - 3.2. Course Management
     - Course Creation & Management
     - Module & Lesson Organization
     - Content Management
   - 3.3. Learning Experience
     - Document Management
     - Blog System
     - Comment System
   - 3.4. Payment Integration
     - Order Management
     - VNPay Integration
     - Payment Processing

4. [Technical Implementation](#technical-implementation)

   - 4.1. Authentication System
     - JWT Implementation
     - Email Confirmation
     - Password Management
   - 4.2. Real-time Communication
     - SignalR Integration
     - Live Updates
   - 4.3. File Management
     - MinIO Integration
     - File Upload/Download
   - 4.4. Database Operations
     - Entity Framework Core
     - Repository Pattern
     - Unit of Work

5. [Testing & Quality Assurance](#testing--quality-assurance)

   - 5.1. Test Cases
     - Authentication Tests
     - Course Management Tests
     - Payment Tests
     - Security Tests
   - 5.2. Performance Testing
   - 5.3. Security Testing

6. [Deployment & Maintenance](#deployment--maintenance)

   - 6.1. Deployment Process
   - 6.2. Environment Configuration
   - 6.3. Monitoring & Logging
   - 6.4. Backup & Recovery

7. [Future Enhancements](#future-enhancements)

   - 7.1. Planned Features
   - 7.2. Scalability Improvements
   - 7.3. Integration Opportunities

8. [Conclusion](#conclusion)
   - 8.1. Project Achievements
   - 8.2. Lessons Learned
   - 8.3. Recommendations

# II. Features

| No  | Feature                              | Role          | Description                                                                                                                                                                                                      |
| --- | ------------------------------------ | ------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 01  | User Registration and Authentication | User          | Users need to create accounts and log in to access personalized features.                                                                                                                                        |
| 02  | Course Management                    | Admin         | Administrators need to create, update, delete, and manage course information (e.g., title, description, level, preview image, etc.). Courses may have different levels (e.g., Beginner, Intermediate, Advanced). |
| 03  | Module Management                    | Admin         | Administrators can organize course content into modules, set module order, and manage module details.                                                                                                            |
| 04  | Lesson Management                    | Admin         | Administrators can create and manage lessons within modules, including content, duration, and prerequisites.                                                                                                     |
| 05  | Document Management                  | Admin/Teacher | Teachers can upload and manage course materials, including PDFs, videos, and other learning resources.                                                                                                           |
| 06  | Blog System                          | Admin/Teacher | Teachers can create and manage blog posts to share updates, announcements, and additional learning resources.                                                                                                    |
| 07  | Comment System                       | User          | Users can comment on blog posts and lessons to ask questions or share insights.                                                                                                                                  |
| 08  | Category Management                  | Admin         | Administrators can create and manage course categories to organize courses by subject or topic.                                                                                                                  |
| 09  | Order Management                     | User          | Users can browse courses, add them to cart, and complete the purchase process.                                                                                                                                   |
| 10  | Payment Integration                  | User          | Users can make payments for courses using VNPay payment gateway.                                                                                                                                                 |
| 11  | Real-time Notifications              | User          | Users receive instant notifications about course updates, new comments, and payment status.                                                                                                                      |
| 12  | Role-based Access Control            | Admin         | Administrators can assign different roles (Admin, Teacher, Student) to users with appropriate permissions.                                                                                                       |
| 13  | User Profile Management              | User          | Users can view and update their profile information, including personal details and learning history.                                                                                                            |
| 14  | Course Enrollment                    | User          | Users can enroll in courses and track their learning progress.                                                                                                                                                   |
| 15  | Course Search and Filtering          | User          | Users can search for courses by title, category, or level, and apply various filters.                                                                                                                            |
| 16  | Course Preview                       | User          | Users can preview course content before enrollment to make informed decisions.                                                                                                                                   |
| 17  | Learning Progress Tracking           | User          | Users can track their progress through courses, including completed lessons and modules.                                                                                                                         |
| 18  | Course Reviews and Ratings           | User          | Users can rate and review courses they have completed.                                                                                                                                                           |
| 19  | Email Notifications                  | User          | Users receive email notifications for account-related activities and course updates.                                                                                                                             |
| 20  | File Upload and Storage              | Admin/Teacher | Teachers can upload and manage course materials using MinIO storage service.                                                                                                                                     |
| 21  | API Rate Limiting                    | System        | System implements rate limiting to prevent abuse and ensure fair usage.                                                                                                                                          |
| 22  | Security Features                    | System        | System implements various security measures including JWT authentication, password hashing, and CORS policies.                                                                                                   |
| 23  | API Documentation                    | System        | System provides comprehensive API documentation using Swagger/OpenAPI.                                                                                                                                           |
| 24  | Error Handling                       | System        | System implements robust error handling and logging mechanisms.                                                                                                                                                  |
| 25  | Performance Optimization             | System        | System implements caching and other performance optimization techniques.                                                                                                                                         |

# III. Entity Relationship Diagram (ERD)

## 1. User Management

### Entity: User (AspNetUsers)

- **Primary Key**: Id (nvarchar(450))
- **Attributes**:
  - FullName (nvarchar(max))
  - UserName (nvarchar(256))
  - Email (nvarchar(256))
  - EmailConfirmed (bit)
  - PasswordHash (nvarchar(max))
  - PhoneNumber (nvarchar(max))
  - PhoneNumberConfirmed (bit)
  - TwoFactorEnabled (bit)
  - LockoutEnd (datetimeoffset)
  - LockoutEnabled (bit)
  - AccessFailedCount (int)
  - VipExpirationDate (datetime2)
  - VipPrice (decimal)
  - VipStatus (int)

### Entity: Role (AspNetRoles)

- **Primary Key**: Id (nvarchar(450))
- **Attributes**:
  - Name (nvarchar(256))
  - NormalizedName (nvarchar(256))
  - ConcurrencyStamp (nvarchar(max))

### Entity: UserRole (AspNetUserRoles)

- **Composite Primary Key**:
  - UserId (nvarchar(450))
  - RoleId (nvarchar(450))

## 2. Course Management

### Entity: Course

- **Primary Key**: Id (uniqueidentifier)
- **Attributes**:
  - Title (nvarchar(max))
  - Description (nvarchar(max))
  - PreviewImage (nvarchar(max))
  - PreviewVideoUrl (nvarchar(max))
  - Level (int)
  - Status (int)
  - CourseType (int)
  - CategoryId (int)

### Entity: Module

- **Primary Key**: Id (int)
- **Attributes**:
  - Title (nvarchar(max))
  - Order (int)
  - Status (int)
  - CourseId (uniqueidentifier)

### Entity: Lesson

- **Primary Key**: Id (int)
- **Attributes**:
  - Title (nvarchar(max))
  - Description (nvarchar(max))
  - UrlVideo (nvarchar(max))
  - Order (int)
  - Status (int)
  - ModuleId (int)
  - VideoDuration (time)

### Entity: Category

- **Primary Key**: Id (int)
- **Attributes**:
  - Name (nvarchar(max))
  - Description (nvarchar(max))

## 3. Learning Progress

### Entity: CourseProgress

- **Primary Key**: Id (int)
- **Attributes**:
  - UserId (nvarchar(450))
  - CourseId (uniqueidentifier)
  - LastViewedLessonId (int)

### Entity: LessonProgress

- **Composite Primary Key**:
  - UserId (nvarchar(450))
  - LessonId (int)
- **Attributes**:
  - IsCompleted (bit)

### Entity: CourseLearningOutcome

- **Primary Key**: Id (int)
- **Attributes**:
  - Outcome (nvarchar(max))
  - CourseId (uniqueidentifier)

## 4. Content Management

### Entity: Document

- **Primary Key**: Id (int)
- **Attributes**:
  - Name (nvarchar(max))
  - Type (nvarchar(max))
  - File (nvarchar(max))
  - LessonId (int)
  - FileSize (bigint)
  - UploadedAt (datetime2)

### Entity: Note

- **Primary Key**: Id (int)
- **Attributes**:
  - Content (nvarchar(max))
  - CreatedAt (datetime2)
  - UserId (nvarchar(450))
  - LessonId (int)

### Entity: Comment

- **Primary Key**: Id (int)
- **Attributes**:
  - Content (nvarchar(max))
  - UserId (nvarchar(450))
  - LessonId (int)

## 5. Blog System

### Entity: Blog

- **Primary Key**: Id (int)
- **Attributes**:
  - Title (nvarchar(200))
  - Content (nvarchar(max))
  - UrlImage (nvarchar(max))
  - CreatedAt (datetime2)
  - ViewCount (int)
  - UserId (nvarchar(450))
  - Status (int)

### Entity: BlogCategory

- **Composite Primary Key**:
  - BlogsId (int)
  - CategoriesId (int)

## 6. Payment System

### Entity: Order

- **Primary Key**: Id (int)
- **Attributes**:
  - UserId (nvarchar(450))
  - Status (int)
  - TotalAmount (decimal)
  - OrderDate (datetime2)
  - PurchasedPlan (int)
  - VipPrice (decimal)
  - VipExpirationDate (datetime2)

### Entity: Enrollment

- **Composite Primary Key**:
  - UserId (nvarchar(450))
  - CourseId (uniqueidentifier)
- **Attributes**:
  - EnrollmentDate (datetime2)

## Relationships

1. User - Role: Many-to-Many through UserRole
2. Course - Module: One-to-Many
3. Module - Lesson: One-to-Many
4. Course - Category: Many-to-One
5. Course - CourseProgress: One-to-Many
6. Lesson - LessonProgress: One-to-Many
7. Course - CourseLearningOutcome: One-to-Many
8. Lesson - Document: One-to-Many
9. Lesson - Note: One-to-Many
10. Lesson - Comment: One-to-Many
11. Blog - Category: Many-to-Many through BlogCategory
12. User - Order: One-to-Many
13. Course - Enrollment: Many-to-Many through Enrollment

# IV. Test Cases

## 1. Authentication & Authorization Tests

### 1.1. User Registration

| Test ID | Test Case                       | Test Data                                                            | Expected Result                           | Status |
| ------- | ------------------------------- | -------------------------------------------------------------------- | ----------------------------------------- | ------ |
| TC001   | Register with valid information | Email: user@example.com<br>Password: Test@123<br>FullName: Test User | Successfully created account, returns 201 | -      |
| TC002   | Register with existing email    | Email: existing@example.com<br>Password: Test@123                    | Returns 400 Bad Request                   | -      |
| TC003   | Register with weak password     | Email: new@example.com<br>Password: 123                              | Returns 400 with invalid password message | -      |
| TC004   | Register with invalid email     | Email: invalid.email<br>Password: Test@123                           | Returns 400 with invalid email message    | -      |
| TC005   | Register with empty data        | Email: ""<br>Password: ""                                            | Returns 400 Bad Request                   | -      |

### 1.2. User Login

| Test ID | Test Case                      | Test Data                                         | Expected Result                     | Status |
| ------- | ------------------------------ | ------------------------------------------------- | ----------------------------------- | ------ |
| TC006   | Login with correct credentials | Email: user@example.com<br>Password: Test@123     | Successful login, returns JWT token | -      |
| TC007   | Login with non-existent email  | Email: notexist@example.com<br>Password: Test@123 | Returns 401 Unauthorized            | -      |
| TC008   | Login with incorrect password  | Email: user@example.com<br>Password: Wrong@123    | Returns 401 Unauthorized            | -      |
| TC009   | Login with locked account      | Email: locked@example.com<br>Password: Test@123   | Returns 403 Forbidden               | -      |

### 1.3. Password Management

| Test ID | Test Case                          | Test Data                                             | Expected Result                            | Status |
| ------- | ---------------------------------- | ----------------------------------------------------- | ------------------------------------------ | ------ |
| TC010   | Change password successfully       | OldPassword: "Test@123"<br>NewPassword: "NewTest@123" | Password changed successfully, returns 200 | -      |
| TC011   | Forgot password - Send reset email | Email: user@example.com                               | Email sent successfully, returns 200       | -      |
| TC012   | Reset password with valid token    | Token: valid_token<br>NewPassword: "Reset@123"        | Reset successful, returns 200              | -      |
| TC013   | Reset password with invalid token  | Token: invalid_token<br>NewPassword: "Reset@123"      | Returns 400 Bad Request                    | -      |

### 1.4. Email Confirmation

| Test ID | Test Case                        | Test Data               | Expected Result                      | Status |
| ------- | -------------------------------- | ----------------------- | ------------------------------------ | ------ |
| TC014   | Confirm email with valid token   | Token: valid_token      | Confirmation successful, returns 200 | -      |
| TC015   | Confirm email with expired token | Token: expired_token    | Returns 400 Bad Request              | -      |
| TC016   | Resend confirmation email        | Email: user@example.com | Email sent successfully, returns 200 | -      |

## 2. Role Management Tests

### 2.1. Role Operations

| Test ID | Test Case            | Test Data                        | Expected Result                         | Status |
| ------- | -------------------- | -------------------------------- | --------------------------------------- | ------ |
| TC017   | Create new role      | RoleName: "Teacher"              | Role created successfully, returns 201  | -      |
| TC018   | Create existing role | RoleName: "Admin"                | Returns 400 Bad Request                 | -      |
| TC019   | Delete role          | RoleName: "Teacher"              | Deletion successful, returns 200        | -      |
| TC020   | Assign role to user  | UserId: 1<br>RoleName: "Teacher" | Role assigned successfully, returns 200 | -      |

## 3. Course Management Tests

### 3.1. Course CRUD Operations

| Test ID | Test Case                 | Test Data                                                                                                                | Expected Result                          | Status |
| ------- | ------------------------- | ------------------------------------------------------------------------------------------------------------------------ | ---------------------------------------- | ------ |
| TC021   | Create new course         | Name: "C# Programming"<br>Description: "Basic C#"<br>StartDate: "2024-04-01"<br>EndDate: "2024-06-01"<br>MaxStudents: 30 | Course created successfully, returns 201 | -      |
| TC022   | Get course list           | -                                                                                                                        | Returns course list, code 200            | -      |
| TC023   | Get course by ID          | CourseId: 1                                                                                                              | Returns course information, code 200     | -      |
| TC024   | Get non-existent course   | CourseId: 999                                                                                                            | Returns 404 Not Found                    | -      |
| TC025   | Update course information | CourseId: 1<br>Name: "Advanced C#"                                                                                       | Update successful, returns 200           | -      |
| TC026   | Delete course             | CourseId: 1                                                                                                              | Deletion successful, returns 204         | -      |

### 3.2. Course Filtering and Sorting

| Test ID | Test Case              | Test Data                                        | Expected Result                   | Status |
| ------- | ---------------------- | ------------------------------------------------ | --------------------------------- | ------ |
| TC027   | Filter courses by name | SearchTerm: "C#"                                 | Returns matching course list      | -      |
| TC028   | Filter courses by date | StartDate: "2024-04-01"<br>EndDate: "2024-06-01" | Returns courses within date range | -      |
| TC029   | Sort courses by name   | SortBy: "name"<br>Order: "asc"                   | Returns sorted list               | -      |

## 4. Module Management Tests

### 4.1. Module Operations

| Test ID | Test Case                  | Test Data                                                                 | Expected Result                          | Status |
| ------- | -------------------------- | ------------------------------------------------------------------------- | ---------------------------------------- | ------ |
| TC030   | Create new module          | CourseId: 1<br>Name: "Introduction"<br>Description: "Course introduction" | Module created successfully, returns 201 | -      |
| TC031   | Get module list for course | CourseId: 1                                                               | Returns module list, code 200            | -      |
| TC032   | Update module information  | ModuleId: 1<br>Name: "Updated Introduction"                               | Update successful, returns 200           | -      |
| TC033   | Delete module              | ModuleId: 1                                                               | Deletion successful, returns 204         | -      |

## 5. Lesson Management Tests

### 5.1. Lesson Operations

| Test ID | Test Case                  | Test Data                                                         | Expected Result                          | Status |
| ------- | -------------------------- | ----------------------------------------------------------------- | ---------------------------------------- | ------ |
| TC034   | Create new lesson          | ModuleId: 1<br>Title: "First Lesson"<br>Content: "Lesson content" | Lesson created successfully, returns 201 | -      |
| TC035   | Get lesson list for module | ModuleId: 1                                                       | Returns lesson list, code 200            | -      |
| TC036   | Update lesson content      | LessonId: 1<br>Content: "Updated content"                         | Update successful, returns 200           | -      |
| TC037   | Delete lesson              | LessonId: 1                                                       | Deletion successful, returns 204         | -      |

## 6. Document Management Tests

### 6.1. Document Operations

| Test ID | Test Case         | Test Data                     | Expected Result                  | Status |
| ------- | ----------------- | ----------------------------- | -------------------------------- | ------ |
| TC038   | Upload document   | File: test.pdf<br>LessonId: 1 | Upload successful, returns 201   | -      |
| TC039   | Get document list | LessonId: 1                   | Returns document list, code 200  | -      |
| TC040   | Download document | DocumentId: 1                 | Returns file stream, code 200    | -      |
| TC041   | Delete document   | DocumentId: 1                 | Deletion successful, returns 204 | -      |

## 7. Blog Management Tests

### 7.1. Blog Operations

| Test ID | Test Case            | Test Data                                    | Expected Result                        | Status |
| ------- | -------------------- | -------------------------------------------- | -------------------------------------- | ------ |
| TC042   | Create new blog post | Title: "New Blog"<br>Content: "Blog content" | Post created successfully, returns 201 | -      |
| TC043   | Get blog list        | -                                            | Returns blog list, code 200            | -      |
| TC044   | Update blog post     | BlogId: 1<br>Content: "Updated content"      | Update successful, returns 200         | -      |
| TC045   | Delete blog post     | BlogId: 1                                    | Deletion successful, returns 204       | -      |

## 8. Comment Management Tests

### 8.1. Comment Operations

| Test ID | Test Case             | Test Data                                  | Expected Result                         | Status |
| ------- | --------------------- | ------------------------------------------ | --------------------------------------- | ------ |
| TC046   | Add comment to blog   | BlogId: 1<br>Content: "New comment"        | Comment added successfully, returns 201 | -      |
| TC047   | Get comments for blog | BlogId: 1                                  | Returns comment list, code 200          | -      |
| TC048   | Update comment        | CommentId: 1<br>Content: "Updated comment" | Update successful, returns 200          | -      |
| TC049   | Delete comment        | CommentId: 1                               | Deletion successful, returns 204        | -      |

## 9. Category Management Tests

### 9.1. Category Operations

| Test ID | Test Case           | Test Data                                                 | Expected Result                            | Status |
| ------- | ------------------- | --------------------------------------------------------- | ------------------------------------------ | ------ |
| TC050   | Create new category | Name: "Programming"<br>Description: "Programming courses" | Category created successfully, returns 201 | -      |
| TC051   | Get category list   | -                                                         | Returns category list, code 200            | -      |
| TC052   | Update category     | CategoryId: 1<br>Name: "Updated Category"                 | Update successful, returns 200             | -      |
| TC053   | Delete category     | CategoryId: 1                                             | Deletion successful, returns 204           | -      |

## 10. Order and Payment Tests

### 10.1. Order Operations

| Test ID | Test Case           | Test Data                         | Expected Result                         | Status |
| ------- | ------------------- | --------------------------------- | --------------------------------------- | ------ |
| TC054   | Create new order    | CourseId: 1<br>UserId: 1          | Order created successfully, returns 201 | -      |
| TC055   | Get order list      | UserId: 1                         | Returns order list, code 200            | -      |
| TC056   | Update order status | OrderId: 1<br>Status: "Completed" | Update successful, returns 200          | -      |
| TC057   | Cancel order        | OrderId: 1                        | Cancellation successful, returns 200    | -      |

### 10.2. Payment Operations

| Test ID | Test Case           | Test Data                 | Expected Result                             | Status |
| ------- | ------------------- | ------------------------- | ------------------------------------------- | ------ |
| TC058   | Process payment     | OrderId: 1<br>Amount: 100 | Payment processed successfully, returns 200 | -      |
| TC059   | Get payment history | UserId: 1                 | Returns payment history, code 200           | -      |
| TC060   | Refund payment      | PaymentId: 1              | Refund processed successfully, returns 200  | -      |

## 11. Real-time Communication Tests

### 11.1. SignalR Hub Tests

| Test ID | Test Case                       | Test Data                   | Expected Result              | Status |
| ------- | ------------------------------- | --------------------------- | ---------------------------- | ------ |
| TC061   | Connect to hub                  | -                           | Connection successful        | -      |
| TC062   | Send new course notification    | Message: "New course added" | Clients receive notification | -      |
| TC063   | Send course update notification | Message: "Course updated"   | Clients receive notification | -      |
| TC064   | Disconnect from hub             | -                           | Disconnection successful     | -      |

## 12. Security Tests

### 12.1. Authentication Security

| Test ID | Test Case                 | Test Data         | Expected Result             | Status |
| ------- | ------------------------- | ----------------- | --------------------------- | ------ |
| TC065   | Access API without token  | -                 | Returns 401 Unauthorized    | -      |
| TC066   | Access with expired token | ExpiredToken      | Returns 401 Unauthorized    | -      |
| TC067   | Access with invalid token | InvalidToken      | Returns 401 Unauthorized    | -      |
| TC068   | Refresh token             | ValidRefreshToken | Returns new token, code 200 | -      |

### 12.2. Authorization Security

| Test ID | Test Case                             | Test Data                | Expected Result                  | Status |
| ------- | ------------------------------------- | ------------------------ | -------------------------------- | ------ |
| TC069   | Test Admin permissions                | AdminToken               | Successfully access admin APIs   | -      |
| TC070   | Test Teacher permissions              | TeacherToken             | Successfully access teacher APIs | -      |
| TC071   | Test Student permissions              | StudentToken             | Successfully access student APIs | -      |
| TC072   | Access without sufficient permissions | StudentToken -> AdminAPI | Returns 403 Forbidden            | -      |

## 13. Performance Tests

| Test ID | Test Case                             | Test Data                 | Expected Result                      | Status |
| ------- | ------------------------------------- | ------------------------- | ------------------------------------ | ------ |
| TC073   | Load test with 100 requests/second    | 100 concurrent users      | Response time < 2s                   | -      |
| TC074   | Stress test with 1000 requests/second | 1000 concurrent users     | Server handles load stably           | -      |
| TC075   | Test concurrent enrollment limits     | 50 concurrent enrollments | Consistent processing, no duplicates | -      |

## 14. API Rate Limiting Tests

| Test ID | Test Case                      | Test Data            | Expected Result               | Status |
| ------- | ------------------------------ | -------------------- | ----------------------------- | ------ |
| TC076   | Exceed API rate limit          | >100 requests/minute | Returns 429 Too Many Requests | -      |
| TC077   | Reset rate limit after timeout | Wait 1 minute        | Can make API calls again      | -      |

Notes:

1. Fill in "Status" when performing tests
2. Status can be: Pass/Fail/Blocked/Not Tested
3. Execute tests in order from basic to advanced
4. Document any bugs found during testing
5. Update test cases when new features are added
6. Ensure testing covers both happy path and error path scenarios
7. Thoroughly check validations and error messages
8. Test security-related functions carefully

# V. Code Appendix

## 1. Program.cs Configuration

```csharp
var builder = WebApplication.CreateBuilder(args);

// Identity Configuration
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});

// Database Configuration
builder.Services.AddDbContext<CourseManagementDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext"));
});

// SignalR Configuration
builder.Services.AddSignalR();

// Identity Configuration
builder.Services.AddIdentityApiEndpoints<AppUser>().
    AddRoles<IdentityRole>().
    AddEntityFrameworkStores<CourseManagementDb>();

// Service Registration
builder.Services.AddScoped<MinioFileService>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

// Business Service Registration
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Email Service Configuration
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<MailService>();

// CORS Configuration
builder.Services.AddCors(option => option.AddPolicy("wasm",
    policy => policy.WithOrigins(builder.Configuration["BackendUrl"] ?? "",
    builder.Configuration["FrontendUrl"] ?? "")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithExposedHeaders("Content-Disposition")
    ));
```

## 2. Authentication Controller

```csharp
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {
    private readonly UserManager<AppUser> _userManager;
    private readonly MailService _mailService;
    private readonly IConfiguration _config;

    // User Registration
    [HttpPost("register/v1")]
    public async Task<IActionResult> Register([FromBody] RegisterSdi req)
    {
        var userExists = await _userManager.FindByEmailAsync(req.Email);
        if (userExists != null) {
            return BadRequest(new { Errors = new[] { $"User {req.Email} already exists!" } });
        }

        var user = new AppUser {
            FullName = req.FullName,
            UserName = req.Email,
            Email = req.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded) {
            return BadRequest(new { Errors = result.Errors.Select(p => p.Description) });
        }

        await _userManager.AddToRoleAsync(user, "User");

        // Send confirmation email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var frontendUrl = _config["FrontendUrl"];
        var confirmationLink = $"{frontendUrl}/confirm-email?email={user.Email}&token={WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))}";
        await _mailService.SendEmailAsync(req.Email, "Email Confirmation",
            $"Confirm your email by <a href='{confirmationLink}'>clicking here</a>.");

        return Ok(new { Message = "User registered successfully" });
    }

    // Password Management
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVm req)
    {
        var response = new ApiResponse<string>();
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        if (userEmail == null)
        {
            response.StatusCode = 400;
            response.Message = "User email not found.";
            return BadRequest(response);
        }

        var user = await _userManager.FindByEmailAsync(userEmail.Value);
        if (user == null)
        {
            response.StatusCode = 400;
            response.Message = "User not found.";
            return BadRequest(response);
        }

        var result = await _userManager.ChangePasswordAsync(user, req.CurrentPassword, req.NewPassword);
        if (!result.Succeeded)
        {
            response.StatusCode = 400;
            response.Message = "Password change failed.";
            response.Data = string.Join("\n", result.Errors.Select(e => e.Description));
            return BadRequest(response);
        }

        response.StatusCode = 200;
        response.Message = "Password changed successfully.";
        return Ok(response);
    }
}
```

## 3. SignalR Hub Implementation

```csharp
public class CommentHub : Hub
{
    public async Task SendComment(string message)
    {
        await Clients.All.SendAsync("ReceiveComment", message);
    }

    public async Task SendCourseUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveCourseUpdate", message);
    }
}
```

## 4. Database Context Configuration

```csharp
public class CourseManagementDb : DbContext
{
    public CourseManagementDb(DbContextOptions<CourseManagementDb> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
}
```

## 5. API Response Model

```csharp
public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
```

## 6. Email Service Configuration

```csharp
public class MailSettings
{
    public string Mail { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
}

public class MailService : IEmailSender
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
```

## 7. File Upload Service

```csharp
public class MinioFileService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _baseUrl;

    public MinioFileService(IConfiguration configuration)
    {
        var awsConfig = new AmazonS3Config
        {
            ServiceURL = configuration["AWS:ServiceURL"]
        };

        _s3Client = new AmazonS3Client(
            configuration["AWS:AccessKey"],
            configuration["AWS:SecretKey"],
            awsConfig
        );

        _bucketName = configuration["AWS:BucketName"];
        _baseUrl = configuration["AWS:BaseUrl"];
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var fileStream = file.OpenReadStream();

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(putRequest);
        return $"{_baseUrl}/{fileName}";
    }
}
```

## 8. Payment Integration

```csharp
public class VnPayService : IVnPayService
{
    private readonly IConfiguration _configuration;
    private readonly string _vnp_Url;
    private readonly string _vnp_ReturnUrl;
    private readonly string _vnp_TmnCode;
    private readonly string _vnp_HashSecret;

    public VnPayService(IConfiguration configuration)
    {
        _configuration = configuration;
        _vnp_Url = _configuration["VnPay:Url"];
        _vnp_ReturnUrl = _configuration["VnPay:ReturnUrl"];
        _vnp_TmnCode = _configuration["VnPay:TmnCode"];
        _vnp_HashSecret = _configuration["VnPay:HashSecret"];
    }

    public string CreatePaymentUrl(PaymentRequest request)
    {
        var vnp_Params = new Dictionary<string, string>
        {
            { "vnp_Version", "2.1.0" },
            { "vnp_Command", "pay" },
            { "vnp_TmnCode", _vnp_TmnCode },
            { "vnp_Amount", (request.Amount * 100).ToString() },
            { "vnp_CurrCode", "VND" },
            { "vnp_TxnRef", request.OrderId.ToString() },
            { "vnp_OrderInfo", request.OrderInfo },
            { "vnp_OrderType", "other" },
            { "vnp_Locale", "vn" },
            { "vnp_ReturnUrl", _vnp_ReturnUrl },
            { "vnp_IpAddr", request.IpAddress }
        };

        vnp_Params.Add("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

        var sortedParams = new SortedDictionary<string, string>(vnp_Params);
        var queryString = string.Join("&", sortedParams.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));
        var signData = $"{queryString}";
        var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_vnp_HashSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signData));
        var signature = BitConverter.ToString(hash).Replace("-", "").ToLower();
        vnp_Params.Add("vnp_SecureHash", signature);

        return $"{_vnp_Url}?{string.Join("&", vnp_Params.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"))}";
    }
}
```

## 9. AutoMapper Configuration

```csharp
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseDTO>().ReverseMap();
        CreateMap<Module, ModuleDTO>().ReverseMap();
        CreateMap<Lesson, LessonDTO>().ReverseMap();
        CreateMap<Document, DocumentDTO>().ReverseMap();
        CreateMap<Comment, CommentDTO>().ReverseMap();
        CreateMap<Blog, BlogDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<Payment, PaymentDTO>().ReverseMap();
        CreateMap<AppUser, UserDTO>().ReverseMap();
    }
}
```

## 10. Swagger Configuration

```csharp
builder.Services.AddSwaggerGen(option =>
{
    option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
});
```

These code snippets represent the core functionality and configuration of the Course Management API. The implementation includes:

1. Program configuration with dependency injection
2. Authentication and authorization
3. Real-time communication using SignalR
4. Database context setup
5. Standardized API response model
6. Email service for notifications
7. File upload service using MinIO
8. Payment integration with VNPay
9. AutoMapper configuration for DTOs
10. Swagger documentation setup

The code follows best practices for:

- Dependency injection
- Separation of concerns
- Error handling
- Security
- API documentation
- Real-time communication
- File management
- Payment processing

# VI. References

## 1. Official Documentation

1. ASP.NET Core Documentation

   - [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api)
   - [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
   - [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core)
   - [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr)

2. Authentication & Authorization

   - [JWT Authentication](https://jwt.io/introduction)
   - [OAuth 2.0](https://oauth.net/2/)
   - [ASP.NET Core Identity Documentation](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)

3. Database & ORM

   - [SQL Server Documentation](https://docs.microsoft.com/en-us/sql/sql-server)
   - [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core)
   - [Code First Approach](https://docs.microsoft.com/en-us/ef/core/modeling)

4. Real-time Communication

   - [SignalR Documentation](https://docs.microsoft.com/en-us/aspnet/core/signalr)
   - [WebSocket Protocol](https://developer.mozilla.org/en-US/docs/Web/API/WebSocket)

5. File Storage

   - [MinIO Documentation](https://docs.min.io/)
   - [Amazon S3 API](https://docs.aws.amazon.com/AmazonS3/latest/API/Welcome.html)

6. Payment Integration
   - [VNPay API Documentation](https://sandbox.vnpayment.vn/apis/docs)
   - [Payment Gateway Integration](https://developers.vnpay.vn/apis/docs)

## 2. Best Practices & Guidelines

1. API Design

   - [RESTful API Design Guidelines](https://restfulapi.net/)
   - [Microsoft REST API Guidelines](https://github.com/microsoft/api-guidelines)
   - [API Versioning Best Practices](https://restfulapi.net/versioning/)

2. Security

   - [OWASP Security Guidelines](https://owasp.org/www-project-top-ten/)
   - [ASP.NET Core Security Documentation](https://docs.microsoft.com/en-us/aspnet/core/security)
   - [CORS Best Practices](https://docs.microsoft.com/en-us/aspnet/core/security/cors)

3. Testing

   - [Unit Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
   - [Integration Testing in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests)
   - [API Testing Guidelines](https://www.postman.com/api-documentation-tool/)

4. Performance
   - [ASP.NET Core Performance Best Practices](https://docs.microsoft.com/en-us/aspnet/core/performance)
   - [Entity Framework Performance](https://docs.microsoft.com/en-us/ef/core/performance)
   - [Caching Best Practices](https://docs.microsoft.com/en-us/aspnet/core/performance/caching)

## 3. Tools & Libraries

1. Development Tools

   - [Visual Studio](https://visualstudio.microsoft.com/)
   - [Visual Studio Code](https://code.visualstudio.com/)
   - [Postman](https://www.postman.com/)

2. NuGet Packages

   - [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore)
   - [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)
   - [AutoMapper](https://www.nuget.org/packages/AutoMapper)
   - [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore)
   - [AWSSDK.S3](https://www.nuget.org/packages/AWSSDK.S3)

3. Version Control
   - [Git Documentation](https://git-scm.com/doc)
   - [GitHub Documentation](https://docs.github.com/)

## 4. Community Resources

1. Forums & Communities

   - [Stack Overflow](https://stackoverflow.com/questions/tagged/asp.net-core)
   - [ASP.NET Core GitHub](https://github.com/dotnet/aspnetcore)
   - [Microsoft Q&A](https://docs.microsoft.com/en-us/answers/topics/dotnet-core.html)

2. Learning Resources

   - [Microsoft Learn](https://learn.microsoft.com/)
   - [Pluralsight](https://www.pluralsight.com/)
   - [Udemy](https://www.udemy.com/)

3. Code Samples
   - [ASP.NET Core Samples](https://github.com/dotnet/aspnetcore/tree/main/src/Samples)
   - [Entity Framework Core Samples](https://github.com/dotnet/EntityFramework.Docs/tree/main/samples)

## 5. Standards & Protocols

1. Web Standards

   - [HTTP/1.1](https://tools.ietf.org/html/rfc2616)
   - [HTTPS](https://tools.ietf.org/html/rfc2818)
   - [CORS](https://www.w3.org/TR/cors/)

2. Security Standards

   - [OAuth 2.0](https://oauth.net/2/)
   - [JWT](https://tools.ietf.org/html/rfc7519)
   - [CORS](https://www.w3.org/TR/cors/)

3. API Standards
   - [REST](https://www.ics.uci.edu/~fielding/pubs/dissertation/rest_arch_style.htm)
   - [JSON](https://www.json.org/)
   - [OpenAPI/Swagger](https://swagger.io/specification/)
