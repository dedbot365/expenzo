using System;

namespace expenzo.Services.Interfaces;

public interface ICategoryService
    {
        void CreateTable();
        void AddCategory(Models.Category category);
        List<Models.Category> GetCategories();
        void UpdateCategory(Models.Category category);
        void DeleteCategory(int categoryId);
    }