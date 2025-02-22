## Hướng dẫn chạy UI và API cùng lúc trong Visual Studio

<img src="images/Buoc1.png" alt="Hướng dẫn chạy UI và API cùng lúc trong Visual Studio" style="width: 100%;" />
<img src="images/Buoc2.png" alt="Hướng dẫn chạy UI và API cùng lúc trong Visual Studio" style="width: 100%;" />
<img src="images/Buoc3.png" alt="Hướng dẫn chạy UI và API cùng lúc trong Visual Studio" style="width: 100%;" />

## Lỗi khi chạy Blazor app

1. Install the required .NET workloads:

```bash
# Restore all required workloads
dotnet workload restore

# If above command doesn't work, try installing specific workloads:
dotnet workload install wasm-tools
dotnet workload install wasm-tools-net8
```
