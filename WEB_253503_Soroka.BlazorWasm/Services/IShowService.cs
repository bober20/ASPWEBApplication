using WEB_253503_Soroka.Domain.Entities;

namespace WEB_253503_Soroka.BlazorWasm.Services;

public interface IShowService
{
// Событие, генерируемое при изменении данных
    event Action DataLoaded;

// Список категорий объектов
    List<Genre> Genres { get; set; }

//Список объектов
    List<Show> Shows { get; set; }

// Признак успешного ответа на запрос к Api
    bool Success { get; set; }

// Сообщение об ошибке
    string ErrorMessage { get; set; }

// Количество страниц списка
    int TotalPages { get; set; }

// Номер текущей страницы
    int CurrentPage { get; set; }

// Фильтр по категории
    Genre SelectedGenre { get; set; }

    public Task GetShowListAsync(int pageNo = 1);

    public Task GetGenreListAsync();
}