using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWeb.Data;
using HelloWorldWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWeb.Controllers
{
    [Authorize(Roles ="Administrators")]
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext context;

        public SkillsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Skill.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var skill = await this.context.Skill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return this.NotFound();
            }

            return this.View(skill);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SkillUrl")] Skill skill)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(skill);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var skill = await this.context.Skill.FindAsync(id);
            if (skill == null)
            {
                return this.NotFound();
            }

            return this.View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SkillUrl")] Skill skill)
        {
            if (id != skill.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(skill);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.SkillExists(skill.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var skill = await this.context.Skill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return this.NotFound();
            }

            return this.View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await this.context.Skill.FindAsync(id);
            this.context.Skill.Remove(skill);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool SkillExists(int id)
        {
            return this.context.Skill.Any(e => e.Id == id);
        }
    }
}
