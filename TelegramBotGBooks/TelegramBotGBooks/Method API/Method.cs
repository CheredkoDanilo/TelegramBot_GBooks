using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotGBooks.Model;

namespace TelegramBotGBooks.Method_API
{
    public class Method
    {
        private HttpClient _client;
        private static string _address = "https://apigbook.azure-api.net/v1/";
        private static string _kay = "71a8b5268d3d4972bfade2da8a17ed74";
        public Method()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }
        public async Task IdBooks(ITelegramBotClient botClient, Message message, string Id)
        {
            var respons = await _client.GetAsync($"BookId?id={Id}&key={_kay}");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<AudioBook>(content);

            if (result.Book.volumeInfo != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Назва книги: {result.Book.volumeInfo.title}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Id книги: {result.Book.id}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Автори книги:");
                foreach (string i in result.Book.volumeInfo.authors)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"{i}");
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Кількість сторінок книжки: {result.Book.volumeInfo.pageCount}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Рейтинг зрілості книжки: {result.Book.volumeInfo.maturityRating}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Версія книжки: {result.Book.volumeInfo.contentVersion}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Мова на якій написана книжка: {result.Book.volumeInfo.language}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Посилання на веб-читання книжки: {result.Book.accessInfo.webReaderLink}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Назва аудіо версії книжки в ютубі {result.YouTube.items[0].snippet.title}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"назва каналу який опублікував аудіо версію книги {result.YouTube.items[0].snippet.channelTitle}");

            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Книги з таким Id нема");

            }
        }

        public async Task NameBooks(ITelegramBotClient botClient, Message message, string name)
        {
            var respons = await _client.GetAsync($"Book?name={name}&key={_kay}");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Books>(content);
            if (result != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Кількість книг які найдено: {result.totalItems}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Назва першої книги з списку: {result.items[0].volumeInfo.title}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Id книги: {result.items[0].id}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Автори книги:");
                foreach (string i in result.items[1].volumeInfo.authors)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"{i}");
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Кількість сторінок книжки: {result.items[0].volumeInfo.pageCount}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Посилання на веб-читання книжки: {result.items[0].accessInfo.webReaderLink}");
            }
            else
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Книги з такою назвою нема");
        }
        public async Task AuthorNameBooks(ITelegramBotClient botClient, Message message, string name, string author)
        {
            var respons = await _client.GetAsync($"AuthorBook?name={name}&author={author}&key={_kay}");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Books>(content);
            if (result != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Кількість книг які найдено: {result.totalItems}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Назва першої книги з списку: {result.items[0].volumeInfo.title}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Id книги: {result.items[0].id}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Автори книги:");
                if (author != null)
                    foreach (string i in result.items[0].volumeInfo.authors)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{i}");
                    }
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Кількість сторінок книжки: {result.items[0].volumeInfo.pageCount}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Посилання на веб-читання книжки: {result.items[0].accessInfo.webReaderLink}");
            }
            else
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Книги з такою назвою нема");

        }
        //public async Task Authorization(ITelegramBotClient botClient, Message message)
        //{
        //    var respons = await _client.GetAsync($"Authorizations?key={_kay}");
        //    var content = respons.Content.ReadAsStringAsync().Result;
        //    var result = content;
        //    await botClient.SendTextMessageAsync(message.Chat.Id, $"{result}");
        //}
        //public async Task Revoke(ITelegramBotClient botClient, Message message)
        //{
        //    var respons = await _client.GetAsync($"Revoke?key={_kay}");
        //    var content = respons.Content.ReadAsStringAsync().Result;
        //    var result = content;
        //    await botClient.SendTextMessageAsync(message.Chat.Id, $"{result}");
        //}
        //public async Task ListShelf(ITelegramBotClient botClient, Message message, string shelf)
        //{
        //    var respons = await _client.GetAsync($"Shelf/ListShelf?shealf={shelf}&key={_kay}");
        //    var content = respons.Content.ReadAsStringAsync().Result;
        //    var result = JsonConvert.DeserializeObject<List<string>>(content);
        //    await botClient.SendTextMessageAsync(message.Chat.Id, $"Книги з {shelf}-ї полиці:");
        //    foreach (var item in result)
        //    {
        //        await botClient.SendTextMessageAsync(message.Chat.Id, $"{item}");
        //    }
        //}
        //public async Task AddBookShelf(ITelegramBotClient botClient, Message message, string shelf, string id)
        //{
        //    var respons = await _client.PostAsJsonAsync($"Shelf/AddBookShelf?shealf={shelf}&key={_kay}", id);
        //    var content = respons.Content.ReadAsStringAsync().Result;
        //    var result = content;
        //    await botClient.SendTextMessageAsync(message.Chat.Id, $"{result}");
        //}
        //public async Task DeleteBookShelf(ITelegramBotClient botClient, Message message, string shelf, string id)
        //{
        //    var respons = await _client.DeleteAsync($"Shelf/DeleteBookShelf?shealf={shelf}&id={id}&key={_kay}");
        //    var content = respons.Content.ReadAsStringAsync().Result;
        //    var result = content;
        //    await botClient.SendTextMessageAsync(message.Chat.Id, $"{result}");
        //}
        public async Task GetAllBookDb(ITelegramBotClient botClient, Message message)
        {
            var respons = await _client.GetAsync($"GetAllDynamoDb?key={_kay}");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<BookDb>>(content);
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Збережені книги:");
            foreach (var item in result)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Нзава книги {item.title}, Id книги {item.Id}, автор книги {item.authors}");
            }
        }
        public async Task GetBookIdDb(ITelegramBotClient botClient, Message message, string id)
        {
            var respons = await _client.GetAsync($"GetDynamoDb?Id={id}&key={_kay}");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<BookDb>(content);
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Назва книги:{result.title}");
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Автор книги:{result.authors}");
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Версія книги:{result.contentVersion}");
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Категорія книги:{result.categories}");
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Дата публікації книги:{result.publishedDate}");
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Кількість сторінок книги:{result.printedPageCount}");
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Рейтинг зрілості книги:{result.maturityRating}");
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Канонічне послання на книгу:{result.canonicalVolumeLink}");
        }
        public async Task DeleteBookDb(ITelegramBotClient botClient, Message message, string id)
        {
            await _client.DeleteAsync($"DeleteDynamoDb?Id={id}&key={_kay}");
        }
    }
}
