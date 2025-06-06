﻿using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Model.DTOs {
    public class AddLessonRequest {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UrlVideo { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Module is required")]
        public int ModuleId { get; set; }
        public TimeSpan? Duration { get; set; }
        // Thêm property mới
        public List<DocumentInfo> Documents { get; set; } = new();
    }

    public class DocumentInfo {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string MinIOFileName { get; set; }
    }

    public class SearchLessonRequest {
        public int ModuleId { get; set; }
    }

    public class SearchLessonResponse {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlVideo { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public int ModuleId { get; set; }
        public ModuleDetail Module { get; set; }

        public class ModuleDetail {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Order { get; set; }
            public int Status { get; set; }
            public string CourseId { get; set; }
        }
    }

    public class UpdateLessonRequest {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UrlVideo { get; set; }
        public int? NewOrder { get; set; }
        [Required]
        public int ModuleId { get; set; }
        public TimeSpan? Duration { get; set; }
    }

    public class RemoveLessonRequest {
        [Required]
        public int LessonId { get; set; }
    }

    public class ReorderLessonsRequest {
        [Required]
        public int ModuleId { get; set; }
        [Required]
        public List<SearchLessonResponse> NewListLessons { get; set; }
    }

    public class DetailLessonRequest {
        [Required]
        public int LessonId { get; set; }
    }

    public class DetailLessonResponse {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlVideo { get; set; }
        public int? Order { get; set; }
        public TimeSpan? Duration { get; set; }
        public int Status { get; set; }

        public int ModuleId { get; set; }
    }

    public class GetLessonsCompletedRequest {
        [Required]
        public string CourseId { get; set; }
    }

    public class GetLessonsCompletedResponse {
        [Required]
        public HashSet<int> ListLessonId { get; set; }
    }

    public class CompletedLessonRequest {
        [Required]
        public int LessonId { get; set; }
    }

    public class NotCompletedLessonRequest {
        [Required]
        public int LessonId { get; set; }
    }

    public class UpdateLastViewedRequest {
        [Required]
        public string CourseId { get; set; }
        [Required]
        public int LessonId { get; set; }
    }

    public class GetLastViewedRequest {
        [Required]
        public string CourseId { get; set; }
    }

    public class GetLastViewedResponse {
        public int? LastViewedLessonId { get; set; }
    }

    public class AddNoteRequest {
        [Required]
        public int LessonId { get; set; }

        [Required]
        public string Content { get; set; }
    }

    public class GetNotesRequest {
        [Required]
        public int LessonId { get; set; }
    }

    public class NoteResponse {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class GetNotesResponse {
        public List<NoteResponse> Notes { get; set; }
    }

    public class RemoveNoteRequest {
        [Required]
        public int NoteId { get; set; }
    }
}
