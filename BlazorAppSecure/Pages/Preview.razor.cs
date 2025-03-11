using AntDesign;
using BlazorAppSecure.Model;
using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorAppSecure.Pages;

public class PreviewBase : ComponentBase
{
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Inject]
    protected IHttpClientFactory HttpClientFactory { get; set; }

    [Inject]
    protected IMessageService _mess { get; set; }
    [Inject]
    protected NavigationManager nav { get; set; }

    [Parameter]
    public string Id { get; set; }

    protected HttpClient httpClient;
    protected PreviewCourseResponse CourseDetail { get; set; }
    protected bool LoadingCourseDetail = false;
    protected string[] ActivePanelKeys { get; set; } = new[] { "1" };

    protected override async Task OnInitializedAsync()
    {
        httpClient = HttpClientFactory.CreateClient("Auth");
        await FetchCourseDetail(Id);
        await base.OnInitializedAsync();
    }

    protected async Task FetchCourseDetail(string courseId)
    {
        LoadingCourseDetail = true;
        try
        {
            var result = await httpClient.GetAsync($"/api/Course/preview?CourseId={courseId}");

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var value = JsonSerializer.Deserialize<PreviewCourseResponse>(response, jsonSerializerOptions);
                if (value != null)
                {
                    CourseDetail = value;
                }
            }
            else
            {
                await _mess.Error(await result.Content.ReadAsStringAsync());
            }
        }
        catch (Exception ex)
        {
            await _mess.Error($"Internal server error when load course detail: {ex.Message}");
        }
        finally
        {
            LoadingCourseDetail = false;
            StateHasChanged();
        }
    }

    protected async Task HandleEnrollCourse(string courseId)  // Đổi thành Task thay vì void
    {
        if (CourseDetail.IsEnrolled) {
            nav.NavigateTo($"/learning/{courseId}");
        }

        try {
            JoinCourseRequest req = new() {
                CourseId = courseId
            };

            var response = await httpClient.PostAsJsonAsync("/api/Course/enroll-course", req);

            if (response.IsSuccessStatusCode) {
                nav.NavigateTo($"/learning/{courseId}");
            } else {
                var error = await response.Content.ReadAsStringAsync();
                await _mess.Error($"Failed to enroll this course: {error}");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
    }


    protected string GetTotalDuration()
    {
        if (CourseDetail?.Modules == null) return "00:00";

        var totalDuration = TimeSpan.Zero;
        foreach (var module in CourseDetail.Modules)
        {
            foreach (var lesson in module.Lessons)
            {
                if (lesson.VideoDuration.HasValue)
                {
                    totalDuration += lesson.VideoDuration.Value;
                }
            }
        }

        return FormatDuration(totalDuration);
    }

    protected string FormatDuration(TimeSpan duration)
    {
        if (duration.Hours > 0)
        {
            return $"{duration.Hours:D2} giờ {duration.Minutes:D2} phút";
        }
        return $"{duration.Minutes:D2} phút";
    }

    protected int GetTotalLessons()
    {
        if (CourseDetail?.Modules == null) return 0;
        return CourseDetail.Modules.Sum(m => m.Lessons.Count);
    }

    protected string GetYouTubeEmbedUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return "";

        string videoId = "";

        if (url.Contains("youtube.com/watch"))
        {
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            videoId = query["v"];
        }
        else if (url.Contains("youtu.be"))
        {
            videoId = url.Split('/').Last();
        }
        else if (url.Length == 11)
        {
            videoId = url;
        }

        return $"https://www.youtube.com/embed/{videoId}";
    }
} 