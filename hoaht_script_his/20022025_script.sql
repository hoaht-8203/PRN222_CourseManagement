USE [CourseManagementDb]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (10, N'Frontend', N'Frontend development focuses on building the user interface and user experience of a website or application. It involves technologies such as HTML, CSS, and JavaScript, along with modern frameworks like React, Vue.js, and Angular. Frontend developers ensure that applications are visually appealing, responsive, and provide a seamless user experience across different devices and browsers.')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (11, N'Backend', N'Back-end development means working on server-side software, which focuses on everything you can''t see on a website. Back-end developers ensure the website performs correctly, focusing on databases, back-end logic, application programming interface (APIs), architecture, and servers.')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[Courses] ([Id], [Title], [Description], [PreviewImage], [PreviewVideoUrl], [Level], [Status], [CategoryId], [CourseType]) VALUES (N'840a7353-06d2-41d9-3c87-08dd4874f647', N'Khoá học Javascript từ cơ bản đến nâng cao', N'Khoá học lập trình Javascript từ cơ bản tới nâng cao dành cho bạn. Tham gia ngay để khám phá sức mạnh của Javascript!', N'https://techvccloud.mediacdn.vn/2018/11/23/js-15429579443112042672363-crop-1542957949936317424252.png', N'https://www.youtube.com/watch?v=DHjqpvDnNGE', 1, 2, 10, 0)
INSERT [dbo].[Courses] ([Id], [Title], [Description], [PreviewImage], [PreviewVideoUrl], [Level], [Status], [CategoryId], [CourseType]) VALUES (N'96fc4ec7-f56f-479a-f36b-08dd4b00dfad', N'Khoá học Java từ cơ bản đến nâng cao', N'Khoá học lập trình Java từ cơ bản tới nâng cao dành cho bạn. Tham gia ngay để khám phá sức mạnh của Java!', N'https://logos-world.net/wp-content/uploads/2022/07/Java-Logo.png', N'https://www.youtube.com/watch?v=l9AzO1FMgM8', 1, 2, 11, 0)
INSERT [dbo].[Courses] ([Id], [Title], [Description], [PreviewImage], [PreviewVideoUrl], [Level], [Status], [CategoryId], [CourseType]) VALUES (N'cb7df934-cab9-46e4-0afa-08dd511c66f5', N'Khoá học Reactjs từ cơ bản đến nâng cao', N'Khoá học lập trình Reactjs từ cơ bản tới nâng cao dành cho bạn. Tham gia ngay để khám phá sức mạnh của Reactjs!', N'https://thuanbui.me/wp-content/uploads/2021/08/react-js.png', N'https://www.youtube.com/watch?v=Tn6-PIqc4UM', 2, 2, 10, 1)
GO
SET IDENTITY_INSERT [dbo].[Modules] ON 

INSERT [dbo].[Modules] ([Id], [Title], [CourseId], [Order], [Status]) VALUES (2, N'Giới thiệu Javascript', N'840a7353-06d2-41d9-3c87-08dd4874f647', 1, 1)
INSERT [dbo].[Modules] ([Id], [Title], [CourseId], [Order], [Status]) VALUES (3, N'Javascript cơ bản', N'840a7353-06d2-41d9-3c87-08dd4874f647', 2, 1)
INSERT [dbo].[Modules] ([Id], [Title], [CourseId], [Order], [Status]) VALUES (4, N'Object trong Javascript', N'840a7353-06d2-41d9-3c87-08dd4874f647', 3, 1)
INSERT [dbo].[Modules] ([Id], [Title], [CourseId], [Order], [Status]) VALUES (5, N'New', N'840a7353-06d2-41d9-3c87-08dd4874f647', 4, 1)
INSERT [dbo].[Modules] ([Id], [Title], [CourseId], [Order], [Status]) VALUES (9, N'new 3', N'840a7353-06d2-41d9-3c87-08dd4874f647', 6, 1)
INSERT [dbo].[Modules] ([Id], [Title], [CourseId], [Order], [Status]) VALUES (10, N'new 2', N'840a7353-06d2-41d9-3c87-08dd4874f647', 5, 1)
INSERT [dbo].[Modules] ([Id], [Title], [CourseId], [Order], [Status]) VALUES (11, N'New 4', N'840a7353-06d2-41d9-3c87-08dd4874f647', 7, 1)
SET IDENTITY_INSERT [dbo].[Modules] OFF
GO
SET IDENTITY_INSERT [dbo].[Lessons] ON 

