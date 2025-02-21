using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using BlazorAppSecure.Model;
using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
    protected bool VisibleReorderModulePositionDrawer = false;
    protected bool VisibleReorderLessonPositionDrawer = false;
    protected bool VisibleEditModule = false;
    protected bool visible = true;

    protected DetailCourseResponse? CourseDetailModel = null;
    protected DetailCourseResponse.Lesson? CurrentLesson = null;
    protected AddModuleRequest AddModuleRequest = new();
    protected AddLessonRequest AddNewLessonModel = new();
    protected UpdateModuleRequest? UpdateModuleModel;

    protected List<SearchModuleResponse> ListModule = [];
    protected List<SearchModuleResponse> ListModulePreview = [];

    protected SearchModuleResponse? _searchModuleResponseDragging;
    protected int? _draggedIndex;
    protected int? _targetIndex;

    protected void OnDragStart(DragEventArgs e, SearchModuleResponse searchModuleResponse) {
        _draggedIndex = ListModulePreview.IndexOf(searchModuleResponse);
        _searchModuleResponseDragging = searchModuleResponse;
    }

    protected void OnDrop(SearchModuleResponse targetModule) {
        if (_searchModuleResponseDragging != null && _draggedIndex.HasValue) {
            var targetIndex = ListModulePreview.IndexOf(targetModule);
            if (targetIndex != -1) {
                var item = ListModulePreview[_draggedIndex.Value];
                ListModulePreview.RemoveAt(_draggedIndex.Value);
                ListModulePreview.Insert(targetIndex, item);

                _searchModuleResponseDragging = null;
                _draggedIndex = null;

                StateHasChanged();
            }
        }
    }

    protected void OnDrop(DragEventArgs e, SearchModuleResponse searchModuleResponse) {
        if (_searchModuleResponseDragging != null && _draggedIndex.HasValue && _targetIndex.HasValue) {
            var item = ListModulePreview[_draggedIndex.Value];
            ListModulePreview.RemoveAt(_draggedIndex.Value);
            ListModulePreview.Insert(_targetIndex.Value, item);

            // Reset drag state
            _searchModuleResponseDragging = null;
            _draggedIndex = null;
            _targetIndex = null;

            StateHasChanged();
        }
    }

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
            Title = "",
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
    protected async Task OpenReorderModulePositionDrawer() {
        VisibleReorderModulePositionDrawer = true;
        var listModulePreview = await FetchModuleList(Id);
        if (listModulePreview != null) {
            ListModulePreview = listModulePreview;
        }
    }

    protected async Task OpenReorderLessonPositionDrawer() {
        VisibleReorderLessonPositionDrawer = true;
    }

    protected async Task OpenEditModuleDrawer(int moduleId) {
        VisibleEditModule = true;
        var moduleDetail = await FetchModuleDetail(moduleId);
        if (moduleDetail != null) {
            UpdateModuleModel = new() {
                ModuleId = moduleDetail.Id,
                Title = moduleDetail.Title,
                NewOrder = null
            };
        }
    }

    protected void CloseAddNewModuleDrawer()
    {
        VisibleAddNewModuleDrawer = false;
    }

    protected void CloseAddNewLessonDrawer()
    {
        VisibleAddNewLessonDrawer = false;
    }

    protected void CloseReorderModulePostionDrawer() {
        VisibleReorderModulePositionDrawer = false;
    }

    protected void CloseReorderLessonPositionDrawer() {
        VisibleReorderLessonPositionDrawer = false;
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

    protected async Task<DetailModuleResponse?> FetchModuleDetail(int moduleId) {
        try {
            var result = await httpClient.GetAsync($"/api/Module/detail?ModuleId={moduleId}");

            if (result.IsSuccessStatusCode) {
                var response = await result.Content.ReadAsStringAsync();
                var values = JsonSerializer
                    .Deserialize<DetailModuleResponse>(response, jsonSerializerOptions);
                if (values != null) {
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

    protected async void UpdateModule() {
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

    protected async void ReorderModulePosition() {
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Module/reorder-modules", new {
                    NewListModules = ListModulePreview,
                    CourseId = Id
                });

            if (response.IsSuccessStatusCode) {
                CloseReorderModulePostionDrawer();
                await FetchCourseDetail(Id);
                await _mess.Success("List module reorder successfully");
            } else {
                await _mess.Error("Failed to reorder list module");
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