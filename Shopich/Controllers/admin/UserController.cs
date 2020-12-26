using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopich.Models;
using Shopich.Repositories.interfaces;

namespace Shopich.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUser _userRepository;
        private readonly IRole _roleRepository;

        public UserController(IUser userRepository, IRole roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = _userRepository.Include(u => u.Role);
            return View(await users.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(await _roleRepository.GetAll(), "RoleId", "RoleName");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserEmail,UserPassword,UserPhone,UserName,UserSurname,UserCity,UserStreet,UserHouse,UserFlat,UserIndex,UserBirthday,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.Create(user);
                await _userRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(await _roleRepository.GetAll(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetById((int)id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(await _roleRepository.GetAll(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserEmail,UserPassword,UserPhone,UserName,UserSurname,UserCity,UserStreet,UserHouse,UserFlat,UserIndex,UserBirthday,RoleId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.Update(user);
                    await _userRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(await _roleRepository.GetAll(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            await _userRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _userRepository.Exists(id);
        }
    }
}