INSERT [dbo].[Lessons] ([Id], [Title], [Description], [UrlVideo], [ModuleId], [Order], [Status], [IsCompleted]) VALUES (1, N'Giới thiệu Javascript', N'Javascript là ngôn ngữ lập trình bậc cao, cực kỳ linh hoạt được sử dụng chủ yếu để tao ra ứng dụng chạy trên trình duyệt web. Được tạo ra bởi Brendan Eich vào năm 1995. Nó thể viết code ở text editor và chạy nó trực tiếp trên trình duyệt mà không cần phải trải qua quá trình biên dịch như C++ hoặc Java.', N'https://www.youtube.com/watch?v=cIc2QfKKWUk&list=PLEe8jzHjtRiRQWZugdORFlqCGh18fwnSz', 2, 1, 1, NULL)
INSERT [dbo].[Lessons] ([Id], [Title], [Description], [UrlVideo], [ModuleId], [Order], [Status], [IsCompleted]) VALUES (2, N'Dev Tools là gì? Code javascript tại sao cần console.log', N'Trong lập trình, việc code lỗi là không thể tránh khỏi. Nhưng trên trình duyệt, bạn thường sẽ không nhìn thấy những lỗi này. Mà thay vào đó, bạn cần xem thông qua một công cụ. Đó chính là Dev Tools.', N'https://youtu.be/YDbVQK4ZdfI', 2, 3, 1, NULL)
INSERT [dbo].[Lessons] ([Id], [Title], [Description], [UrlVideo], [ModuleId], [Order], [Status], [IsCompleted]) VALUES (3, N'IDE là gì?', N'IDE là phần mềm máy tính không thể thiếu khi lập trình. Sau đây, mình sẽ cùng tìm hiểu xem IDE là gì. IDE có tác dụng gì khi lập trình JavaScript. Và một số IDE JavaScript tốt nhất.', N'https://youtu.be/yLBRjDFqx6E', 2, 2, 1, NULL)
INSERT [dbo].[Lessons] ([Id], [Title], [Description], [UrlVideo], [ModuleId], [Order], [Status], [IsCompleted]) VALUES (4, N'Chương trình Javascript đầu tiên', N'Trong toàn bộ khoá học này, chúng ta sẽ chỉ sử dụng Javascript là ngôn ngữ xây dựng ứng dụng client (front-end) trên trình duyệt.', N'https://youtu.be/b62jRbHGAk8', 3, 1, 1, NULL)
INSERT [dbo].[Lessons] ([Id], [Title], [Description], [UrlVideo], [ModuleId], [Order], [Status], [IsCompleted]) VALUES (5, N'Strict Mode trong JavaScript', N'Từ khóa use strict là từ khóa để bật chế độ Strict Mode, tạm dịch là chế độ nghiêm ngặt của JavaScript, bắt đầu xuất hiện từ phiên bản ECMAScript 5. Khi một đoạn lệnh được khai báo use strict thì tất cả các dòng code ở phía dưới dòng khai báo use strict sẽ được JavaScript quản lý nghiêm ngặt hơn về mặt cú pháp.', N'https://youtu.be/e0Epz3quCMw', 3, 3, 1, NULL)
INSERT [dbo].[Lessons] ([Id], [Title], [Description], [UrlVideo], [ModuleId], [Order], [Status], [IsCompleted]) VALUES (6, N'Cấu trúc code trong Javascript', N'Chương trình là tập hợp của các câu lệnh. Hay nói cách khác, câu lệnh trong JavaScript là đơn vị cơ bản xây dựng nên một chương trình. Vì vậy, câu lệnh là khái niệm đầu tiên mà mình cần nắm vững khi học lập trình JavaScript.', N'https://youtu.be/TJaHlzvY3As', 3, 2, 1, NULL)
INSERT [dbo].[Lessons] ([Id], [Title], [Description], [UrlVideo], [ModuleId], [Order], [Status], [IsCompleted]) VALUES (7, N'Test', N'Test', N'https://youtu.be/0SJE9dYdpps?list=PL_-VfJajZj0VgpFpEVFzS5Z-lkXtBe-x5', 5, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Lessons] OFF
GO
