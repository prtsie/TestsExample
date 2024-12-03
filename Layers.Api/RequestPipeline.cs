namespace TestsExample;

public static class RequestPipeline
{
    /// <summary>
    /// Настраивает конвейер обработки запросов (middleware)
    /// </summary>
    public static void ConfigureRequestPipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            //Если при обработке запроса возникнет исключение, произойдет редирект на эту страницу
            app.UseExceptionHandler("/Home/Error"); 
            
            app.UseHsts();
        }

        //Заставить пользователя использовать https вместо http
        app.UseHttpsRedirection(); 
        
        //Дать пользователю возможность свободно обращаться к файлам в папке wwwroot
        //В этой папке хранятся редко изменяющиеся ресурсы, например, лого сайта, картинки, таблицы стилей css и js-скрипты
        app.UseStaticFiles(); 
        
        //Сопоставлять url запроса с обработчиками запросов (в данном случае это контроллеры)
        app.UseRouting(); 
        
        //Подключение middleware для аутентификации, которое будет автоматически возвращать пользователю 401 код
        //и не пускать к эндпоинтам с атрибутом [Authorize] (например, к эндпоинту CreatePost в HomeController-е)
        app.UseAuthentication();
        app.UseAuthorization();

        //Подключить контроллеры и настроить сопоставление url запроса и контроллера
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=TheWall}/{id?}");
    }
}