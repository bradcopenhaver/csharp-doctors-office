// using Nancy;
// using System.Collections.Generic;
// using DrOffice.Objects;
// using System;
// using System.Globalization;
//
// namespace DrOffice
// {
//   public class HomeModule : NancyModule
//   {
//     public HomeModule()
//         {
//           Get["/"] = _ => {
//               return View["index.cshtml"];
//           };
//           Get["/categories"] = _ => {
//             var allCategories = Category.GetAll();
//             return View["categories.cshtml", allCategories];
//           };
//           Get["/categories/new"] = _ => {
//             return View["category_form.cshtml"];
//           };
//           Post["/categories"] = _ => {
//             var newCategory = new Category(Request.Form["category-name"]);
//             newCategory.Save();
//             var allCategories = Category.GetAll();
//             return View["categories.cshtml", allCategories];
//           };
//           Get["/categories/{id}"] = parameters => {
//             var selectedCategory = Category.Find(parameters.id);
//             List<Task> categoryTasks = selectedCategory.GetTasks();
//             return View["category.cshtml", selectedCategory];
//           };
//           Get["/categories/{id}/tasks/new"] = parameters => {
//             Dictionary<string, object> model = new Dictionary<string, object>();
//             Category selectedCategory = Category.Find(parameters.id);
//             List<Task> allTasks = selectedCategory.GetTasks();
//             model.Add("category", selectedCategory);
//             model.Add("tasks", allTasks);
//             return View["category_tasks_form.cshtml", model];
//           };
//           Post["/tasks"] = _ => {
//             Category selectedCategory = Category.Find(Request.Form["category-id"]);
//             int selectedCategoryId = selectedCategory.GetId();
//             string taskDescription = Request.Form["task-description"];
//             DateTime taskDue = Request.Form["task-due"];
//             Task newTask = new Task(taskDescription, selectedCategoryId, taskDue);
//             newTask.Save();
//             List<Task> categoryTasks = selectedCategory.GetTasks();
//             return View["category.cshtml", selectedCategory];
//           };
//           Post["/"] = _ => {
//             Task.DeleteAll();
//             Category.DeleteAll();
//             return View["index.cshtml"];
//           };
//         }
//       }
//     }
