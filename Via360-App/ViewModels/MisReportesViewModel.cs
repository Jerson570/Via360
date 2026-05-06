using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Via360.App.Services;
using Via360.Shared.Models; // Ajusta según tu namespace

public partial class MisReportesViewModel : ObservableObject
{
    private readonly ApiService _apiService;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private bool sinReportes;

    public ObservableCollection<Reporte> Reportes { get; } = new();

    public MisReportesViewModel(ApiService apiService, IAuthService authService)
    {
        _apiService = apiService;
        _authService = authService;
    }

    [RelayCommand]
    public async Task CargarReportes()
    {
        IsRefreshing = true;
        SinReportes = false;

        try
        {
            // Obtenemos el UID real del servicio de Auth
            string uid = await _authService.GetActualUserId();

            var lista = await _apiService.ObtenerMisReportes(uid);

            Reportes.Clear();
            if (lista != null && lista.Any())
            {
                foreach (var reporte in lista)
                {
                    Reportes.Add(reporte);
                }
            }
            else
            {
                SinReportes = true;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "No pudimos conectar con el servidor", "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}