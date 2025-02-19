using System.Net.Http.Json;
using System.Text.Json;
using AntDesign;
using BlazorAppSecure.Model;
using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.Components;
using static CourseManagement.Model.DTOs.ModuleDTO;

namespace BlazorAppSecure.Component.Courses;

public class CourseDetailBase : ComponentBase
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
    protected ModalService ModelService { get; set; }

    [Inject]
    protected NavigationManager _nav { get; set; }

    protected HttpClient httpClient;
    protected CollapseExpandIconPosition expandIconPosition = CollapseExpandIconPosition.Left;

    [Parameter]
    public string Id { get; set; }

    protected bool LoadingCourseDetail = false;
    protected bool LoadingModuleList = false;
    protected bool VisibleAddNewModuleDrawer = false;
    protected bool VisibleAddNewLessonDrawer = false;
    protected bool VisibleEditModule = false;
    protected bool visible = true;

    protected DetailCourseResponse? CourseDetailModel = null;
    protected DetailCourseResponse.Lesson? CurrentLesson = null;
    protected AddModuleRequest AddModuleRequest = new();
    protected AddLessonRequest AddNewLessonModel = new();
    protected UpdateModuleRequest UpdateModuleModel = new();

    protected List<SearchModuleResponse> ListModule = [];

    protected void Callback(string[] keys)
    {
        Console.WriteLine(string.Join(',', keys));
    }

    protected void HandleLessonClick(DetailCourseResponse.Lesson lesson)
    {
        CurrentLesson = lesson;
    }

    protected void handleClose() {
        visible = false;
    }

    protected void OpenAddNewModuleDrawer()
    {
        AddModuleRequest = new()
        {
            CourseId = Id,
            Title = String.Empty,
        };
        VisibleAddNewModuleDrawer = true;
    }

    protected async Task OpenAddNewLessonDrawer()
    {
        AddNewLessonModel = new();
        var ListModuleRes = await FetchModuleList(Id);
        if (ListModuleRes != null && ListModuleRes.Count > 0) {
            ListModule = ListModuleRes;
            VisibleAddNewLessonDrawer = true;
        }
    }

    protected void OpenEditModuleDrawer() {
        VisibleEditModule = true;
        UpdateModuleModel = new();
    }

    protected void CloseAddNewModuleDrawer()
    {
        VisibleAddNewModuleDrawer = false;
    }

    protected void CloseAddNewLessonDrawer()
    {
        VisibleAddNewLessonDrawer = false;
    }

    protected void CloseEditModuleDrawer() {
        VisibleEditModule = false;
    }

    protected override async Task OnInitializedAsync()
    {
        httpClient = HttpClientFactory.CreateClient("Auth");

        await FetchCourseDetail(Id);
        if (CourseDetailModel != null)
        {
            var firstModule = CourseDetailModel.Modules.SingleOrDefault((m) => m.Order == 1);
            if (firstModule != null)
            {
                var firstLesson = firstModule.Lessons.SingleOrDefault(l => l.Order == 1);
                if (firstLesson != null)
                {
                    CurrentLesson = firstLesson;
                }
            }
        }

        await base.OnInitializedAsync();
    }

    protected async Task FetchCourseDetail(string courseId)
    {
        LoadingCourseDetail = true;
        try
        {
            var result = await httpClient.GetAsync($"/api/Course/detail?CourseId={courseId}");

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var value = JsonSerializer
                    .Deserialize<DetailCourseResponse>(response, jsonSerializerOptions);
                if (value != null)
                {
                    CourseDetailModel = value;
                }
            }
            else
            {
                await _mess.Error(await result.Content.ReadAsStringAsync());
            }
        }
        catch (Exception ex)
        {
            await _mess.Error($"Internal server error when load list course: {ex.Message}");
        }
        finally
        {
            LoadingCourseDetail = false;
            StateHasChanged();
        }
    }

    protected async Task<List<SearchModuleResponse>?> FetchModuleList(string courseId) 
    {
        LoadingModuleList = true;
        try {
            var result = await httpClient.GetAsync($"/api/Module/search?CourseId={courseId}");

            if (result.IsSuccessStatusCode) {
                var response = await result.Content.ReadAsStringAsync();
                var values = JsonSerializer
                    .Deserialize<List<SearchModuleResponse>>(response, jsonSerializerOptions);
                if (values != null && values.Count != 0) {
                    return values;
                } else {
                    await _mess.Error("Error: List module is null, please add module to this course first");
                    return null;
                }
            } else {
                await _mess.Error(await result.Content.ReadAsStringAsync());
            }
        } catch (Exception ex) {
            await _mess.Error($"Internal server error when load list course: {ex.Message}");
        } finally {
            LoadingModuleList = false;
        }
        return null;
    }

    protected async void SubmitNewModule() {
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Module/add", AddModuleRequest);

            if (response.IsSuccessStatusCode) {
                CloseAddNewModuleDrawer();
                await FetchCourseDetail(Id);
                await _mess.Success("Module added successfully");
            } else {
                await _mess.Error("Failed to add module");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
    }

    protected async void SubmitNewLesson() {
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Lesson/add", AddNewLessonModel);

            if (response.IsSuccessStatusCode) {
                CloseAddNewLessonDrawer();
                await FetchCourseDetail(Id);
                await _mess.Success("Lesson added successfully");
            } else {
                await _mess.Error("Failed to add lesson");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
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