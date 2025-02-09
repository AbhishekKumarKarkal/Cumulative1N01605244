﻿using Microsoft.AspNetCore.Mvc;
using Cumulative1N01605244.Models;

namespace Cumulative1N01605244.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;

        
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        // GET: TeacherPage/List
        public IActionResult List()
        {
            List<Teacher> Teachers = _api.ListTeachers();
            return View(Teachers);
        }

        // GET: TeacherPage/Show/{id}
        public IActionResult Show(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            if (SelectedTeacher == null)
            {
                return NotFound("Teacher not found.");
            }
            return View(SelectedTeacher);
        }
    }
}

