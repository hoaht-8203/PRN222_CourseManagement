using AntDesign;
using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Org.BouncyCastle.Ocsp;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;
using static CourseManagement.Model.DTOs.ModuleDTO;

namespace BlazorAppSecure.Component.Courses;

public class CourseDetailBase : ComponentBase {
    private readonly JsonSerializerOptions jsonSerializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Inject]
    protected IConfiguration Configuration { get; set; }

    [Inject]
    protected IHttpClientFactory HttpClientFactory { get; set; }

    [Inject]
    protected IMessageService _mess { get; set; }

    [Inject]
    protected ModalService ModelService { get; set; }

    [Inject]
    protected NavigationManager _nav { get; set; }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    protected HttpClient httpClient;
    protected CollapseExpandIconPosition expandIconPosition = CollapseExpandIconPosition.Left;

    [Parameter]
    public string Id { get; set; }

    protected TimeSpan? videoDuration;

    protected bool LoadingCourseDetail = false;
    protected bool LoadingModuleList = false;
    protected bool LoadingLessonList = false;
    protected bool VisibleAddNewModuleDrawer = false;
    protected bool VisibleAddNewLessonDrawer = false;
    protected bool VisibleReorderModulePositionDrawer = false;
    protected bool VisibleReorderLessonPositionDrawer = false;
    protected bool VisibleEditModule = false;
    protected bool VisibleEditLesson = false;
    protected bool visible = true;
    protected bool LoadingUpdateListModule = false;
    protected bool LoadingUpdateListLesson = false;

    protected DetailCourseResponse? CourseDetailModel = null;
    protected DetailCourseResponse.Lesson? CurrentLesson = null;
    protected AddModuleRequest AddModuleRequest = new();
    protected AddLessonRequest AddNewLessonModel = new();
    protected UpdateModuleRequest? UpdateModuleModel;
    protected UpdateLessonRequest UpdateLessonModel = new();

    protected List<SearchModuleResponse> ListModule = [];
    protected List<SearchModuleResponse> ListModulePreview = [];

    protected int ModuleIdToReorderListLesson;
    protected List<SearchLessonResponse>? ListLessonPreview = null;

    protected SearchModuleResponse? _searchModuleResponseDragging;
    protected int? _draggedIndex;
    protected int? _targetIndex;

    protected SearchLessonResponse? _searchLessonResponseDragging;
    protected int? _draggedIndexLesson;
    protected int? _targetIndexLesson;

    protected DetailCourseResponse.Lesson? PreviousLesson = null;
    protected DetailCourseResponse.Lesson? NextLesson = null;

    protected string[] ActiveModuleKeys { get; set; } = new[] { "1" };
    protected int? LastModuleId { get; set; }

    protected List<UploadFileItem> CurrentLessonFiles { get; set; } = new();
    protected List<DocumentResponse> LessonDocuments { get; set; } = new();

    private int? deletingDocumentId;

    protected async Task DeleteDocument(DocumentResponse document) {
        try {
            deletingDocumentId = document.Id;

            // Delete document from database first
            var response = await httpClient.PostAsJsonAsync("/api/Document/remove",
                new RemoveDocumentRequest {
                    LessonId = document.LessonId,
                    File = document.File
                });

            if (!response.IsSuccessStatusCode) {
                _mess.Error("Failed to delete document from database");
                return;
            }

            // If database deletion successful, delete file from MinIO
            var deleteFileResponse = await httpClient.DeleteAsync($"/api/File/{document.File}");
            if (!deleteFileResponse.IsSuccessStatusCode) {
                _mess.Warning("Document removed from database but failed to delete file from storage");
                // Continue anyway since the document is already removed from DB
            }

            // Remove document from local list
            LessonDocuments.RemoveAll(d => d.Id == document.Id);
            _mess.Success("Document deleted successfully");
            StateHasChanged();
        } catch (Exception ex) {
            _mess.Error($"Error deleting document: {ex.Message}");
        } finally {
            deletingDocumentId = null;
            StateHasChanged();
        }
    }

    protected override async Task OnInitializedAsync() {
        httpClient = HttpClientFactory.CreateClient("Auth");

        await FetchCourseDetail(Id);
        if (CourseDetailModel != null) {
            var firstModule = CourseDetailModel.Modules.SingleOrDefault((m) => m.Order == 1);
            if (firstModule != null) {
                var firstLesson = firstModule.Lessons.SingleOrDefault(l => l.Order == 1);
                if (firstLesson != null) {
                    CurrentLesson = firstLesson;
                    UpdateNavigationLessons();
                }
            }
        }

        if (CurrentLesson != null) {
            await LoadLessonDocuments(CurrentLesson.Id);
        }

        await base.OnInitializedAsync();
    }

    protected async Task LoadLessonDocuments(int lessonId) {
        try {
            // Clear list cũ
            LessonDocuments = new List<DocumentResponse>();
            StateHasChanged(); // Cập nhật UI ngay sau khi clear list

            // Kiểm tra CurrentLesson
            if (CurrentLesson == null) return;

            var response = await httpClient.GetAsync($"/api/Document/get-by-lesson/{lessonId}");
            if (response.IsSuccessStatusCode) {
                var documents = await response.Content.ReadFromJsonAsync<List<DocumentResponse>>();
                LessonDocuments = documents ?? new List<DocumentResponse>();
            } else {
                LessonDocuments = new List<DocumentResponse>();
            }
        } catch (Exception ex) {
            await _mess.Error($"Error loading documents: {ex.Message}");
            LessonDocuments = new List<DocumentResponse>();
        } finally {
            StateHasChanged();
        }
    }

    protected async Task OnLessonFileUploadCompleted(UploadInfo fileInfo) {
        if (fileInfo.File != null && fileInfo.File.State == UploadState.Success) {
            try {
                var response = JsonSerializer.Deserialize<Dictionary<string, string>>(fileInfo.File.Response);
                if (response != null && response.ContainsKey("fileName")) {
                    // Chỉ cập nhật UI, không gọi API document
                    var existingFile = CurrentLessonFiles.FirstOrDefault(f => f.FileName == fileInfo.File.FileName);
                    if (existingFile != null && existingFile != fileInfo.File) {
                        CurrentLessonFiles.Remove(existingFile);
                    }
                    _mess.Success("File uploaded successfully");
                    StateHasChanged();
                }
            } catch (Exception ex) {
                CurrentLessonFiles.Remove(fileInfo.File);
                _mess.Error($"Error processing uploaded file: {ex.Message}");
                StateHasChanged();
            }
        }
    }

    protected async Task<bool> OnLessonFileRemoved(UploadFileItem file) {
        if (file.State == UploadState.Success) {
            try {
                var response = JsonSerializer.Deserialize<Dictionary<string, string>>(file.Response);
                if (response != null && response.ContainsKey("fileName")) {
                    var fileName = response["fileName"];

                    var deleteResponse = await httpClient.DeleteAsync($"/api/File/{fileName}");
                    if (!deleteResponse.IsSuccessStatusCode) {
                        _mess.Error("Failed to remove file");
                        return false;
                    }

                    _mess.Success("File removed successfully");
                    return true;
                }
            } catch (Exception ex) {
                _mess.Error($"Error removing file: {ex.Message}");
                return false;
            }
        }
        return true;
    }

    protected async Task DownloadDocument(DocumentResponse document) {
        try {
            var result = await httpClient.GetAsync($"/api/File/url/{document.File}");
            if (result.IsSuccessStatusCode) {
                var urlResponse = await result.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                if (urlResponse != null && urlResponse.ContainsKey("url")) {
                    await JSRuntime.InvokeVoidAsync("window.open", urlResponse["url"], "_blank");
                }
            }
        } catch (Exception ex) {
            await _mess.Error($"Error downloading file: {ex.Message}");
        }
    }

    protected async Task HandleLessonFileUploadChange(UploadInfo args) {
        if (args.File.State == UploadState.Uploading) {
            // Check if file already exists in the list
            var existingFile = CurrentLessonFiles.FirstOrDefault(f => f.FileName == args.File.FileName);
            if (existingFile != null) {
                // Remove the existing file if it's a duplicate
                CurrentLessonFiles.Remove(existingFile);
            }

            // Add the new file
            CurrentLessonFiles.Add(args.File);
            StateHasChanged();
        }
    }

    protected async Task NavigateToPreviousLesson() {
        if (PreviousLesson != null) {
            try {
                CurrentLesson = PreviousLesson;
                UpdateNavigationLessons();
                await LoadLessonDocuments(PreviousLesson.Id);
                EnsureModuleExpanded(PreviousLesson.ModuleId);
                StateHasChanged();
            } catch (Exception) {
            }
        }
    }

    protected async Task NavigateToNextLesson() {
        try {
            if (NextLesson != null) {
                CurrentLesson = NextLesson;
                UpdateNavigationLessons();
                await LoadLessonDocuments(CurrentLesson.Id);
                EnsureModuleExpanded(NextLesson.ModuleId);
                StateHasChanged();
            }
        } catch (Exception) {
        }
    }

    protected void UpdateNavigationLessons() {
        if (CourseDetailModel == null || CurrentLesson == null) return;

        PreviousLesson = null;
        NextLesson = null;

        // Find the current module
        var currentModule = CourseDetailModel.Modules.FirstOrDefault(m => m.Id == CurrentLesson.ModuleId);
        if (currentModule == null) return;

        // Find the index of the current lesson in the current module
        var currentLessonIndex = currentModule.Lessons.FindIndex(l => l.Id == CurrentLesson.Id);
        if (currentLessonIndex == -1) return;

        // Check for previous lesson in the same module
        if (currentLessonIndex > 0) {
            PreviousLesson = currentModule.Lessons[currentLessonIndex - 1];
        }
        // If no previous lesson in the same module, check the previous module
        else {
            var currentModuleIndex = CourseDetailModel.Modules.FindIndex(m => m.Id == currentModule.Id);
            if (currentModuleIndex > 0) {
                var previousModule = CourseDetailModel.Modules[currentModuleIndex - 1];
                if (previousModule.Lessons.Count > 0) {
                    PreviousLesson = previousModule.Lessons[previousModule.Lessons.Count - 1];
                }
            }
        }

        // Check for next lesson in the same module
        if (currentLessonIndex < currentModule.Lessons.Count - 1) {
            NextLesson = currentModule.Lessons[currentLessonIndex + 1];
        }
        // If no next lesson in the same module, check the next module
        else {
            var currentModuleIndex = CourseDetailModel.Modules.FindIndex(m => m.Id == currentModule.Id);
            if (currentModuleIndex < CourseDetailModel.Modules.Count - 1) {
                var nextModule = CourseDetailModel.Modules[currentModuleIndex + 1];
                if (nextModule.Lessons.Count > 0) {
                    NextLesson = nextModule.Lessons[0];
                }
            }
        }
    }

    protected string FormatDuration(TimeSpan duration) {
        if (duration.Hours > 0) {
            return $"{duration.Hours}:{duration.Minutes:D2}:{duration.Seconds:D2}";
        }
        return $"{duration.Minutes}:{duration.Seconds:D2}";
    }

    protected async Task OnVideoUrlChangedAdd() {
        if (!string.IsNullOrEmpty(AddNewLessonModel.UrlVideo)) {
            videoDuration = await GetYouTubeDuration(AddNewLessonModel.UrlVideo);
            AddNewLessonModel.Duration = videoDuration;
            StateHasChanged();
        }
    }

    protected async Task OnVideoUrlChangedUpdate() {
        if (!string.IsNullOrEmpty(UpdateLessonModel.UrlVideo)) {
            videoDuration = await GetYouTubeDuration(UpdateLessonModel.UrlVideo);
            UpdateLessonModel.Duration = videoDuration;
            StateHasChanged();
        }
    }

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

    protected void OnDragStartLesson(DragEventArgs e, SearchLessonResponse searchLessonResponse) {
        if (ListLessonPreview != null && ListLessonPreview.Count != 0) {
            _draggedIndexLesson = ListLessonPreview.IndexOf(searchLessonResponse);
            _searchLessonResponseDragging = searchLessonResponse;
        }
    }

    protected void OnDropLesson(SearchLessonResponse targetLesson) {
        if (ListLessonPreview != null &&
            ListLessonPreview.Count != 0 &&
            _searchLessonResponseDragging != null &&
            _draggedIndexLesson.HasValue) {
            var targetIndex = ListLessonPreview.IndexOf(targetLesson);
            if (targetIndex != -1) {
                var item = ListLessonPreview[_draggedIndexLesson.Value];
                ListLessonPreview.RemoveAt(_draggedIndexLesson.Value);
                ListLessonPreview.Insert(targetIndex, item);

                _searchLessonResponseDragging = null;
                _draggedIndexLesson = null;

                StateHasChanged();
            }
        }
    }

    protected void Callback(string[] keys) {
        ActiveModuleKeys = keys;
        Console.WriteLine(string.Join(',', keys));
    }

    protected async void HandleLessonClick(DetailCourseResponse.Lesson lesson) {
        try {
            CurrentLesson = lesson;
            UpdateNavigationLessons();
            EnsureModuleExpanded(lesson.ModuleId);
            await LoadLessonDocuments(lesson.Id);
            StateHasChanged();
        } catch (Exception ex) {
            await _mess.Error($"Error handling lesson click: {ex.Message}");
        }
    }

    protected void EnsureModuleExpanded(int moduleId) {
        if (LastModuleId != moduleId) {
            // Find the module by ID
            var module = CourseDetailModel?.Modules.FirstOrDefault(m => m.Id == moduleId);
            if (module != null) {
                // Set the active key to the module's order
                ActiveModuleKeys = new[] { module.Order.ToString() };
                LastModuleId = moduleId;
                StateHasChanged();
            }
        }
    }

    protected void handleClose() {
        visible = false;
    }

    protected void OpenAddNewModuleDrawer() {
        AddModuleRequest = new() {
            CourseId = Id,
            Title = "",
        };
        VisibleAddNewModuleDrawer = true;
    }

    protected async Task OpenAddNewLessonDrawer() {
        AddNewLessonModel = new();
        var ListModuleRes = await FetchModuleList(Id);
        if (ListModuleRes != null && ListModuleRes.Count > 0) {
            ListModule = ListModuleRes;
            VisibleAddNewLessonDrawer = true;
        }
    }

    protected async Task ReloadModulePreviewList() {
        var ListModuleRes = await FetchModuleList(Id);
        if (ListModuleRes != null && ListModuleRes.Count > 0) {
            ListModulePreview = ListModuleRes;
        }
    }
    protected async Task ReloadModuleList() {
        var ListModuleRes = await FetchModuleList(Id);
        if (ListModuleRes != null && ListModuleRes.Count > 0) {
            ListModule = ListModuleRes;
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
        ModuleIdToReorderListLesson = 0;
        ListLessonPreview = null;
        var ListModuleRes = await FetchModuleList(Id);
        if (ListModuleRes != null && ListModuleRes.Count > 0) {
            ListModule = ListModuleRes;
            VisibleReorderLessonPositionDrawer = true;
        }
        StateHasChanged();
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

    protected async Task OpenEditLessonDrawer(int lessonId) {
        var lessonDetail = await FetchLessonDetail(lessonId);
        var ListModuleRes = await FetchModuleList(Id);
        if (ListModuleRes != null && ListModuleRes.Count > 0 && lessonDetail != null) {
            ListModule = ListModuleRes;
            UpdateLessonModel = new() {
                Id = lessonDetail.Id,
                Title = lessonDetail.Title,
                Description = lessonDetail.Description,
                UrlVideo = lessonDetail.UrlVideo,
                ModuleId = lessonDetail.ModuleId,
                NewOrder = null,
                Duration = lessonDetail.Duration
            };
            VisibleEditLesson = true;
        }
    }

    protected void CloseAddNewModuleDrawer() {
        VisibleAddNewModuleDrawer = false;
    }

    protected void CloseAddNewLessonDrawer() {
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

    protected void CloseEditLessonDrawer() {
        VisibleEditLesson = false;
    }

    protected async Task OnSelectedModuleChangedHandler(SearchModuleResponse module) {
        if (module != null) {
            var fetchLessonListRes = await FetchLessonList(module.Id);
            if (fetchLessonListRes != null && fetchLessonListRes.Count != 0) {
                ListLessonPreview = fetchLessonListRes;
            } else {
                ListLessonPreview = null;
            }
        }
    }

    protected async Task FetchCourseDetail(string courseId) {
        LoadingCourseDetail = true;
        try {
            var result = await httpClient.GetAsync($"/api/Course/detail?CourseId={courseId}");

            if (result.IsSuccessStatusCode) {
                var response = await result.Content.ReadAsStringAsync();
                var value = JsonSerializer
                    .Deserialize<DetailCourseResponse>(response, jsonSerializerOptions);
                if (value != null) {
                    CourseDetailModel = value;
                }
            } else {
                await _mess.Error(await result.Content.ReadAsStringAsync());
            }
        } catch (Exception ex) {
            await _mess.Error($"Internal server error when load list course: {ex.Message}");
        } finally {
            LoadingCourseDetail = false;
            StateHasChanged();
        }
    }

    protected async Task<List<SearchModuleResponse>?> FetchModuleList(string courseId) {
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

    protected async Task<List<SearchLessonResponse>?> FetchLessonList(int moduleId) {
        LoadingLessonList = true;
        try {
            var result = await httpClient.GetAsync($"/api/Lesson/search?ModuleId={moduleId}");

            if (result.IsSuccessStatusCode) {
                var response = await result.Content.ReadAsStringAsync();
                var values = JsonSerializer
                    .Deserialize<List<SearchLessonResponse>>(response, jsonSerializerOptions);
                if (values != null && values.Count != 0) {
                    return values;
                }
            } else {
                await _mess.Error(await result.Content.ReadAsStringAsync());
            }
        } catch (Exception ex) {
            await _mess.Error($"Internal server error when load list lessons: {ex.Message}");
        } finally {
            LoadingLessonList = false;
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
            await _mess.Error($"Internal server error when load module detail: {ex.Message}");
        }
        return null;
    }

    protected async Task<DetailLessonResponse?> FetchLessonDetail(int lessonId) {
        try {
            var result = await httpClient.GetAsync($"/api/Lesson/detail?LessonId={lessonId}");

            if (result.IsSuccessStatusCode) {
                var response = await result.Content.ReadAsStringAsync();
                var values = JsonSerializer
                    .Deserialize<DetailLessonResponse>(response, jsonSerializerOptions);
                return values;
            } else {
                await _mess.Error(await result.Content.ReadAsStringAsync());
            }
        } catch (Exception ex) {
            await _mess.Error($"Internal server error when load lesson detail: {ex.Message}");
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
            AddNewLessonModel.Duration = await GetYouTubeDuration(AddNewLessonModel.UrlVideo);

            // Thêm thông tin documents vào request
            AddNewLessonModel.Documents = CurrentLessonFiles
                .Where(f => f.State == UploadState.Success)
                .Select(f => {
                    var fileResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(f.Response);
                    return new DocumentInfo {
                        FileName = f.FileName,
                        FileType = f.Type,
                        FileSize = f.Size,
                        MinIOFileName = fileResponse["fileName"]
                    };
                }).ToList();

            var response = await httpClient
                .PostAsJsonAsync("/api/Lesson/add", AddNewLessonModel);

            if (response.IsSuccessStatusCode) {
                CloseAddNewLessonDrawer();
                await FetchCourseDetail(Id);
                CurrentLessonFiles.Clear();
                await _mess.Success("Lesson added successfully");
            } else {
                // Xóa các files đã upload nếu tạo lesson thất bại
                foreach (var file in CurrentLessonFiles.Where(f => f.State == UploadState.Success)) {
                    try {
                        var fileResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(file.Response);
                        if (fileResponse != null && fileResponse.ContainsKey("fileName")) {
                            await httpClient.DeleteAsync($"/api/File/{fileResponse["fileName"]}");
                        }
                    } catch { }
                }
                await _mess.Error("Failed to add lesson");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
    }

    protected async void UpdateModule() {
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Module/update", UpdateModuleModel);

            if (response.IsSuccessStatusCode) {
                CloseEditModuleDrawer();
                var listModuleRes = await FetchModuleList(Id);
                if (listModuleRes != null) {
                    ListModulePreview = listModuleRes;
                }
                await FetchCourseDetail(Id);                
                await _mess.Success("Module updated successfully");
            } else {
                await _mess.Error("Failed to update module");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
    }

    protected async void UpdateLesson() {
        try {
            UpdateLessonModel.Duration = videoDuration;
            var response = await httpClient
                .PostAsJsonAsync("/api/Lesson/update", UpdateLessonModel);

            if (response.IsSuccessStatusCode) {
                CloseEditLessonDrawer();
                var listLessonRes = await FetchLessonList(ModuleIdToReorderListLesson);
                if (listLessonRes != null) {
                    ListLessonPreview = listLessonRes;
                }
                await FetchCourseDetail(Id);
                await _mess.Success("Lesson updated successfully");
            } else {
                await _mess.Error("Failed to update lesson");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
    }

    protected async void ReorderModulePosition() {
        LoadingUpdateListModule = true;
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Module/reorder-modules", new {
                    NewListModules = ListModulePreview,
                    CourseId = Id
                });

            LoadingUpdateListModule = false;

            if (response.IsSuccessStatusCode) {
                CloseReorderModulePostionDrawer();
                await FetchCourseDetail(Id);
                await _mess.Success("List module reorder successfully");
            } else {
                await _mess.Error("Failed to reorder list module");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        } finally {
            LoadingUpdateListModule = false;
            StateHasChanged();
        }
    }

    protected async void ReorderLessonPosition() {
        LoadingUpdateListLesson = true;
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Lesson/reorder-lessons", new {
                    NewListLessons = ListLessonPreview,
                    ModuleId = ModuleIdToReorderListLesson
                });

            LoadingUpdateListLesson = false;
            if (response.IsSuccessStatusCode) {
                CloseReorderLessonPositionDrawer();
                await FetchCourseDetail(Id);
                await _mess.Success("List lesson reorder successfully");
            } else {
                await _mess.Error("Failed to reorder list lesson");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        } finally {
            LoadingUpdateListLesson = false;
        }
    }

    protected async void DeleteModule(int id) {
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Module/remove", new RemoveModuleRequest() {
                    ModuleId = id
                });

            if (response.IsSuccessStatusCode) {
                var listModuleRes = await FetchModuleList(Id);
                if (listModuleRes != null) {
                    ListModulePreview = listModuleRes;
                } else {
                    ListModulePreview = [];
                }
                    await FetchCourseDetail(Id);
                StateHasChanged();
                await _mess.Success("Module removed successfully");
            } else {
                await _mess.Error("Failed to remove module");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
    }

    protected async void DeleteLesson(int id, int moduleId) {
        try {
            var response = await httpClient
                .PostAsJsonAsync("/api/Lesson/remove", new RemoveLessonRequest() {
                    LessonId = id
                });

            if (response.IsSuccessStatusCode) {
                var listLessonRes = await FetchLessonList(moduleId);
                if (listLessonRes != null) {
                    ListLessonPreview = listLessonRes;
                } else {
                    ListLessonPreview = [];
                }
                await FetchCourseDetail(Id);
                StateHasChanged();
                await _mess.Success("Lesson removed successfully");
            } else {
                await _mess.Error("Failed to remove lesson");
            }
        } catch (Exception ex) {
            await _mess.Error($"Error: {ex.Message}");
        }
    }

    protected async Task<TimeSpan?> GetYouTubeDuration(string url) {
        try {
            string videoId = GetYouTubeVideoId(url);
            if (string.IsNullOrEmpty(videoId)) return null;

            string apiKey = Configuration["YouTube:ApiKey"];
            string apiUrl = $"https://www.googleapis.com/youtube/v3/videos?id={videoId}&part=contentDetails&key={apiKey}";

            // Tạo HttpClient mới không có authentication header
            using (var client = new HttpClient()) {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    using JsonDocument document = JsonDocument.Parse(content);
                    var items = document.RootElement.GetProperty("items");

                    if (items.GetArrayLength() > 0) {
                        var duration = items[0].GetProperty("contentDetails").GetProperty("duration").GetString();
                        if (duration != null) {
                            return System.Xml.XmlConvert.ToTimeSpan(duration);
                        }
                    }
                } else {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    await _mess.Error($"YouTube API error: {errorContent}");
                }
            }
        } catch (Exception ex) {
            await _mess.Error($"Error getting video duration: {ex.Message}");
        }
        return null;
    }

    protected string GetYouTubeVideoId(string url) {
        if (string.IsNullOrEmpty(url)) return "";

        if (url.Contains("youtube.com/watch")) {
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            return query["v"] ?? "";
        } else if (url.Contains("youtu.be")) {
            return url.Split('/').Last();
        } else if (url.Length == 11) {
            return url;
        }
        return "";
    }

    protected string GetYouTubeEmbedUrl(string url) {
        if (string.IsNullOrEmpty(url)) return "";

        string videoId = "";

        if (url.Contains("youtube.com/watch")) {
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            videoId = query["v"];
        } else if (url.Contains("youtu.be")) {
            videoId = url.Split('/').Last();
        } else if (url.Length == 11) {
            videoId = url;
        }

        return $"https://www.youtube.com/embed/{videoId}";
    }
    protected async Task ExportLessonsToExcel()
    {
        if (CourseDetailModel == null || CourseDetailModel.Modules == null)
        {
            await _mess.Warning("No data available to export.");
            return;
        }

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Lessons");

        // Tiêu đề cột
        worksheet.Cells[1, 1].Value = "Module Title";
        worksheet.Cells[1, 2].Value = "Lesson Title";
        worksheet.Cells[1, 3].Value = "Video URL";

        using (var range = worksheet.Cells[1, 1, 1, 3])
        {
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }

        int row = 2; // Bắt đầu ghi dữ liệu từ dòng 2
        foreach (var module in CourseDetailModel.Modules)
        {
            int startRow = row;
            if (module.Lessons != null && module.Lessons.Any())
            {
                // Ghi tất cả bài học của module
                foreach (var lesson in module.Lessons)
                {
                    worksheet.Cells[row, 1].Value = module.Title;
                    worksheet.Cells[row, 2].Value = lesson.Title;
                    worksheet.Cells[row, 3].Value = lesson.UrlVideo;
                    row++;
                }
            }
            else
            {
                // Module không có bài học
                worksheet.Cells[row, 1].Value = module.Title;
                worksheet.Cells[row, 2].Value = "Không có lesson";
                worksheet.Cells[row, 3].Value = "";
                row++;
            }

            // Merge các ô trong cột Module Title nếu có nhiều dòng
            if (row - startRow > 1)
            {
                worksheet.Cells[startRow, 1, row - 1, 1].Merge = true;
                worksheet.Cells[startRow, 1, row - 1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        var stream = new MemoryStream();
        await package.SaveAsAsync(stream);
        var fileBytes = stream.ToArray();
        var fileBase64 = Convert.ToBase64String(fileBytes);

        var fileName = $"Course_{Id}_Lessons.xlsx";
        await JSRuntime.InvokeVoidAsync("saveAsFile", fileName, fileBase64);
    }

}

public static class Extensions {
    public static string ToFileSize(this long bytes) {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1) {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}