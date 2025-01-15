using expenzo.Database;
using expenzo.Models;
using expenzo.Services.Interfaces;
using System.Collections.Generic;

namespace expenzo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDao _categoryDao;

        public CategoryService(CategoryDao categoryDao)
        {
            _categoryDao = categoryDao;
        }

        public void CreateTable()
        {
            _categoryDao.CreateTable();
        }

        public void AddCategory(Category category)
        {
            _categoryDao.AddCategory(category);
        }

        public List<Category> GetCategories()
        {
            return _categoryDao.GetCategories();
        }

        public void UpdateCategory(Category category)
        {
            _categoryDao.UpdateCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryDao.DeleteCategory(categoryId);
        }
    }
}