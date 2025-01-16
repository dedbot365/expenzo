using expenzo.Database;
using expenzo.Models;
using expenzo.Services.Interfaces;
using System.Collections.Generic;

namespace expenzo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDao _categoryDao; // Data access object for category-related operations
        // Constructor for the CategoryService class
        public CategoryService(CategoryDao categoryDao)
        {
            _categoryDao = categoryDao;
        }

        public void CreateTable()
        {
            _categoryDao.CreateTable(); // Create the categories table in the database
        }

        public void AddCategory(Category category)
        {
            _categoryDao.AddCategory(category); // Add a new category to the database
        }

        public List<Category> GetCategories()
        {
            return _categoryDao.GetCategories(); // Retrieve all categories from the database
        }

        public void UpdateCategory(Category category)
        {
            _categoryDao.UpdateCategory(category); // Update an existing category in the database
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryDao.DeleteCategory(categoryId); // Delete a category from the database by ID
        }
    }
}