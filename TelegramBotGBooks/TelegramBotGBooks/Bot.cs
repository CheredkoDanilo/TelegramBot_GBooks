using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotGBooks.Method_API;

namespace TelegramBotGBooks
{
    public class Bot
    {
        TelegramBotClient botClient = new TelegramBotClient("5568838847:AAEf4Ql4QfL_XL7oSRNRgmTR36-YcKr6buw");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        string CMessage;
        Message PMessage;
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var BotMe = await botClient.GetMeAsync();
            Console.WriteLine($"Бот {BotMe.Username} почав працювати");
            Console.ReadKey();
        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErorMassage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в телеграм бот API:\n {apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErorMassage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessage(botClient, CMessage, PMessage, update.Message);
                if (update.Message.Text == "IdBooks" || update.Message.Text == "NameBooks" || update.Message.Text == "AuthorNameBooks" || update.Message.Text == "ListShelf" || update.Message.Text == "AddBookShelf" || update.Message.Text == "DeleteBookShelf" || update.Message.Text == "GetBooksIdDb" || update.Message.Text == "DeleteBooksIdDb")
                    CMessage = update.Message.Text;
                PMessage = update.Message;
            }
        }

        private async Task HandlerMessage(ITelegramBotClient botClient, string cmessage, Message pmessage, Message message)
        {
            Method method = new Method();
            if (message.Text == "/start")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                    new[]
                    {
                        new KeyboardButton [] { "IdBooks", "NameBooks", "AuthorNameBooks" },
                        //new KeyboardButton [] { "Autorithation Google Book", "Revoke" },
                        //new KeyboardButton [] { "ListShelf", "AddBookShelf", "DeleteBookShelf" },
                        new KeyboardButton [] { "GetAllBooksDb", "GetBooksIdDb", "DeleteBooksIdDb" }
                    }
                    )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть команду:", replyMarkup: replyKeyboardMarkup);
                return;
            }

            if (message.Text == "IdBooks")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть Id книги");
                return;
            }

            if (message.Text == "NameBooks")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть назву книги");
                return;
            }

            if (message.Text == "AuthorNameBooks")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть назву книги, після чого введіть автора книги");
                return;
            }

            //if (message.Text == "ListShelf")
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть номер полиці");
            //    return;
            //}

            //if (message.Text == "AddBookShelf")
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть номер полиці, після чого введіть id книги");
            //    return;
            //}

            //if (message.Text == "DeleteBookShelf")
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть номер полиці, після чого введіть id книги");
            //    return;
            //}

            if (message.Text == "GetBooksIdDb")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть id книги");
                return;
            }
            if (message.Text == "DeleteBooksIdDb")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть id книги");
                return;
            }
            if (message.Text == "GetAllBooksDb")
            {
                await method.GetAllBookDb(botClient, message);
                return;
            }
            //if (message.Text == "Autorithation Google Book")
            //{
            //    method.Authorization(botClient, message);
            //    return;
            //}

            //if (message.Text == "Revoke")
            //{
            //    method.Revoke(botClient, message);
            //    return;
            //}

            if (cmessage == "IdBooks" && message.Text != "IdBooks")
            {
                await method.IdBooks(botClient, message, message.Text);
                return;
            }

            if (cmessage == "NameBooks" && message.Text != "NameBooks")
            {
                await method.NameBooks(botClient, message, message.Text);
                return;
            }

            if (cmessage == "AuthorNameBooks" && pmessage.Text != "AuthorNameBooks" && message.Text != "AuthorNameBooks")
            {
                await method.AuthorNameBooks(botClient, message, pmessage.Text, message.Text);
                message.Text = "AuthorNameBooks";
                return;
            }

            //if (cmessage == "ListShelf" && message.Text != "ListShelf")
            //{
            //    method.ListShelf(botClient, message, message.Text);
            //    return;
            //}

            //if (cmessage == "AddBookShelf" && pmessage.Text != "AddBookShelf" && message.Text != "AddBookShelf")
            //{
            //    method.AddBookShelf(botClient, message, pmessage.Text, message.Text);
            //    return;
            //}

            //if (cmessage == "DeleteBookShelf" && pmessage.Text != "DeleteBookShelf" && message.Text != "DeleteBookShelf")
            //{
            //    method.DeleteBookShelf(botClient, message, pmessage.Text, message.Text);
            //    return;
            //}
            if (cmessage == "GetBooksIdDb" && message.Text != "GetBooksIdDb")
            {
                await method.GetBookIdDb(botClient, message, message.Text);
                return;
            }
            if (cmessage == "DeleteBooksIdDb" && message.Text != "DeleteBooksIdDb")
            {
                await method.DeleteBookDb(botClient, message, message.Text);
                return;
            }
        }

    }
}
